using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Cryptography
{
    class CryptoExamples
    {
        private byte[] KeyAndIV = Encoding.UTF8.GetBytes("Kolorowejarmarki");
        private string SecretText = "Super tajny tekst";

        [Example("Symmetric Example", false)]
        public void SymmetricExamples()
        {

            var symmetricAlgorythm = SymmetricAlgorithm.Create();
            symmetricAlgorythm.Padding = PaddingMode.PKCS7;
            ICryptoTransform encryptor = symmetricAlgorythm.CreateEncryptor(KeyAndIV, KeyAndIV);
            var cipher = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(SecretText), 0,SecretText.Length);


            var file = new FileStream(SecretFilePath, FileMode.Truncate);
            file.Write(cipher, 0, cipher.Length);
            file.Close();
        }

        [Example("Symmetric Decript Example", false)]
        public void SymmetricDecriptExamples()
        {
            var algorithm = SymmetricAlgorithm.Create();
            algorithm.Padding = PaddingMode.PKCS7;
            var decryptor = algorithm.CreateDecryptor(KeyAndIV, KeyAndIV);
            var file = File.OpenRead(SecretFilePath);

            var buffer = new byte[file.Length];
            var memory = new MemoryStream();

            file.Read(buffer, 0, (int)file.Length);

            var decrypted = decryptor.TransformFinalBlock(buffer, 0, (int)file.Length);
            file.Close();

            Console.WriteLine(Encoding.UTF8.GetString(decrypted, 0, decrypted.Length));

        }

        private string SecretFilePath = @"c:\secret.txt";

        [Example("Creypto Stream Example", false)]
        public void CreyptoStreamExample()
        {
            var algorithm = SymmetricAlgorithm.Create();
            //algorithm.GenerateKey();
            //algorithm.GenerateIV();

            using(var file = File.Create(@".\key.txt"))
            {
              //  file.Write(algorithm.Key, 0, algorithm.Key.Length);
            }

            using (var file = File.Create(@".\IV.txt"))
            {
                //file.Write(algorithm.IV, 0, algorithm.IV.Length);
            }

            algorithm.Key = KeyAndIV;
            algorithm.IV = KeyAndIV;

            var encryptor = algorithm.CreateEncryptor();
            algorithm.Padding = PaddingMode.None;
            
            using(var inputStream = new FileStream(@".\EncryptedFile.txt", FileMode.OpenOrCreate))
            using(var cryptoStream = new CryptoStream(inputStream, encryptor, CryptoStreamMode.Write))
            {
                var input = Encoding.UTF8.GetBytes(SecretText);
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.Clear();
            }
        }

        [Example("Decrypt Stream Example", false)]
        public void DecryptStreamExample()
        {
            var algorithm = SymmetricAlgorithm.Create();
            algorithm.Padding = PaddingMode.None;

            using (var file = File.OpenRead(@".\key.txt"))
            {
                file.Read(algorithm.Key, 0, (int)file.Length);
            }

            using (var file = File.OpenRead(@".\IV.txt"))
            {
                //file.Read(algorithm.IV, 0, (int)file.Length);
            }

            algorithm.Key = KeyAndIV;
            algorithm.IV = KeyAndIV;

            var decryptor = algorithm.CreateDecryptor();

            var stream = new FileStream(@".\EncryptedFile.txt", FileMode.Open);
            var buffer = new byte[stream.Length];
            
            stream.Read(buffer, 0, (int)stream.Length);

            using(var memory = new MemoryStream(buffer))
            using (var crypto = new CryptoStream(memory, decryptor, CryptoStreamMode.Read))
            {
                var streamReader = new StreamReader(crypto);
                var newBuffer = new byte[5000];
                crypto.Read(newBuffer, 0, 5000);
                Console.WriteLine("Direct text: " + Encoding.UTF8.GetString(newBuffer));
                Console.WriteLine("Tajny text: " + streamReader.ReadToEnd());
            }
        }

        [Example("Hash Example", true)]
        public void HashExample()
        {
            var hash = SHA512.Create();
            var hashed = hash.ComputeHash(Encoding.UTF8.GetBytes("test"));
            Console.WriteLine(Convert.ToBase64String(hashed));
        }
    }
}
