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
            Expression test = new Expression("4x+5x-5+2a");
            try
            {
                Console.WriteLine(test);
                test /= 2;
                Console.WriteLine(test);
                test.Simplify();
                Console.WriteLine(test);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
