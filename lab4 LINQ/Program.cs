using System;
using System.Collections.Generic;
using System.IO;

namespace lab
{
    class Program
    {
        public static void SurPrinter(List<Employee> employees)
        {
            foreach(Employee emp in employees)
            {
                Console.WriteLine(emp.lastname);
            }
        }

        static void Main(string[] args)
        {
            Reader<Territories> territoriesReader = new Reader<Territories>();
            List<Territories> territories = territoriesReader.readList("data\\territories.csv",
                values => new Territories
                {
                    territoryId = values[0],
                    territoryDescription = values[1],
                    regionId = values[2]
                });

                Reader<Employee_Territories> employee_territoriesReader = new Reader<Employee_Territories>();
            List<Employee_Territories> employee_territories = employee_territoriesReader.readList("data\\employee_territories.csv",
                values => new Employee_Territories
                {
                    employeeId = values[0],
                    territoryId = values[1]
                });

                Reader<Regions> regionsReader = new Reader<Regions>();
            List<Regions> regions = regionsReader.readList("data\\regions.csv",
                values => new Regions
                {
                    regionId = values[0],
                    regionDescription = values[1]
                });

                 Reader<OrderDetails> orderDetailsReader = new Reader<OrderDetails>();
            List<OrderDetails> orderDetails = orderDetailsReader.readList("data\\orders_details.csv",
                values => new OrderDetails
                {
                    orderId = values[0],
                    productId = values[1],
                    unitPrice = values[2],
                    quantity = values[3],
                    discount = values[4]
                });

                Reader<Employee> employeeReader = new Reader<Employee>();
            List<Employee> employee = employeeReader.readList("data\\employees.csv",
                values => new Employee
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
                });

                SurPrinter(employee);
        }
    }
}