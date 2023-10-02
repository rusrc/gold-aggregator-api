using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Parser.DbContext.Seedings
{
    public static class DealerSeeding
    {
        public static void SeedDealers(this ModelBuilder builder)
        {
            var data = new[]
            {
                new Dealer()
                {
                   Id = 1,                  
                   Name="Золотой монетный дом",
                   Description="Монетный дилер в Москве, Санкт-Петребурге, Нижнем Новгороде, Севастополе, Казани. Покупка и продажа золотых, серебрянных, платиновых монет",
                   BaseUrl ="https://zoloto-md.ru",
                   DealerType =DealerTypeEnum.Dealer,
                   ProviderName="ZolotoMdRuProvider",
                   HasDelivery = false
                },
                 new Dealer()
                {
                   Id = 2,                  
                   Name="Монета",
                   Description="Монетный дилер в Санкт-петребурге. Покупка и продажа золотых, серебрянных, платиновых монет",
                   BaseUrl ="https://monetainvest.ru/",
                   DealerType =DealerTypeEnum.Dealer,
                   ProviderName="MonetaInvestProvider",
                   HasDelivery = true
                },
                 new Dealer()
                 {
                    Id = 3,
                    Name="ООО «Инвестиции в Золото»",
                    Description="Монетный дилер в Санкт-Петербурге. Покупка и продажа золотых, серебрянных, платиновых монет",
                    BaseUrl = "http://zoloto-piter.ru/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "ZolotoPiterRuProvider",
                 },
                 new Dealer()
                 {
                    Id = 4,                   
                    Name="МонетаИнвест",
                    Description="Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет",
                    BaseUrl = "http://a-fin.net/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "AFinProvider",
                 },
                 new Dealer()
                 {
                    Id = 5,                  
                    Name="Золотой Департамент",
                    Description="Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет",
                    BaseUrl = "https://golddep.ru/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "GoldDepProvider",
                 },
                  new Dealer()
                 {
                    Id = 6,
                    Name="Золото Державы",
                    Description="Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет",
                    BaseUrl = "https://9999d.ru/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "GoldDergavaProvider",
                 },
                  new Dealer()
                 {
                    Id = 7,
                    Name="Петроинвест",
                    Description="Монетный дилер в Санкт-Петербурге. Покупка и продажа золотых, серебрянных, платиновых монет",
                    BaseUrl = "http://petroinvest.ru/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "PetroinvestProvider",
                 },
                  new Dealer()
                 {
                    Id = 8,
                    Name="Neva Gold",
                    Description="Золотые слитки в Санкт-Петербурге.",
                    BaseUrl = "https://neva-gold.ru/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "NevaGoldProvider",
                 },
                  new Dealer()
                 {
                    Id = 9,
                    Name="Инвестиционно-Финансовая Компания \"Пик\"",
                    Description="Монетный дилер в Санкт-Петербурге. Покупка и продажа золотых, серебрянных, платиновых монет",
                    BaseUrl = "http://ifk-pik.ru/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "IFK-PikProvider",
                 },
                  new Dealer()
                 {
                    Id = 10,
                    Name="Клуб Нумизмат",
                    Description="Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет",
                    BaseUrl = "https://www.numizmatik.ru/",
                    DealerType = DealerTypeEnum.Dealer,
                    ProviderName= "NumizmatikProvider",
                 },                
             };
            builder.Entity<Dealer>().HasData(data);
        }
    }
}