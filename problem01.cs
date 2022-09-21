using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Problem01
{
    class Program
    {
        const int dataSize = 1000000000;
        const int threadSize = 12;
        const double step = dataSize/threadSize;
        static byte[] Data_Global = new byte[dataSize];
        static long Sum_Global = 0;
        static long[] Pre_Sum_Global = new long[threadSize];
        static int G_index = 0;

        static int ReadData()
        {
            int returnData = 0;
            FileStream fs = new FileStream("Problem01.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            try 
            {
                Data_Global = (byte[]) bf.Deserialize(fs);
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Read Failed:" + se.Message);
                returnData = 1;
            }
            finally
            {
                fs.Close();
            }

            return returnData;
        }
        // static void sum(int threadIndex, int index)
        // {
        //     if (Data_Global[index] % 2 == 0)
        //     {
        //         Pre_Sum_Global[threadIndex] -= Data_Global[index];
        //     }
        //     else if (Data_Global[index] % 3 == 0)
        //     {
        //         Pre_Sum_Global[threadIndex] += (Data_Global[index] * 2);
        //     }
        //     else if (Data_Global[index] % 5 == 0)
        //     {
        //         Pre_Sum_Global[threadIndex] += (Data_Global[index] / 2);
        //     }
        //     else if (Data_Global[index] % 7 == 0)
        //     {
        //         Pre_Sum_Global[threadIndex] += (Data_Global[index] / 3);
        //     }
        //     Data_Global[index] = 0;
        // }
        static void TestThread(int threadIndex)  
        {
            int start = Convert.ToInt32(threadIndex * step);
            int stop = Convert.ToInt32((threadIndex + 1) * step);
            if (threadIndex == threadSize - 1)
            {
                stop = dataSize;
            }
            long sum = 0;
            Span<byte> data = Data_Global;
            for (int i = start; i < stop; i++)
            {
                if (data[i] % 2 == 0)
                {
                    sum -= data[i];
                }
                else if (data[i] % 3 == 0)
                {
                    sum += (data[i] * 2);
                }
                else if (data[i] % 5 == 0)
                {
                    sum += (data[i] / 2);
                }
                else if (data[i] % 7 == 0)
                {
                    sum += (data[i] / 3);
                }
            }
            Sum_Global += sum;
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int y;
            Thread[] th = new Thread[threadSize];
            
            for (int i = 0; i < threadSize; i++)
            {
                int localNum = i;
                th[i] = new Thread(() => TestThread(localNum));
                th[i].Priority = ThreadPriority.Highest;
            }

            for (int i = 0; i < threadSize; i++)
            {
                Pre_Sum_Global[i] =  0;
            }

            /* Read data from file */
            Console.Write("Data read...");
            y = ReadData();
            if (y == 0)
            {
                Console.WriteLine("Complete.");
            }
            else
            {
                Console.WriteLine("Read Failed!");
            }

            /* Start */
            Console.Write("\n\nWorking...");
            sw.Start();
            for (int i = 0; i < threadSize; i++)
            {
                th[i].Start();
            }
            for (int i = 0; i < threadSize; i++)
            {
                th[i].Join();
            }
            // Sum_Global = Pre_Sum_Global.Sum();
            sw.Stop();
            Console.WriteLine("Done.");

            /* Result */
            Console.WriteLine("Summation result: {0}", Sum_Global);
            Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
