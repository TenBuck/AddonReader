using System.DirectoryServices.ActiveDirectory;
using System.Threading.Tasks;

namespace TenBot.Core
{
    public interface IBotState
    {
        public Task Update();
    }
}