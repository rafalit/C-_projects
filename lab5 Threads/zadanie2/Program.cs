using System;
using System.Threading;

using FileWatcher;

class Program
{
    void ex2()
    {
        FileWatch fileWatcher = new FileWatch("./");
        fileWatcher.Start();
    }

    public static void Main(string[] args)
    {
        Program p = new Program();
        p.ex2();
    }
}