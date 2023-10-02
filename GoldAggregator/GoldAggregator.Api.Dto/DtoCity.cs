using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldAggregator.Api.Dto
{
    public class DtoCity
    {
        public int Id { get; set; }
        /// <summary>
        /// Название города
        /// </summary>
        public string Name { get; set; }

        public string NameGenitive { get; set; }

        public string NameDative { get; set; }

        public string NameAccusative { get; set; }

        public string NameInstrumental { get; set; }
        /// <summary>
        /// Транслитирация названия
        /// </summary>
        public string TranslitName { get; set; }
    }
}
