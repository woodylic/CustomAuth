using System.Threading.Tasks;

namespace CustomAuth.Client
{
    public interface ICustomAuthClient
    {
        Task<UserProfile> GetUserProfileAsync(string token);
    }
}