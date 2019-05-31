namespace GF.DillyDally.Mvvmc.Validation
{
    public interface IValidationRule<in TValidatationObject> : IValidationRule where TValidatationObject : IValidateable
    {
    }
}