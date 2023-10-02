using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Parser.DbContext.Seedings
{
    public static class DealerOfficesSeeding
    {
        public static void SeedDealerOffices(this ModelBuilder builder)
        {
            var data = new[]
            {
                //Золотой монетный дом
                new DealerOffice(){
                    Id=1,
                    DealerId = 1,
                    CityId= 1,
                },new DealerOffice(){
                    Id=2,
                    DealerId = 1,
                    CityId= 2,
                },new DealerOffice(){
                    Id=3,
                    DealerId = 1,
                    CityId= 3,
                },new DealerOffice(){
                    Id=4,
                    DealerId = 1,
                    CityId= 4,
                },new DealerOffice(){
                    Id=5,
                    DealerId = 1,
                    CityId= 6,

                //Монета
                },new DealerOffice(){
                    Id=6,
                    DealerId = 2,
                    CityId= 2,
                //ООО «Инвестиции в Золото»
                },new DealerOffice(){
                    Id=7,
                    DealerId = 3,
                    CityId= 2,
                //МонетаИнвест
                },new DealerOffice(){
                    Id=8,
                    DealerId = 4,
                    CityId= 1,
                //Золотой Департамент
                },new DealerOffice(){
                    Id=9,
                    DealerId = 5,
                    CityId= 1,
                //Золото Державы
                },new DealerOffice(){
                    Id=10,
                    DealerId = 6,
                    CityId= 1,
                //Петроинвест
                },new DealerOffice(){
                    Id=11,
                    DealerId = 7,
                    CityId= 2,
                //Neva Gold
                },new DealerOffice(){
                    Id=12,
                    DealerId = 8,
                    CityId= 2,
                //Инвестиционно-Финансовая Компания "Пик"
                },new DealerOffice(){
                    Id=13,
                    DealerId = 9,
                    CityId= 2,
                //Клуб Нумизмат
                },new DealerOffice(){
                    Id=14,
                    DealerId = 10,
                    CityId= 1,
                } 
            };
            builder.Entity<DealerOffice>().HasData(data);
        }
    }
}