namespace GF.DillyDally.Mvvmc.Validation
{
    public sealed class PropertyError
    {
        public PropertyError(string propertyName, string error)
        {
            this.PropertyName = propertyName;
            this.Error = error;
        }

        public string PropertyName { get; }

        public string Error { get; }
    }
}