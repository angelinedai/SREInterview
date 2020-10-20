using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Interview.StockService;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SREInterview.Controllers
{
    [Route("api/StockPrice")]
    [ApiController]
    public class StockPriceController : ControllerBase
    {
        private readonly IStockService _stockService = new StockService();
        private IOptionsSnapshot<AppConfiguration> _appSettings;

        public StockPriceController(IOptionsSnapshot<AppConfiguration> appSettings)
        {
            _appSettings = appSettings;
        }

        // GET api/<StockPriceController>/5
        [HttpGet("symbol/{symbol}/nday/{nDay}")]
        public string Get(string symbol, int nDay)
        {
            var apiKey = _appSettings.Value.ApiKeyFromAppsettings;
            if (!string.IsNullOrEmpty(_appSettings.Value.SymbolFromKubernetesEnv))
                apiKey = _appSettings.Value.ApiKeyFromKubernetesEnv;

            var result = _stockService.GetStockPrice(symbol, nDay, apiKey);

            return result;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var symbol = _appSettings.Value.SymbolFromAppsettings;
            var nDay = int.Parse(_appSettings.Value.NDayFromAppsettings);
            var apiKey = _appSettings.Value.ApiKeyFromAppsettings;

            if (!string.IsNullOrEmpty(_appSettings.Value.SymbolFromKubernetesEnv))
                symbol = _appSettings.Value.SymbolFromKubernetesEnv;

            if (!string.IsNullOrEmpty(_appSettings.Value.NDayFromKubernetesEnv))
                nDay = int.Parse(_appSettings.Value.NDayFromKubernetesEnv);

            if (!string.IsNullOrEmpty(_appSettings.Value.SymbolFromKubernetesEnv))
                apiKey = _appSettings.Value.ApiKeyFromKubernetesEnv;

            return new List<string>()
            {
                _stockService.GetStockPrice(symbol, nDay, apiKey),
                "[Parameters settings as following]:",
                $"SymbolFromAppsettings: {_appSettings.Value.SymbolFromAppsettings}",
                $"SymbolFromKubernetesEnv: {_appSettings.Value.SymbolFromKubernetesEnv}",
                $"NDayFromAppsettings: {_appSettings.Value.NDayFromAppsettings}",
                $"NDayFromKubernetesEnv: {_appSettings.Value.NDayFromKubernetesEnv}",
                $"ApiKeyFromAppsettings: {_appSettings.Value.ApiKeyFromAppsettings}",
                $"ApiKeyFromKubernetesEnv: {_appSettings.Value.ApiKeyFromKubernetesEnv}"
             };
        }
    }
}
