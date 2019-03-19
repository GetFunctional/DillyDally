using System;
using GF.DillyDally.Contracts.RewardSystem.Models;

namespace GF.DillyDally.Contracts.RewardSystem
{
    public class CurrencyFactory
    {
        public Currency CreateGameTime()
        {
            var gameTimeId = Guid.Parse("{67AD9C5E-3EE2-462B-B24B-1E220B87B8F4}");
            var currencyName = "Gametime (minutes)";
            var shortTerm = "min.";
            return new Currency(gameTimeId, currencyName, shortTerm);
        }

        public Currency CreateDayOff()
        {
            var dayOffId = Guid.Parse("{639343E8-7F9F-462F-95B7-FBE0601F4FFE}");
            var currencyName = "Day off";
            var shortTerm = "XXX";
            return new Currency(dayOffId, currencyName, shortTerm);
        }

        public Currency CreateGameCredit()
        {
            var gameCreditId = Guid.Parse("{A5FB572C-9048-4239-A251-7D6EEAC018BA}");
            var currencyName = "Gamecredits";
            var shortTerm = "€";
            return new Currency(gameCreditId, currencyName, shortTerm);
        }

        public Currency CreateSweet()
        {
            var sweetsId = Guid.Parse("{1A929DBF-E6CE-4DE5-A86A-DD3E232549AD}");
            var currencyName = "Small sweets";
            var shortTerm = "sw";
            return new Currency(sweetsId, currencyName, shortTerm);
        }

        public Currency CreateBigSweet()
        {
            var sweetsId = Guid.Parse("{CCC28ED1-9001-4447-99D2-061E2274AB50}");
            var currencyName = "Big sweets";
            var shortTerm = "bsw";
            return new Currency(sweetsId, currencyName, shortTerm);
        }

        public Currency CreateHS()
        {
            var sweetsId = Guid.Parse("{69C95C92-E2B2-4628-A562-961E5C7B6BE0}");
            var currencyName = "HS Matches";
            var shortTerm = "Hsm";
            return new Currency(sweetsId, currencyName, shortTerm);
        }

        public Currency CreateCalories()
        {
            var sweetsId = Guid.Parse("{BA2B82E3-E9D4-44FF-8109-6EDB2DCD910F}");
            var currencyName = "Extra Kalorien";
            var shortTerm = "kcal";
            return new Currency(sweetsId, currencyName, shortTerm);
        }

        public Currency CreateGoldForPacks()
        {
            var sweetsId = Guid.Parse("{1AA86CF0-D71D-427A-93D4-0EA27D614276}");
            var currencyName = "Gold for Packs";
            var shortTerm = "Gold";
            return new Currency(sweetsId, currencyName, shortTerm);
        }
    }
}