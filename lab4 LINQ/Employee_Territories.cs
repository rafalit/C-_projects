using System;
namespace lab
{
    class Employee_Territories
    {
        public string employeeId{get;set;}
        public string territoryId{get; set;}

        public static Employee_Territories MapCsvToModel(string[] values)
        {
            return new Employee_Territories
            {
                employeeId = values[0],
                territoryId = values[1]
            };
        }
    }
}