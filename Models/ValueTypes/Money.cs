using RegistroServizi.Models.Enums;
using RegistroServizi.Models.Exceptions.Infrastructure;
namespace RegistroServizi.Models.ValueTypes
{
    public record Money
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
            init
            {
                if (value < 0) {
                    throw new InvalidAmountException();
                }
                amount = value;
            }
        }
        public Currency Currency
        {
            get; init;
        }

        public override string ToString()
        {
            return $"{Currency} {Amount:0.00}";
        }
    }
}