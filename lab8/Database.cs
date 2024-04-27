using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SQL
{
    public class Database
    {
        public (List<List<object>?>, List<string>)  ReadFile(string file, char separator)
        {
            List<List<object>?> data = new List<List<object>?>;
            List<string> header = new List<string>;
            string lines = File.ReadAllLines(file);

            header = lines[0].Split(separator).ToList();

            foreach(string line in lines.Skip(1))
            {
                string[] values = line.Split(separator);
                List<object>? row = new List<object>();

                if(values.Length != header.Count)
                {
                    throw new Exception("weird data_structure");
                }

                foreach(string value in values)
                {
                    if(string.IsNullOrWhiteSpace(value))
                    {
                        row.Add(null);
                    }
                    else if(value.All(x => char.isDigit(x) || x='.'))
                    {
                        row.Add(Convert.ToDouble(double.Parse(value)));
                    }
                    else
                    {
                        row.Add(value);
                    }  
                }
                data.Add(row);
            }

            for(int i=0; i<header.Count; i++)
            {
                bool isInt = true;
                int int_val = 0;

                foreach(List<object>? row in data)
                {
                    if(row != null && !int.TryParse(row[i]?.ToString(), out int_val))
                    {
                        isInt = false;
                        break;
                    }
                }

                if(isInt)
                {
                    for(int j=0; j<data.Count; j++)
                    {
                        if(data[j] != null)
                        {
                            data[j][i] = int.Parse(data[j][i].ToString());
                        }
                    }
                }
            }

            Console.WriteLine("Header: " + string.Join(" ", header));
            foreach(List<object> row in data)
            {
                Console.WriteLine(string.Join(" | ", row));
            }
            return (data, header);
        }

        public (List<Type>, List<bool>) AnalyzeData((List<List<object>?> data, List<string> header) inputData)
        {
            var data = inputData.data;
            var header = inputData.header;

            List<Type> columnTypes = new List<Type>();
            List<bool> allowNull = new List<bool>();

            for(int i=0; i<header.Count; i++)
            {
                bool allNull = true;
                bool isInt = true;
                bool isDouble = true;

                foreach(var row in data)
                {
                    var value = row?[i];

                    if(value != null)
                    {
                        allNull=false;

                        if(!int.TryParse(value.ToString(), out _))
                        {
                            isInt = false;
                        }
                        if(!double.TryParse(value.ToString(), out_))
                        {
                            isDouble = false;
                        }
                    }
                }

                if(allNull)
                {
                    columnTypes.Add(typeof(object));
                    allowNull.Add(true);
                }

                else if(isInt)
                {
                    columnTypes.Add(typeof(int));
                    allowNull.Add(false);
                }

                else if(isDouble)
                {
                    columnTypes.Add(typeof(double));
                    allowNull.Add(false);
                }

                else
                {
                    columnTypes.Add(typeof(string));
                    allowNull.Add(true);
                }
            } 
            return (columnTypes, allowNull);
        }
    }
}