namespace Brace.DomainModel.Cryptography
{
    public interface ICryptography
    {
        EncryptedData Encrypt(string key, string original);
        string Decrypt(EncryptedData encryptedData, string key);
    }
}