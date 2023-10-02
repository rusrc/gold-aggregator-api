using GoldAggregator.Parser.Managers;

namespace GoldAggregator.Parser.WorkerService.ZolotoMdRu
{
    /// <summary>
    /// https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio
    /// </summary>
    public class Worker : BackgroundService, IDisposable
    {
        private readonly ILogger<Worker> _logger;      
        private readonly IConfiguration _configuration;

        public Worker(
            IServiceProvider services,
            ILogger<Worker> logger,
            IConfiguration configuration
            )
        {
            _logger = logger;
            Services = services;
            _configuration = configuration;
        }

        public IServiceProvider Services { get; }

        /// <summary>      
        /// You could create a service by implementing IHostedService. That interface has StartAsync and StopAsync.
        /// BackgroundService is an(base) implementation of IHostedService, and could be used for long running tasks.
        /// This one defines the abstract ExecuteAsync.
        /// 
        /// In summary
        /// When inheriting from BackgroundService, implement ExecuteAsync
        /// When implementing IHostedService, implement StartAsync and StopAsync
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service running.");

            await DoWork(stoppingToken);              
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            var isActive = _configuration.GetValue<bool>("Settings:isActive");
            while (isActive)
            {

                var now = DateTime.Now;
                //В ночное время скипаем парсинг
                if (now.Hour >= 7 || now.Hour <= 23)
                {
                    _logger.LogInformation("Consume Scoped Service Hosted Service is working.");

                    using (var scope = Services.CreateScope())
                    {
                        var saveUrlsManager = scope.ServiceProvider.GetRequiredService<ISaveUrlsManager>();
                        var saveItemsManager = scope.ServiceProvider.GetRequiredService<ISaveItemsManager>();

                        await RunGetUrlsAsync(saveUrlsManager, cancellationToken);
                        await RunGetItemsAsync(saveItemsManager, cancellationToken);
                    }
                }
                // Проверяем, вдруг конфиг изменился
                isActive = _configuration.GetValue<bool>("Settings:isActive");
                var periodInMinutes = _configuration.GetValue<int>("Settings:periodInMinutes");

                await Task.Delay(TimeSpan.FromMinutes(periodInMinutes), cancellationToken);
            }
        }

        private async Task RunGetUrlsAsync(ISaveUrlsManager saveUrlsManager, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting get urls...");
            await saveUrlsManager.RunAsync(cancellationToken);
            _logger.LogInformation("End get urls...");
        }

        private async Task RunGetItemsAsync(ISaveItemsManager saveItemsManager, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting get items...");
            await saveItemsManager.RunAsync(cancellationToken);
            _logger.LogInformation("End get items...");
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");
            await base.StopAsync(stoppingToken);
        }
    }
}