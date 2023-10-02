using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.UI.Admin.Mvc.Models;
using System.Reflection;

using Microsoft.AspNetCore.Authorization;

using GoldAggregator.Parser.Extension;
using ImageMagick;

using static System.OperatingSystem;
using static GoldAggregator.Parser.Services.Transliteration;
using AutoMapper;

namespace GoldAggregator.UI.Admin.Mvc.Controllers
{
    // Пагинаци и сортировка
    // https://docs.microsoft.com/ru-ru/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-6.0
    [Authorize]
    public class CoinCatalogsController : MvcBaseController
    {
        const string LinuxImageRootPath = "/var/gold-aggregator/uploads/assets/coins";

        private readonly IConfiguration _configuration;
        private readonly ParserDbContext _context;
        private readonly IMapper _mapper;

        public CoinCatalogsController(
            ParserDbContext context,
            IConfiguration configuration,
            IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public string UploadFilesFolderPath => this.GetUploadFilesFolderPath();

        // GET: CoinCatalogs
        public async Task<IActionResult> Index(string? sortOrder)
        {
            var coinCatalogs = await _context.CoinCatalogs.Include(c => c.MintCountry).ToListAsync();
            var mappedCoinCatalogs = this._mapper.Map<IEnumerable<CoinCatalog>, IEnumerable<CoinCatalogViewModel>>(coinCatalogs).ToList();

            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MetalTypeParm"] = string.IsNullOrEmpty(sortOrder) ? "metal_type_desc" : "";
            ViewData["WeightParm"] = string.IsNullOrEmpty(sortOrder) ? "weight_desc" : "";

            switch (sortOrder)
            {
                case "id_desc":
                    return View(mappedCoinCatalogs.OrderByDescending(s => s.Id));
                case "name_desc":
                    return View(mappedCoinCatalogs.OrderByDescending(s => s.Name));
                case "metal_type_desc":
                    return View(mappedCoinCatalogs.OrderByDescending(s => s.MetalType));
                case "weight_desc":
                    return View(mappedCoinCatalogs.OrderByDescending(s => s.MetalType));
                default:
                    return View(mappedCoinCatalogs.OrderBy(s => s.Name));
            }
        }

        // GET: CoinCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CoinCatalogs == null)
            {
                return NotFound();
            }

            var coinCatalog = await _context.CoinCatalogs
                .Include(c => c.MintCountry)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (coinCatalog == null)
            {
                return NotFound();
            }

            return View(coinCatalog);
        }

        // GET: CoinCatalogs/Create
        public async Task<IActionResult> Create()
        {
            var mintCountriesId = await _context.MintCountries.ToListAsync();

            mintCountriesId.Insert(0, new MintCountry());  // <= Empty option

            ViewData["MintCountryId"] = new SelectList(mintCountriesId, "Id", "Name");
            return View();
        }

        // POST: CoinCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoinCatalogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var translitTitle = base.GetTranslitTitle(model.Name, model.Weight, model.MetalType);

                var coinCatalog = new CoinCatalog
                {
                    Name = model.Name,
                    TranslitName = Generate(model.Name),
                    SeoUrl = translitTitle,
                    SeoTitle = model.SeoTitle,
                    SeoDescription = model.SeoDescription,
                    SeoKeyWords = model.SeoKeyWords,
                    Nomination = model.Nomination,
                    Weight = model.Weight,
                    MetalType = model.MetalType,
                    MintCountryId = model.MintCountryId,
                    StartMiningYear = model.StartMiningYear,
                    EndMiningYear = model.EndMiningYear,
                    ImageName = translitTitle
                };

                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    // Try save data first
                    await _context.AddAsync(coinCatalog);
                    await _context.SaveChangesAsync();

                    // Save images
                    if (model.FileObverse?.Length > 0 && model.FileReverse?.Length > 0)
                    {
                        await SaveImagesAsyns(model.FileObverse, this.UploadFilesFolderPath, translitTitle, "obverse");
                        await SaveImagesAsyns(model.FileReverse, this.UploadFilesFolderPath, translitTitle, "reverse");
                    }

