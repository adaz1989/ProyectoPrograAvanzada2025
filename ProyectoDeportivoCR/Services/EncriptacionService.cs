using System.Security.Cryptography;
using System.Text;

namespace ProyectoDeportivoCR.Services
{
    public class EncriptacionService : IEncriptacionService
    {

        private readonly IConfiguration _configuration;
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncriptacionService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = Encoding.UTF8.GetBytes(_configuration.GetSection("Variables:llaveCifrado").Value!);
            _iv = new byte[16];

        }

        public string Encriptar(string texto)
        {
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
    }
}
