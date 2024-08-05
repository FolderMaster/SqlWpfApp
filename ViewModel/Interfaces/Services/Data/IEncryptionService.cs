namespace ViewModel.Interfaces.Services.Data
{
    public interface IEncryptionService
    {
        public byte[] Encrypt(byte[] data);

        public byte[] Decrypt(byte[] data);
    }
}
