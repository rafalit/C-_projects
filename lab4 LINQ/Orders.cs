using System;

namespace lab
{
    class Orders
    {
        public string orderId{get; set;}
        public string customerId{get; set;}
        public string employeeId{get; set;}
        public string orderDate{get; set;}
        public string requiredDate{get; set;}
        public string shippedDate{get; set;}
        public string shipVia{get; set;}
        public string freight{get; set;}
        public string shipName{get; set;}
        public string shipAddress{get; set;}
        public string shipCity{get; set;}
        public string shipRegion{get; set;}
        public string shipPostalCode{get; set;}
        public string shipCountry{get;set;}

        public static Orders MapCsvToModel(string[] values)
        {
            return new Orders
            {
                orderId = values[0],
                customerId = values[1],
                employeeId = values[2],
                orderDate = values[3],
                requiredDate = values[4],
                shippedDate = values[5],
                shipVia = values[6],
                freight = values[7],
                shipName = values[8],
                shipAddress = values[9],
                shipCity = values[10],
                shipRegion = values[11],
                shipPostalCode = values[12],
                shipCountry = values[13],
            };
        }
    }
}