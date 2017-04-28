namespace Brace.DomainModel.Cryptography
{
    public class EcryptedData
    {
        public byte[] CipherText { get; set; }
        public byte[] Iv { get; set; }
    }
}