using System;
using GF.DillyDally.Mvvmc;

namespace GF.DillyDally.Wpf.Client.Presentation.Content.Tasks.Create
{
    internal sealed class CreateTaskAdditionalInfosViewModel : DisplayPageViewModelBase
    {
        private string _definitionOfDone;
        private string _description;
        private DateTime? _dueDate;
        public override string Title { get; } = "Additional Infos";

        public DateTime? DueDate
        {
            get { return this._dueDate; }
            set { this.SetAndRaiseIfChanged(ref this._dueDate, value); }
        }

        public string DefinitionOfDone
        {
            get { return this._definitionOfDone; }
            set { this.SetAndRaiseIfChanged(ref this._definitionOfDone, value); }
        }

        public string Description
        {
            get { return this._description; }
            set { this.SetAndRaiseIfChanged(ref this._description, value); }
        }
    }
}