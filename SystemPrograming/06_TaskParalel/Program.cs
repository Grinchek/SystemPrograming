using System.Threading.Channels;

namespace _06_TaskParalel
{
    internal class Program
    {
        static int Factorial(int num)
        {
            int result = 1;
            for (int i = 1; i <= num; i++) 
            {
                result *= i;
            }
            return result;
        }
        static void FactorialVoid(int num)
        {
            int result = 1;
            for (int i = 1; i <= num; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Factorial {num}: {result}");

        }
        static int FactorialCounter(int num)
        {            
            string factorial=Factorial(num).ToString();
            int result = factorial.Count();
            return result;

        }
        static int FactorialMembersSumma(int num)
        {
            List<int> members = new List<int>();
            string factorial = Factorial(num).ToString();
            foreach (char member in factorial) 
            {
                members.Add(int.Parse(member.ToString()));
            }
            return members.Sum();
        }
        static void MultiplicationTable(int num) 
        {
            for (int i = 1;i <= 10; i++)
            {
                Console.WriteLine($"{i} * {num} = {i*num}");
            }
            Console.WriteLine("///////////////////////////////////////////////");

        }
        static void Main(string[] args)
        {
            // # 1 - 2
            //int number = 5;
            //Parallel.Invoke(() => Console.WriteLine($"The factorial is: {Factorial(number)}"),
            //    ()=>Console.WriteLine($"There are {FactorialCounter(number)} digits in factorial of {number}."),
            //    ()=>Console.WriteLine($"Suma of members of factorial of {number} is: {FactorialMembersSumma(number)}"));
            // #3
            //int startRange = 2;
            //int endRange = 6;
            //Parallel.For(startRange, endRange, MultiplicationTable);
            Random random = new Random();
            List<int> array = new List<int>();
            for (int i = 0; i <=10 ; i++)
            {
                array.Add(random.Next(11));
            }
            Parallel.ForEach(array,FactorialVoid);

        }

    }
}
