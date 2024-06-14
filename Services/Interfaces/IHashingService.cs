namespace ColorPaletteGeneratorApi.Services.Interfaces
{
    public interface IHashingService
    {
        byte[] CreateHash(string value, byte[] key);
        byte[] GenerateKey();
        bool VerifyHash(string value, byte[] key, byte[] hash);
    }
}