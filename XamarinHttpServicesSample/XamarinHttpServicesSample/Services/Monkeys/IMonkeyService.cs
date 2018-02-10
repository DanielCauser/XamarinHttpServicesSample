using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinHttpServicesSample.Services
{
    public interface IMonkeyService
    {
        Task<IEnumerable<Models.Monkeys>> GetMonkeysAsync();
    }
}
