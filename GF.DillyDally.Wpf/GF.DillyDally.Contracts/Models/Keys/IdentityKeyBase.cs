namespace GF.DillyDally.Contracts
{
    public abstract class IdentityKeyBase<T> : IIdentityKey<T>
    {
        #region IIdentityKey<T> Members

        public abstract bool Equals(T other);

        #endregion

        #region - Methoden oeffentlich -

        public static bool IsNull(IIdentityKey<T> src)
        {
            return src == null;
        }

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