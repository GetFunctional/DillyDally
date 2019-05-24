namespace GF.DillyDally.WriteModel.Domain.Files
{
    internal class FileParameterData
    {
        public FileParameterData(string filePath)
        {
            this.FilePath = filePath;
        }

        public FileParameterData(byte[] binary)
        {
            this.Binary = binary;
        }

        public string FilePath { get; }

        public byte[] Binary { get; }
    }
}