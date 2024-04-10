using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class FileWatch
    {
        string path;
        Thread thread;

        public FileWatch(string path)
        {
            this.path=path;
        }

        public void Start()
        {
            Console.WriteLine($"Directory: {path}");

            var watcher = new FileSystemWatcher(path);
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = false;
            watcher.Created += (sender, e) => Console.WriteLine($"Created: {e.Name}");
            watcher.Deleted += (sender, e) => Console.WriteLine($"Deleted: {e.Name}");
            watcher.Changed += (sender, e) => Console.WriteLine($"Changed: {e.Name}");
            watcher.Renamed += (sender, e) => Console.WriteLine($"Renamed: {e.OldName} -> {e.Name}");

            Thread thread = new Thread(() =>
            {
                Console.WriteLine("here");
                while(true)
                {
                    Console.WriteLine("Press q to quit!");
                    var input = Console.ReadKey();

                    if(input.KeyChar == 'q')
                    {
                        watcher.EnableRaisingEvents = false;
                        watcher.Dispose();
                        break;
                    }
                }
            Console.WriteLine("Stopped monitoring directory!");
            });

            thread.Start();
        }
    }
}