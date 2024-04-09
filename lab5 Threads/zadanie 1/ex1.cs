using System;
using System.Threading;
using System.Collections.Generic;

namespace lab5
{
    public class Exercise1
    {
        Random random = new Random();
        List<Producer> producers = new List<Producer>();
        List<Consumer> consumers = new List<Consumer>();
        List<int> data = new List<int>();
        int m;
        int n;

        public Exercise1(int n, int m)
        {
            this.n = n;
            this.m = m;
        }

        public void Start()
        {
            for (int i = 0; i < n; i++)
            {
                Producer producer = new Producer(i, data, random.Next(1000, 5000));
                producers.Add(producer);
                Thread producerThread = new Thread(new ThreadStart(producer.Start));
                producerThread.Start();
                Console.WriteLine($"Producer {i} started");
            }

            for (int j = 0; j < m; j++)
            {
                Consumer consumer = new Consumer(j, data, random.Next(1000, 5000));
                consumers.Add(consumer);
                Thread consumerThread = new Thread(new ThreadStart(consumer.Start));
                consumerThread.Start();
                Console.WriteLine($"Consumer {j} started");
            }

            Console.WriteLine("Press q to quit!");
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                Console.WriteLine("Press q to quit!");
            }
            Stop();
        }

        public void Stop()
        {
            foreach (var producer in producers)
            {
                producer.running = false;
                Console.WriteLine($"Producer {producer.number} stopped");
            }
            foreach (var consumer in consumers)
            {
                consumer.running = false;
                Console.WriteLine($"Consumer {consumer.number} stopped");
                if (consumer != null)
                {
                    consumer.PrintConsumerCounts();
                }
            }
        }
    }
}