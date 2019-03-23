using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GF.DillyDally.Mvvmc
{
    /// <summary>
    ///     Klasse für die Implementation von INotifyPropertyChanged und INotifyPropertyChanging
    ///     Die beiden Event-Methoden werden deklariert, sowie eine Hilfsmethode SetField um den
    ///     Wert einer Eigenschaft zu setzen.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        /// <summary>
        ///     Setzt den Wert einer Eigenschaft. Prüft, ob sich der Wert geändert hat und ruft in diesem Fall die
        ///     Notification-Events auf.
        /// </summary>
        /// <typeparam name="T">Eigenschaften-Typ</typeparam>
        /// <param name="field">Feld der Eigenschaft</param>
        /// <param name="value">Neuer Wert der Eigenschaft</param>
        /// <param name="propertyName">Name der Eigenschaft (wird ab C# 5 vom Compiler gesetzt)</param>
        /// <param name="changedCallback">
        ///     Callback der nach dem Setzen der Eigenschaften aber noch vor dem return der Methode
        ///     ausgeführt wird.
        /// </param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName, Action changedCallback)
        {
            if (this.AreEqual(ref field, value))
            {
                return false;
            }

            // ReSharper disable once ExplicitCallerInfoArgument
            this.RaisePropertyChanging(propertyName);
            field = value;

            // ReSharper disable once ExplicitCallerInfoArgument
            this.RaisePropertyChanged(propertyName);

            if (changedCallback != null)
            {
                changedCallback();
            }

            return true;
        }

        protected bool SetField<T>(ref T field, T value, Expression<Func<T>> expression, Action changedCallback)
        {
            return this.SetField(ref field, value, ObservableObjectExtensions.GetPropertyName(expression),
                changedCallback);
        }

        protected bool SetField<T>(ref T field, T value, Expression<Func<T>> expression)
        {
            return this.SetField(ref field, value, expression, null);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            return this.SetField(ref field, value, propertyName, null);
        }

        protected bool AreEqual<T>(ref T field, T value)
        {
            return EqualityComparer<T>.Default.Equals(field, value);
        }

        protected bool AreEqual<T>(ref T field, T value, IEqualityComparer<T> comparer)
        {
            return comparer.Equals(field, value);
        }

        protected void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        protected void RaisePropertiesChanging(params string[] propertyNames)
        {
            if (propertyNames == null || propertyNames.Length == 0)
            {
                this.RaisePropertyChanging(string.Empty);
                return;
            }

            foreach (var propertyName in propertyNames)
            {
                this.RaisePropertyChanging(propertyName);
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertiesChanged(params string[] propertyNames)
        {
            if (propertyNames == null || propertyNames.Length == 0)
            {
                this.RaisePropertyChanged(string.Empty);
                return;
            }

            foreach (var propertyName in propertyNames)
            {
                this.RaisePropertyChanged(propertyName);
            }
        }
    }
}