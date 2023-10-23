using System;
using System.Globalization;

namespace CSharp.Events.Model
{
    /* Just make a new Stock and Change its price!
     * e.g. :
      Stock EGX30 = new Stock(30.15m, "EGX30");
            EGX30.UpdateStock(20m);
            EGX30.UpdateStock(30m);
     * Just copy this code and try it!
     */
    public class Stock
    {
        private decimal _price;
        private string _name;
        private delegate void EventHandler (Stock stock, decimal old_price);
        public static CultureInfo ci = new CultureInfo("en-eg");
        private event EventHandler PrintEvent;
        private void SubscribeEvent ( )
        {
            this.PrintEvent += (stock, old_price) =>
            {
                if (old_price < stock.Price)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (old_price > stock.Price)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(stock.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            };
        }
        public Stock (decimal price, string name)
        {
            this._price = price;
            this._name = name;
            SubscribeEvent();
        }
        public decimal Price => _price;
        public string Name => _name;

        public override string ToString ( )
        {
            return $"Stock Name: {Name} - Price: {Price.ToString("C2", ci)}";
        }
        public void UpdateStock (decimal price)
        {
            decimal old_price = _price;
            _price = price;
            PrintEvent?.Invoke(this, old_price);
        }
    }

}
