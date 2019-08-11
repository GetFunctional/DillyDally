using System;
using System.IO;
using System.Security.Cryptography;

namespace GF.DillyDally.WriteModel.Files.Aggregates.Files.Filestore
{
    internal sealed class Md5HashGenerator
    {
        internal string CreateMd5HashFromFile(string filePath)
        {
            return this.CreateMd5Hash(File.OpenRead(filePath));
        }

        private string CreateMd5Hash(Stream stream)
        {
            string md5HashString;
            using (var md5 = MD5.Create())
            {
                using (stream)
                {
                    var md5Hash = md5.ComputeHash(stream);
                    md5HashString = BitConverter.ToString(md5Hash).Replace("-", "").ToLowerInvariant();
                }
            }

            return md5HashString;
        }

        internal string CreateMd5HashFromBinary(byte[] binary)
        {
            return this.CreateMd5Hash(new MemoryStream(binary));
        }
    }
}