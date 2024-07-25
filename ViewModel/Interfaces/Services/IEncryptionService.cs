namespace ViewModel.Interfaces.Services
{
    public interface IEncryptionService
    {
        public byte[] Encrypt(byte[] data);

        public byte[] Decrypt(byte[] data);
    }
}
