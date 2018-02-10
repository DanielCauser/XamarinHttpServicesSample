using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinHttpServicesSample.Extensions;
using XamarinHttpServicesSample.Models;

namespace XamarinHttpServicesSample.Services
{
    public class MonkeysService : IMonkeyService
    {
        private readonly IRequestsService _requestService;

        public MonkeysService(IRequestsService requestService)
        {
            _requestService = requestService;
        }
        public async Task<IEnumerable<Monkeys>> GetMonkeysAsync()
        {
            UriBuilder builder = new UriBuilder(AppSettings.MonkeysEndpoint);
            builder.AppendToPath("monkeys");

            var url = builder.ToString();

            return await _requestService.GetAsync<IEnumerable<Monkeys>>(url);
        }
    }
}
