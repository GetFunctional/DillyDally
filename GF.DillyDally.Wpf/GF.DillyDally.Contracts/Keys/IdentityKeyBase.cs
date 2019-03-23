namespace GF.DillyDally.Contracts.Keys
{
    public abstract class IdentityKeyBase<T> : IIdentityKey<T>
    {
        #region Interface Implementations

        #region IIdentityKey<T> Members

        public abstract bool Equals(T other);

        #endregion

        #endregion

        #region - Methoden oeffentlich -

        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();

        public static bool operator ==(IdentityKeyBase<T> a, IdentityKeyBase<T> b)
        {
            return ReferenceEquals(a, b) || !ReferenceEquals(null, a) && a.Equals(b);
        }

        public static bool operator !=(IdentityKeyBase<T> a, IdentityKeyBase<T> b)
        {
            return !(ReferenceEquals(a, b) || !ReferenceEquals(null, a) && a.Equals(b));
        }

        #endregion
    }
}