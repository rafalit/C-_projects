﻿using System;
using MultiThread;

class Program
{
    void ex4()
    {
        Manager manager = new Manager(10);
        manager.Start();
    }

    public static void Main(string [] args)
    {
        Program program = new Program();
        program.ex4();
    }
}