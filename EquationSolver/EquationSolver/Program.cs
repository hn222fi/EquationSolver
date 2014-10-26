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
            Expression test = new Expression("2x-3x+7x+8t-9.6x+9-7");
            try
            {
                Console.WriteLine(test);
                test += new Term("4x");
                Console.WriteLine(test);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
