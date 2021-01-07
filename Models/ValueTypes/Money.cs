using System;
using RegistroServizi.Models.Enums;
using RegistroServizi.Models.Exceptions.Infrastructure;

namespace RegistroServizi.Models.ValueTypes
{
    public class Money
    {
        public Money() : this(Currency.EUR, 0.00m)
        {
        }
        public Money(Currency currency, decimal amount)
        {
            Amount = amount;
            Currency = currency;
        }
        private decimal amount = 0;
        public decimal Amount
        { 
            get
            {
                return amount;
            }
            set
            {
                if (value < 0) {
                    throw new InvalidAmountException();
                }
                amount = value;
            }
        }
        public Currency Currency
        {
            get; set;
        }

        public override bool Equals(object obj)
        {
            var money = obj as Money;
            return money != null &&
                   Amount == money.Amount &&
                   Currency == money.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }
        
        public override string ToString()
        {
            return $"{Currency} {Amount:0.00}";
        }
    }
}