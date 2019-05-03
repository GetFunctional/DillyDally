using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GF.DillyDally.Mvvmc;
using GF.DillyDally.Wpf.Client.Core.Dialoge;
using MediatR;

namespace GF.DillyDally.Wpf.Client.Presentation.Common.Currencies.CreateCurrency
{
    public sealed class NewCurrencyRequestHandler : IRequestHandler<NewCurrencyRequest, NewCurrencyResponse>
    {
        private readonly IDialogService _dialogService;
        private readonly ControllerFactory<NewCurrencyController> _newCurrencyControllerFactory;

        public NewCurrencyRequestHandler(IDialogService dialogService,
            ControllerFactory<NewCurrencyController> newCurrencyControllerFactory)
        {
            this._dialogService = dialogService;
            this._newCurrencyControllerFactory = newCurrencyControllerFactory;
        }

        #region IRequestHandler<NewCurrencyRequest,NewCurrencyResponse> Members

        public async Task<NewCurrencyResponse> Handle(NewCurrencyRequest request, CancellationToken cancellationToken)
        {
            var controller = await this._newCurrencyControllerFactory.CreateControllerAsync();

            bool ConfirmationCondition()
            {
                return this.DialogConfirmationCondition(controller);
            }

            var saveNewCurrency = new DialogCommandResult("Währung anlegen", ConfirmationCondition);
            var cancelProcess = new DialogCommandResult("Abbrechen");
            var dialogSettings =
                new DialogSettings(saveNewCurrency, new List<IDialogResult> {saveNewCurrency, cancelProcess},
                    new Size(400, 300));
            var dialogResult = await this._dialogService.ShowDialogAsync(controller, dialogSettings);

            if (dialogResult == saveNewCurrency)
            {
                var newCurrency = await controller.SaveNewCurrency().ConfigureAwait(true);
                return new NewCurrencyResponse(newCurrency);
            }

            return new NewCurrencyResponse();
        }

        #endregion

        private bool DialogConfirmationCondition(NewCurrencyController controller)
        {
            var validationResult = controller.ValidateCurrentCurrencyData();
            return validationResult;
        }
    }
}