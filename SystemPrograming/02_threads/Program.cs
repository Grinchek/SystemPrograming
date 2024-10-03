namespace _02_threads
{
    internal class Program
    {
        const string FilePath = @"C:\Users\User\Desktop\System_programing\SystemPrograming\02_threads\my_file.txt";
        static StreamWriter myFile = new StreamWriter(FilePath);
        //============================= 1 - 3 tasks =======================================
        //static void Method()
        //{
        //    for (int i = 0; i < 50; i++) 
        //    {
        //        Console.WriteLine(i);
        //        Thread.Sleep(350);
        //    }
        //}
        //static void ParamMethod(object a)
        //{
        //    Tuple<int,int> tuple= (Tuple<int,int>)a;
        //    for (int i = tuple.Item1; i < tuple.Item2; i++)
        //    {
        //        Console.WriteLine(i);
        //        Thread.Sleep(350);
        //    }
        //}
        //==================================================================================
        //======================= 4 task  ======================================================
        static void Maximum(object a)
        {
            List<int> list = (List<int>)a;
            string line = $"Maximum is: {list.Max()}\n";
            Console.WriteLine(line);
            
            myFile.Write(line);
           
        }
        static void Minimum(object a)
        {
            List<int> list = (List<int>)a;
            string line = $"Minimum is: {list.Min()}\n";
            Console.WriteLine(line);
            myFile.Write(line);
            
        }
        static void Average(object a)
        {
            List<int> list = (List<int>)a;
            string line = $"Average is: {list.Average()}\n";
            Console.WriteLine(line);
            myFile.Write(line);
        }
        static void ShowList(object a)
        {
            List<int> list = (List<int>)a;
            int idx = list.Count;
            for (int i=0;i<idx;i++)
            {
                Console.Write($"[{list[i]}]");
                if (i % 10 == 0)
                {
                    Console.WriteLine();
                }
                
            }
        }


        static void Main(string[] args)
        {
            //=========================== 1 - 3 tasks ==========================================
            //int start = 0;
            //int end = 0;
            //int threadsCount=0;

            //Console.WriteLine("Enter start of range: "); start =int.Parse( Console.ReadLine());

            //Console.WriteLine("Enter end of range: "); end = int.Parse(Console.ReadLine());
            //Tuple<int, int> range = new Tuple<int, int>(start, end);
            //Console.WriteLine("Enter amount of threads: ");threadsCount = int.Parse( Console.ReadLine());


            // Thread thread1 = new Thread(Method);

            //thread1.Start();
            //for (int i = 0;i < threadsCount;i++) 
            //{
            //    Thread thread2 = new Thread(ParamMethod);
            //    thread2.Start((object)range);
            //}
            //===================================================================================
            //=================== 4 - 5 tasks ================================================
            List<int> list= new List<int>();
     

            Random rand = new Random();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(rand.Next(9999));
            }
            Thread thread1 = new Thread(Maximum);
            Thread thread2 = new Thread(Minimum);
            Thread thread3 = new Thread(Average);
            Thread thread4 = new Thread(ShowList);
            try
            {
                thread1.Start((object)list);
                thread2.Start((object)list);
                thread3.Start((object)list);
                thread4.Start((object)list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                myFile.Close();
            }


            
            
        }
    }
}
