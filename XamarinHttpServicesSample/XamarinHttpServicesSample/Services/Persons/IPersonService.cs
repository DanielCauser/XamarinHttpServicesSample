using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinHttpServicesSample.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Models.Person>> GetPersonsAsync();
    }
}
