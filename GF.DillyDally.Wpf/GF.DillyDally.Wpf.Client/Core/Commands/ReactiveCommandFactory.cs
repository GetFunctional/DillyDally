using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Mvvm.Native;
using GF.DillyDally.Wpf.Client.Core.Exceptions;
using ReactiveUI;

namespace GF.DillyDally.Wpf.Client.Core.Commands
{
    internal sealed class ReactiveCommandFactory : IDisposable
    {
        private readonly ISet<IDisposable> _commandDisposables = new HashSet<IDisposable>();

        #region IDisposable Members

        public void Dispose()
        {
            this._commandDisposables.ForEach(dis => dis.Dispose());
        }

        #endregion

        /// <summary>
        ///     Creates a parameterless <see cref="ReactiveCommand{TParam, TResult}" /> with asynchronous execution logic.
        /// </summary>
        /// <param name="execute">
        ///     Provides a <see cref="Task" /> representing the command's asynchronous execution logic.
        /// </param>
        /// <param name="canExecute">
        ///     An optional observable that dictates the availability of the command for execution.
        /// </param>
        /// <param name="outputScheduler">
        ///     An optional scheduler that is used to surface events. Defaults to <c>RxApp.MainThreadScheduler</c>.
        /// </param>
        /// <returns>
        ///     The <c>ReactiveCommand</c> instance.
        /// </returns>
        public ReactiveCommand<Unit, Unit> CreateFromTask(
            Func<Task> execute,
            IObservable<bool> canExecute = null,
            IScheduler outputScheduler = null)
        {
            var command = ReactiveCommand.CreateFromTask(execute, canExecute, outputScheduler);
            var disposable = command.ThrownExceptions.Subscribe(ExceptionEvaluator.Evaluate);
            this._commandDisposables.Add(disposable);
            return command;
        }

        /// <summary>
        ///     Creates a <see cref="ReactiveCommand{TParam, TResult}" /> with asynchronous execution logic that takes a parameter
        ///     of type <typeparamref name="TParam" />.
        /// </summary>
        /// <param name="execute">
        ///     Provides a <see cref="Task" /> representing the command's asynchronous execution logic.
        /// </param>
        /// <param name="canExecute">
        ///     An optional observable that dictates the availability of the command for execution.
        /// </param>
        /// <param name="outputScheduler">
        ///     An optional scheduler that is used to surface events. Defaults to <c>RxApp.MainThreadScheduler</c>.
        /// </param>
        /// <returns>
        ///     The <c>ReactiveCommand</c> instance.
        /// </returns>
        /// <typeparam name="TParam">
        ///     The type of the parameter passed through to command execution.
        /// </typeparam>
        internal ReactiveCommand<TParam, Unit> CreateFromTask<TParam>(
            Func<TParam, Task> execute,
            IObservable<bool> canExecute = null,
            IScheduler outputScheduler = null)
        {
            var command = ReactiveCommand.CreateFromTask(execute, canExecute, outputScheduler);
            var disposable = command.ThrownExceptions.Subscribe(ExceptionEvaluator.Evaluate);
            this._commandDisposables.Add(disposable);
            return command;
        }

        /// <summary>
        ///     Creates a <see cref="ReactiveCommand{TParam, TResult}" /> with asynchronous, cancellable execution logic that takes
        ///     a parameter of type <typeparamref name="TParam" />.
        /// </summary>
        /// <param name="execute">
        ///     Provides a <see cref="Task" /> representing the command's asynchronous execution logic.
        /// </param>
        /// <param name="canExecute">
        ///     An optional observable that dictates the availability of the command for execution.
        /// </param>
        /// <param name="outputScheduler">
        ///     An optional scheduler that is used to surface events. Defaults to <c>RxApp.MainThreadScheduler</c>.
        /// </param>
        /// <returns>
        ///     The <c>ReactiveCommand</c> instance.
        /// </returns>
        /// <typeparam name="TParam">
        ///     The type of the parameter passed through to command execution.
        /// </typeparam>
        internal ReactiveCommand<TParam, Unit> CreateFromTask<TParam>(
            Func<TParam, CancellationToken, Task> execute,
            IObservable<bool> canExecute = null,
            IScheduler outputScheduler = null)
        {
            var command = ReactiveCommand.CreateFromTask(execute, canExecute, outputScheduler);
            var disposable = command.ThrownExceptions.Subscribe(ExceptionEvaluator.Evaluate);
            this._commandDisposables.Add(disposable);
            return command;
        }
    }
}