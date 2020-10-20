using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.StockService
{
    public interface IStockService
    {
        string GetStockPrice(string symbol, int nDay, string apiKey);
    }
}
