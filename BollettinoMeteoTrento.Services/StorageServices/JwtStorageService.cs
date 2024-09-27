#region

using Microsoft.Maui.Storage;

#endregion
namespace BollettinoMeteoTrento.Services.StorageServices;

public sealed class JwtStorageService
{
    private const string TokenKeyName = "Jwt";

    public async Task StoreTokenAsync(string token)
    {
        try
        {
            await SecureStorage.SetAsync(TokenKeyName, token);
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
            return await SecureStorage.GetAsync(TokenKeyName);
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
        SecureStorage.Remove(TokenKeyName);
    }
}