                    // If all good then commit
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                return RedirectToAction(nameof(Details), new { id = coinCatalog.Id });
            }

            ViewData["MintCountryId"] = new SelectList(_context.MintCountries, "Id", "Name", model.MintCountryId);
            return View(model);
        }

        // GET: CoinCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CoinCatalogs == null) return NotFound();

            var coinCatalog = await _context.CoinCatalogs.FindAsync(id); if (coinCatalog == null) return NotFound();
            var model = _mapper.Map<CoinCatalogViewModel>(coinCatalog);
            var uploadFilesFolder = this.GetUploadFilesFolderPath();


            if (!coinCatalog.ImageName.IsEmpty())
            {
                ViewData["SimilarImagesInFolder"] = this.GetImagesByDirectory(uploadFilesFolder, coinCatalog.ImageName);
            }

            ViewData["MintCountryId"] = new SelectList(_context.MintCountries, "Id", "Name", coinCatalog.MintCountryId);
            return View(model);
        }

        // POST: CoinCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CoinCatalogViewModel model)
        {
            #region Validation
            if (id != model.Id)
            {
                return NotFound();
            }

            var coinCatalog = await _context.CoinCatalogs.FindAsync(id);
            if (coinCatalog == null)
            {
                return NotFound($"Пустой объект {nameof(coinCatalog)}");
            }

            var isNotEnoughPhotos = new[] { model.FileObverse, model.FileReverse }.Where(m => m == null).Count() == 1;
            if (isNotEnoughPhotos)
            {
                ModelState.AddModelError("", $"Необходимо два фото {nameof(model.FileObverse)}, {nameof(model.FileReverse)}");
            }

            var hasNewName = !model.Name.Equals(coinCatalog.Name, StringComparison.InvariantCultureIgnoreCase);
            if (hasNewName && (model.FileObverse == null || model.FileReverse == null))
            {
                ModelState.AddModelError("", $"Если поменяли имя, надо также добавить фотографии '{nameof(model.FileObverse)}', '{nameof(model.FileReverse)}'");
            }

            if (hasNewName)
            {
                var newTranslitTitle = base.GetTranslitTitle(model.Name, model.Weight, model.MetalType);
                var hasOldFiles = this.GetImagesByDirectory(this.UploadFilesFolderPath, newTranslitTitle).Count > 0;
                if (hasOldFiles) ModelState.AddModelError("", $"Чтобы сохранить изображения с новым именем '{newTranslitTitle}'. Сначала, удалите старые фотографии.");
            }


            if (!ModelState.IsValid)
            {
                ViewData["MintCountryId"] = new SelectList(_context.MintCountries, "Id", "Name", model.MintCountryId);
                return View(model);
            }
            #endregion

            var translitTitle = base.GetTranslitTitle(model.Name, model.Weight, model.MetalType);

            coinCatalog.Name = model.Name;
            coinCatalog.TranslitName = model.TranslitName;
            coinCatalog.SeoUrl = model.SeoUrl;
            coinCatalog.SeoTitle = model.SeoTitle;
            coinCatalog.SeoDescription = model.SeoDescription;
            coinCatalog.SeoKeyWords = model.SeoKeyWords;
            coinCatalog.Nomination = model.Nomination;
            coinCatalog.Weight = model.Weight;
            coinCatalog.MetalType = model.MetalType;
            coinCatalog.MintCountryId = model.MintCountryId;
            coinCatalog.StartMiningYear = model.StartMiningYear;
            coinCatalog.EndMiningYear = model.EndMiningYear;
            coinCatalog.ImageName = translitTitle;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Save item
                await _context.SaveChangesAsync();

                // Save images
                if (model.FileObverse?.Length > 0 && model.FileReverse?.Length > 0)
                {
                    // Delete existed files first
                    // For optimization
                    var uploadFilesFolder = this.UploadFilesFolderPath;
                    var imagePathes = this.GetImagesByDirectory(uploadFilesFolder, translitTitle);
                    if (imagePathes?.Count > 0)
                    {
                        imagePathes.ForEach(imagePath => System.IO.File.Delete(imagePath));
                    }

                    // Save files
                    await SaveImagesAsyns(model.FileObverse, uploadFilesFolder, translitTitle, "obverse");
                    await SaveImagesAsyns(model.FileReverse, uploadFilesFolder, translitTitle, "reverse");
                }


                // If all good then commit
                await transaction.CommitAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();
                return !CoinCatalogExists(coinCatalog.Id) ? NotFound() : throw new Exception(ex.Message, ex);
            }
            return RedirectToAction(nameof(Details), new { Id = id });
        }

        // GET: CoinCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CoinCatalogs == null)
            {
                return NotFound();
            }

            var coinCatalog = await _context.CoinCatalogs
                .Include(c => c.MintCountry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coinCatalog == null)
            {
                return NotFound();
            }

            return View(coinCatalog);
        }

        // POST: CoinCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CoinCatalogs == null)
            {
                return Problem("Entity set 'ParserDbContext.CoinCatalogs'  is null.");
            }
            var coinCatalog = await _context.CoinCatalogs.FindAsync(id);
            if (coinCatalog != null)
            {
                _context.CoinCatalogs.Remove(coinCatalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("DeleteImage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImage(string filePath, int id)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToAction("Edit", new { id = id });
        }

        private async Task SaveImagesAsyns(IFormFile? file, string imageUploadFolderPath, string uniqueFileName, string postfixFileName = "obverse")
        {
            if (file == null) throw new ArgumentNullException("File is null");

            // The library: https://github.com/dlemstra/Magick.NET
            // The optimization: https://stackoverflow.com/questions/53908949/how-to-serve-a-jpg-for-browser-that-not-support-webp-in-a-proper-way
            // The google webp: https://developers.google.com/speed/webp/docs/cwebp

            var exts = new List<MagickFormat>();
            exts.Add(MagickFormat.WebP);
            exts.Add(MagickFormat.Png);

            const int width = 1024;

            if (file?.Length > 0)
            {
                foreach (var ext in exts)
                {
                    string fileName = $"{uniqueFileName}_{postfixFileName}.{ext}".ToLower();
                    string fileFullPath = Path.Combine(imageUploadFolderPath, fileName);

                    using var fileStream = System.IO.File.Create(fileFullPath);
                    using var image = new MagickImage(file.OpenReadStream());

                    var w = image.Width < width ? image.Width : width;
                    var size = new MagickGeometry(w, 0) { IgnoreAspectRatio = false };

                    // Sets the output format to webp, png
                    image.Format = ext;
                    image.Quality = 70;
                    image.Resize(size);

                    // Write the image to the fileStream
                    await image.WriteAsync(fileStream);
                }
            }
        }

        private bool CoinCatalogExists(int id)
        {
            return (_context.CoinCatalogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string GetUploadFilesFolderPath()
        {
            var imageUploadFolderPath =
                    IsLinux() ? Path.GetFullPath(_configuration.GetValue<string>("LinuxImageRootPath") ?? LinuxImageRootPath) :
                    IsWindows() ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) : "no folder determined";

            if (imageUploadFolderPath == null)
            {
                throw new NullReferenceException($"Variable {nameof(imageUploadFolderPath)} is null");
            }

            if (!Directory.Exists(imageUploadFolderPath))
            {
                throw new DirectoryNotFoundException($"Directory not found by path {imageUploadFolderPath}. Create it please");
            }

            return imageUploadFolderPath;
        }

        /// <summary>
        /// Get all files matched by <paramref name="imageName"/>
        /// </summary>
        /// <param name="uploadFilesFolder">full folder path</param>
        /// <param name="imageName">File image name</param>
        private List<string> GetImagesByDirectory(string uploadFilesFolder, string imageName)
        {
            return Directory.GetFiles(uploadFilesFolder).Where(f => f.Contains(imageName)).ToList();
        }
    }
}
