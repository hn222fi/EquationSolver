using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Term test1 = new Term("9x");
            Term test2 = new Term("3t");
            Term test3 = new Term("-2.5x");
            Term constant = new Term(3);
            Term constant2 = new Term("6.5");

            Console.WriteLine(test1);
            Console.WriteLine(test2);
            Console.WriteLine(test3);


            try
            {
                test3 *= 5;
               Console.WriteLine(test3);
               Console.WriteLine(test3 += test1*5);
               Console.WriteLine(test3 - test1);
               Console.WriteLine(test3);
               Console.WriteLine(test1 != test3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
