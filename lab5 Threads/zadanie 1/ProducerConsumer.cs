using System;
using System.Threading;
using System.Collections.Generic;


namespace lab5
{
    public class ProducerConsumer
    {
        public int number;
        public List<int> data { get; set; }
        public Thread thread = null;
        public int sleepTime { get; set; }

        protected ProducerConsumer(int number, List<int> data, int sleepTime)
        {
            this.number = number;
            this.data = data;
            this.sleepTime = sleepTime;
        }
    }

    public class Producer : ProducerConsumer
    {
        public bool running = true;
        Random random = new Random();
        public Producer(int number, List<int> data, int sleepTime) : base(number, data, sleepTime) { }

        public void Start()
        {
            while (running)
            {
                Thread.Sleep(sleepTime);
                int producedData = (number);
                lock (data)
                {
                    this.data.Add(producedData);
                }
            }
        }
    }

    public class Consumer : ProducerConsumer
    {
        public bool running { get; set; }
        Random random = new Random();
        public Dictionary<int, Dictionary<int, int>> consumedCounts { get; set; }

        public Consumer(int number, List<int> data, int sleepTime) : base(number, data, sleepTime)
        {
            consumedCounts = new Dictionary<int, Dictionary<int, int>>();
            running = true;
        }

        public void Start()
        {
            while (running)
            {
                Thread.Sleep(random.Next(1000, 5000));
                int consumedData;
                lock (data)
                {
                    if (data.Count > 1)
                    {
                        consumedData = data[0];
                        data.RemoveAt(0);
                        int producerNumber = consumedData % data.Count;

                        Console.WriteLine($"PR {producerNumber} - CO {number}, DA {consumedData}");

                        if (!consumedCounts.ContainsKey(producerNumber))
                        {
                            consumedCounts[producerNumber] = new Dictionary<int, int>();
                        }

                        if (consumedCounts[producerNumber].ContainsKey(number))
                        {
                            consumedCounts[producerNumber][number]++;
                        }
                        else
                        {
                            consumedCounts[producerNumber][number] = 1;
                        }
                    }
                }
            }
        }

        public void PrintConsumerCounts()
        {
            foreach (var producer in consumedCounts.Keys)
            {
                foreach (var consumerCounts in consumedCounts[producer])
                {
                    int count = consumerCounts.Value;
                    if (!consumedCounts[producer].ContainsKey(consumerCounts.Key))
                    {
                        count = 0;
                    }
                    Console.WriteLine($"Consumer {consumerCounts.Key} -> Producer {producer}: {count}");
                }
            }
        }
    }
}