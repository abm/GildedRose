using System.Threading.Tasks;

namespace GildedRose
{
    public interface ITokenService
    {
        Task<Customer> Authenticate(string token);
    }
}