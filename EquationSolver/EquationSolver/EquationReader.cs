using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class EquationReader
    {

        public static Equation ConsoleEquationsRead()
        {
            string readLine = "";

            while (true)
            {
                try
                {
                    Console.WriteLine("Ange en linjär ekvation eller olikhet: ");
                    readLine = Console.ReadLine();

                    Equation readEquation = new Equation(readLine);

                    return readEquation;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("FEL! {0} kunde inte tolkas som en ekvation.", readLine);
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

        }
    }
}
