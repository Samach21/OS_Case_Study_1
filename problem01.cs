using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Problem01
{
    class Program
    {
        static byte[] Data_Global = new byte[1000000000];
        static long[] Sum_Global = new long[12];
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
        static void sum(int threadIndex, int index)
        {
            if (Data_Global[index] % 2 == 0)
            {
                Sum_Global[threadIndex] -= Data_Global[index];
            }
            else if (Data_Global[index] % 3 == 0)
            {
                Sum_Global[threadIndex] += (Data_Global[index]*2);
            }
            else if (Data_Global[index] % 5 == 0)
            {
                Sum_Global[threadIndex] += (Data_Global[index] / 2);
            }
            else if (Data_Global[index] %7 == 0)
            {
                Sum_Global[threadIndex] += (Data_Global[index] / 3);
            }
            Data_Global[index] = 0;
        }
        static void Thread1()  
        {
            for (int i = 0; i < 83333333; i++)
            {
                sum(0, i);
            }
        }
        static void Thread2()  
        {
            for (int i = 83333333; i < 166666666; i++)
            {
                sum(1, i);
            }
        }
        static void Thread3()  
        {
            for (int i = 166666666; i < 250000000; i++)
            {
                sum(2, i);
            }
        }
        static void Thread4()  
        {
            for (int i = 250000000; i < 333333333; i++)
            {
                sum(3, i);
            }
        }
        static void Thread5()  
        {
            for (int i = 333333333; i < 416666666; i++)
            {
                sum(4, i);
            }
        }
        static void Thread6()  
        {
            for (int i = 416666666; i < 500000000; i++)
            {
                sum(5,  i);
            }
        }
        static void Thread7()  
        {
            for (int i = 500000000; i < 583333333; i++)
            {
                sum(6, i);
            }
        }
        static void Thread8()  
        {
            for (int i = 583333333; i < 666666666; i++)
            {
                sum(7, i);
            }
        }
        static void Thread9()  
        {
            for (int i = 666666666; i < 750000000; i++)
            {
                sum(8, i);
            }
        }
        static void Thread10()  
        {
            for (int i = 750000000; i < 833333333; i++)
            {
                sum(9, i);
            }
        }
        static void Thread11()  
        {
            for (int i = 833333333; i < 916666666; i++)
            {
                sum(10, i);
            }
        }
        static void Thread12()  
        {
            for (int i = 916666666; i < 1000000000; i++)
            {
                sum(11, i);
            }
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int y;
            Thread th1 = new Thread(Thread1);
            Thread th2 = new Thread(Thread2);
            Thread th3 = new Thread(Thread3);
            Thread th4 = new Thread(Thread4);
            Thread th5 = new Thread(Thread5);
            Thread th6 = new Thread(Thread6);
            Thread th7 = new Thread(Thread7);
            Thread th8 = new Thread(Thread8);
            Thread th9 = new Thread(Thread9);
            Thread th10 = new Thread(Thread10);
            Thread th11 = new Thread(Thread11);
            Thread th12 = new Thread(Thread12);

            for (int i = 0; i < 12; i++)
            {
                Sum_Global[i] =  0;
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
            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();
            th5.Start();
            th6.Start();
            th7.Start();
            th8.Start();
            th9.Start();
            th10.Start();
            th11.Start();
            th12.Start();
            th1.Join();
            th2.Join();
            th3.Join();
            th4.Join();
            th5.Join();
            th6.Join();
            th7.Join();
            th8.Join();
            th9.Join();
            th10.Join();
            th11.Join();
            th12.Join();
            sw.Stop();
            Console.WriteLine("Done.");

            /* Result */
            Console.WriteLine("Summation result: {0}", Sum_Global.Sum());
            Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
