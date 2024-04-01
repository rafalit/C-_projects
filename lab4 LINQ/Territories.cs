using System;
namespace lab 
{
    class Territories
    {
        public string territoryId{get;set;}
        public string territoryDescription{get;set;}
        public string regionId{get; set;}

        public static Territories MapCsvToModel(string[] values)
        {
            return new Territories
            {
                territoryId = values[0],
                territoryDescription = values[1],
                regionId = values[2]
            };
        }
    }
}