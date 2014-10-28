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
            Equation test = new Equation("-12x+23-4x+2234=45");

            try
            {
                Console.WriteLine(test);
                test.SolveNextStep();
                Console.WriteLine(test);
                test.SolveNextStep();
                Console.WriteLine(test);
                test.SolveNextStep();
                Console.WriteLine(test);
                test.SolveNextStep();
                Console.WriteLine(test);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
