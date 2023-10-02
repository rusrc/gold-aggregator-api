
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldAggregator.Parser.Services.Abstractions
{
    public interface IMapService
    {
        string GetCityNameByAddress(string address);
    }
}
