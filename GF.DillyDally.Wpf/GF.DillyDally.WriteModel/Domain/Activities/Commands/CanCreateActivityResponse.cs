namespace GF.DillyDally.WriteModel.Domain.Activities.Commands
{
    public class CanCreateActivityResponse
    {
        public CanCreateActivityResponse(bool canCreate)
        {
            this.CanCreate = canCreate;
        }


        public bool CanCreate { get; }
    }
}