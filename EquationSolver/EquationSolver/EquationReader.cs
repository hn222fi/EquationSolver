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
                    Console.Write("Ange en linjär ekvation eller olikhet: ");
                    readLine = Console.ReadLine();

                    Equation readEquation = new Equation(readLine);

                    return readEquation;
                }
                catch (Exception ex)
                {
                    ConsoleGraphics.ViewMessage(String.Format("FEL! {0} kunde inte tolkas som en ekvation.\n {1}", readLine, ex.Message), ConsoleColor.Red);
                }
            }

        }

        
    }
}
