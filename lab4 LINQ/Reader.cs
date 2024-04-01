using System;
using System.Collections.Generic;
using System.IO;

namespace lab
{
    class Reader<T>
    {
        public List<T> readList(String path, Func<String[], T> generuj)
        {
            List<T> resultList = new List<T>();

            string[] lines = File.ReadAllLines(path).Skip(1).ToArray();

            foreach(var line in lines)
            {
                string [] values = line.Split(',');
                T obj = generuj(values);
                resultList.Add(obj);
            }
            return resultList;
        }
    }
}