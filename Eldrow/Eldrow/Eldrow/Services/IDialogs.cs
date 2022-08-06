using System.Threading.Tasks;

namespace Eldrow.Services
{
    public interface IDialogs
    {
        Task ShowMessageAsync(string title, string message);
    }
}