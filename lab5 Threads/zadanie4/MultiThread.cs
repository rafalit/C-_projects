using System;
using System.Threading;

namespace MultiThread
{
    public class Worker
    {
        private Manager manager;
        public Worker(Manager manager)
        {
            this.manager = manager;
        }

        public void Start()
        {
            Interlocked.Increment(ref manager.active_threads);
            manager.EWHworker.WaitOne();
            Interlocked.Decrement(ref manager.active_threads);
            manager.EWHManager.Set();
        }
    }

    public class Manager
    {
        public long active_threads;
        public long all_threads;
        public EventWaitHandle EWHworker;
        public EventWaitHandle EWHManager;

        public Manager(long all_threads)
        {
            this.all_threads=all_threads;
            this.active_threads=0;
            this.EWHManager = new EventWaitHandle(false, EventResetMode.AutoReset);
            this.EWHworker = new EventWaitHandle(false, EventResetMode.AutoReset);
        } 

        public void Start()
        {
            for(var i=0; i<all_threads; i++)
            {
                new Thread(new Worker(this).Start).Start();
            }

            while(Interlocked.Read(ref active_threads) != all_threads)
            {
                Thread.Sleep(1000);
            }

            System.Console.WriteLine("All threads started!");

            while(Interlocked.Read(ref active_threads) > 0)
            {
                WaitHandle.SignalAndWait(EWHworker, EWHManager);
                Console.WriteLine("blah blah " + active_threads);
            }

            System.Console.WriteLine("All threads finished!");
        }
    }
}