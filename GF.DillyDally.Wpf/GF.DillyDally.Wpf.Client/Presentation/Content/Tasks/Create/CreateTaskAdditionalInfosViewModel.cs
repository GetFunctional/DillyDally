using System;
using GF.DillyDally.Mvvmc;
using ReactiveUI;

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
            get
            {
                return this._dueDate;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._dueDate, value);
            }
        }

        public string DefinitionOfDone
        {
            get
            {
                return this._definitionOfDone;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._definitionOfDone, value);
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._description, value);
            }
        }
    }
}