using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace lab
{
    class Program
    {
        public static void SurPrinter(List<Employee> employees)
        {
            Console.WriteLine("Surname");
            foreach (Employee emp in employees)
            {
                Console.WriteLine(emp.lastname);
            }
            Console.WriteLine("");
        }

        public static void SurRegTerPrinter(List<Employee> employees, List<Employee_Territories> emp_ter, List<Territories> ter, List<Regions> reg)
        {
            var results = from e in employees
                          join e_t in emp_ter on e.employeeId equals e_t.employeeId
                          join t in ter on e_t.territoryId equals t.territoryId
                          join r in reg on t.regionId equals r.regionId
                          select new
                          {
                              surname = e.lastname,
                              region = r.regionDescription,
                              territory = t.territoryDescription
                          };

            Console.WriteLine("{0,-15}{1,-20}{2,-25}", "lastname", "regiondescription", "territorydescription");
            foreach (var e in results)
            {
                Console.WriteLine($"{e.surname,-15}{e.region,-20}{e.territory,-25}");
            }
            Console.WriteLine("");
        }

        public static void RegEmpPrinter(List<Employee> employees, List<Employee_Territories> emp_ter, List<Territories> ter, List<Regions> regions)
        {
            var results = from r2 in (from r in regions
                                      join t in ter on r.regionId equals t.regionId
                                      join e_t in emp_ter on t.territoryId equals e_t.territoryId
                                      join e in employees on e_t.employeeId equals e.employeeId
                                      select new { reg = r, emp = e })
                          group r2 by r2.reg.regionDescription into grouped
                          select (grouped);

            foreach (var et in results)
            {
                Console.WriteLine("{0}:", et.Key);
                foreach (var pr in et)
                {
                    Console.WriteLine("  {0}", pr.emp.lastname);
                }
            }
            Console.WriteLine("");
        }

        public static void RegCountPrinter(List<Employee> employees, List<Employee_Territories> emp_ter, List<Territories> ter, List<Regions> regions)
        {
            var results = from r2 in (from r in regions
                                      join t in ter on r.regionId equals t.regionId
                                      join e_t in emp_ter on t.territoryId equals e_t.territoryId
                                      join e in employees on e_t.employeeId equals e.employeeId
                                      select new { reg = r.regionDescription, emp = e.employeeId })
                          group r2 by r2.reg into grouped
                          select new
                          {
                              reg = grouped.Key,
                              count = grouped.Count()
                          };

            Console.WriteLine("{0,-20}{1,-10}", "region", "count");
            foreach (var res in results)
            {
                Console.WriteLine($"{res.reg,-20}{res.count,-10}");
            }
            Console.WriteLine("");
        }

        public static void EmpPurchasesPrinter(List<Orders> orders, List<OrderDetails> order_details)
        {
            var results = from r2 in (from o in orders
                                      join order_det in order_details on o.orderId equals order_det.orderId
                                      select new
                                      {
                                          emp = o.employeeId,
                                          price = order_det.unitPrice,
                                          disc = order_det.discount,
                                          quantity = order_det.quantity
                                      })
                          group r2 by r2.emp into grouped
                          select new
                          {
                              emp_id = grouped.Key,
                              count = grouped.Count(),
                              avg = Math.Round(grouped.Average(gr => gr.price * gr.quantity - gr.disc), 2),
                              max = Math.Round(grouped.Max(gr => gr.price * gr.quantity - gr.disc), 2)
                          };
            Console.WriteLine("{0,-10}{1,-10}{2,-20}{3,-20}", "employee", "count", "average_purchase", "max_purchase");
            foreach (var res in results)
            {
                Console.WriteLine($"{res.emp_id,-10}{res.count,-10}{res.avg.ToString("F2"),-20} {res.max.ToString("F2"),-20}");
            }

        }

        static void Main(string[] args)
        {
            List<Employee> employee = new Reader<Employee>().readList("data/employees.csv", Employee.MapCsvToModel);

            List<Employee_Territories> employee_territories = new Reader<Employee_Territories>().readList("data/employee_territories.csv", Employee_Territories.MapCsvToModel);

            List<OrderDetails> orderDetails = new Reader<OrderDetails>().readList("data/orders_details.csv", OrderDetails.MapCsvToModel);

            List<Territories> territories = new Reader<Territories>().readList("data/territories.csv", Territories.MapCsvToModel);

            List<Regions> regions = new Reader<Regions>().readList("data/regions.csv", Regions.MapCsvToModel);
                
            List<Orders> orders = new Reader<Orders>().readList("data/orders.csv", Orders.MapCsvToModel);
             
            SurPrinter(employee);
            SurRegTerPrinter(employee, employee_territories, territories, regions);
            RegEmpPrinter(employee, employee_territories, territories, regions);
            RegCountPrinter(employee, employee_territories, territories, regions);
            EmpPurchasesPrinter(orders, orderDetails);
        }
    }
}