#region

using Microsoft.Maui.Storage;

#endregion
namespace BollettinoMeteoTrento.Services.StorageServices;

public sealed class JwtStorageService
{
    private const string TOKEN_KEY_NAME = "Jwt";

    public async Task StoreTokenAsync(string token)
    {
        try
        {
            await SecureStorage.SetAsync(TOKEN_KEY_NAME, token);
        }
        catch (Exception ex)
        {
            // Handle exception when SecureStorage is not supported on the device
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }

    public async Task<string?> RetrieveTokenAsync()
    {
        try
        {
            return await SecureStorage.GetAsync(TOKEN_KEY_NAME);
        }
        catch (Exception ex)
        {
            // Handle exception when SecureStorage is not supported on the device
            Console.WriteLine($"Exception: {ex.Message}");
            return null;
        }
    }

    public void ClearToken()
    {
        SecureStorage.Remove(TOKEN_KEY_NAME);
    }
}
