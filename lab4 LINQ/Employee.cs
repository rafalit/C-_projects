using System;

namespace lab
{
    class Employee
    {
        public string employeeId{get; set;}
        public string lastname{get; set;}
        public string firstname{get; set;}
        public string title{get; set;}
        public string titleOfCourtesy{get; set;}
        public string birthdate{get; set;}
        public string hiredate{get; set;}
        public string address{get; set;}
        public string city{get; set;}
        public string region{get; set;}
        public string postalCode{get; set;}
        public string country{get; set;}
        public string homephone{get; set;}
        public string extension{get; set;}
        public string photo{get; set;}
        public string notes{get; set;}
        public string reportsto{get; set;}
        public string photopath{get; set;}

        public static Employee MapCsvToModel(string[] values)
        {
            return new Employee
            {
                employeeId = values[0],
                    lastname = values[1],
                    firstname = values[2],
                    title = values[3],
                    titleOfCourtesy = values[4],
                    birthdate = values[5],
                    hiredate = values[6],
                    address = values[7],
                    city = values[8],
                    region = values[9],
                    postalCode = values[10],
                    country = values[11],
                    homephone = values[12],
                    extension = values[13],
                    photo = values[14],
                    notes = values[15],
                    reportsto = values[16],
                    photopath = values[17]
            };
        }
    }
}