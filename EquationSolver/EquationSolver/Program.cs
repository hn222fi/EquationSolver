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
            Expression test = new Expression("4x+18-13x+1078-43-5a+7,3a");
            try
            {
                Console.WriteLine(test);
                test.Simplify();
                Console.WriteLine();
                Console.WriteLine(test);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
