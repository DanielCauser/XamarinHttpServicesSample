using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinHttpServicesSample.Extensions;
using XamarinHttpServicesSample.Models;

namespace XamarinHttpServicesSample.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRequestsService _requestService;

        public PersonService(IRequestsService requestService)
        {
            _requestService = requestService;
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            UriBuilder builder = new UriBuilder($"{AppSettings.PersonsEndpoint}?ext&amount=25");

            var url = builder.ToString();

            return await _requestService.GetAsync<IEnumerable<Person>>(url);
        }
    }
}
