namespace Brace.DomainModel.Cryptography
{
    public class EncryptedData
    {
        public byte[] CipherText { get; set; }
        public byte[] Iv { get; set; }
    }
}