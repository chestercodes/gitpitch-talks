namespace Downloader.Api.Other
{
    public class PersonAmount
    {
        public PersonAmount(string name, decimal amount)
        {
            this.Name = name;
            this.Amount = amount;
        }

        public string Name { get; }
        public decimal Amount { get; }
    }
}