using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace FileSearch
{
    public class Search
    {
        string dirPath;
        string searchStr;

        public Search(string dirPath, string searchStr)
        {
            this.dirPath = dirPath;
            this.searchStr = searchStr;
        }

        public void Begin()
        {
            Thread newThread = new Thread(() => SearchFiles(dirPath, searchStr));
            newThread.Start();

            Console.WriteLine("Searching...");
            Console.ReadLine();
        }

        public void SearchFiles(string dirPath, string searchStr)
        {
            foreach(string filePath in Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories))
            {
                if(Path.GetFileName(filePath).Contains(searchStr))
                {
                    Console.WriteLine(filePath);
                }
            }
        }
    }
}