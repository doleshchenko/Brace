namespace Brace.DomainModel.Cryptography
{
    public interface ICryptography
    {
        EcryptedData Encrypt(string key, string original);
        string Decrypt(EcryptedData encryptedData, string key);
    }
}