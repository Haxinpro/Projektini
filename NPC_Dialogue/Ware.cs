using System;

namespace Game
{
    public class Ware
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        // Järjestyksessä: Nimi, hinta, määrä.
        public Ware(string name, int price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}