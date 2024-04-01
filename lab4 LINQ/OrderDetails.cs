using System;
using System.Globalization;

namespace lab
{
    class OrderDetails
    {
        public string orderId{get; set;}
        public string productId{get; set;}
        public double unitPrice{get; set;}
        public double quantity{get; set;}
        public double discount{get; set;} 

        public static OrderDetails MapCsvToModel(string[] values)
        {
            return new OrderDetails
            {
                orderId = values[0],
                productId = values[1],
                unitPrice = double.Parse(values[2], CultureInfo.InvariantCulture),
                quantity = double.Parse(values[3], CultureInfo.InvariantCulture),
                discount = double.Parse(values[4], CultureInfo.InvariantCulture)
            };
        }
    }
}