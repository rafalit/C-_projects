using System;
namespace lab
{
    class Regions
    {
        public string regionId{get; set;}
        public string regionDescription{get; set;}

        public static Regions MapCsvToModel(string[] values)
        {
            return new Regions
            {
                regionId = values[0],
                regionDescription = values[1]
            };
        }
    }
}