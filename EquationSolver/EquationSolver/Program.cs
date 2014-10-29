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
            Equation equationToSolve = EquationReader.ConsoleEquationsRead();

            Console.Clear();

            Console.WriteLine(equationToSolve);
            Console.WriteLine();
            equationToSolve.Solve();
            Console.WriteLine(equationToSolve);
           
        }
    }
}
