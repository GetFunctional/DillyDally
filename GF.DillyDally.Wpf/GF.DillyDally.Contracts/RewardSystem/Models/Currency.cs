﻿using System;

namespace GF.DillyDally.Contracts.RewardSystem.Models
{
    public class Currency : IEquatable<Currency>
    {
        public Currency(Guid currencyId, string name, string kuerzel)
        {
            this.CurrencyId = currencyId;
            this.Name = name;
            this.Kuerzel = kuerzel;
        }

        public Guid CurrencyId { get; }

        public string Name { get; }

        public string Kuerzel { get; }

        public bool Equals(Currency other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.CurrencyId.Equals(other.CurrencyId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Currency) obj);
        }

        public override int GetHashCode()
        {
            return this.CurrencyId.GetHashCode();
        }
    }
}