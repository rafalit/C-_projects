using System;
using FileSearch;

class Program
{
    void ex3()
    {
        Search search = new Search("./files", "Demons");
        search.Begin();
    }

    public static void Main()
    {
        Program program = new Program();
        program.ex3();
    }
}