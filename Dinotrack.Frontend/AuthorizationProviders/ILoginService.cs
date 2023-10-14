namespace Dinotrack.Frontend.AuthorizationProviders
{
    public interface ILoginService
    {
        Task LoginAsync(string token);
        Task LogoutAsync();
    }
}
