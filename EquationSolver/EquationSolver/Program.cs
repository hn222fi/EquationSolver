using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class Program
    {
        // Menu alternativ
        const string newEquation = "Skriv in en ny ekvation";
        const string solveEquation = "Lös vald ekvation";
        const string solveEquationStepWise = "Lös vald ekvation steg för steg";
        const string randomEquation = "Slumpa fram en ekvaiton";
        const string randomizeHundredEquations = "Slumpa 100 ekvationer (för felsökning)";
        const string chooseDecimals = "Välj antal decimaler som visas";
        const string endProgram = "Avsluta";
        static Equation _equation;
        static Menu EquationSolverMenu;
        static Random randomizer;

        static void Main(string[] args)
        {
            _equation = null;
            randomizer = new Random();

            // Skapar en ny menu
            EquationSolverMenu = new Menu(new string[] { newEquation, solveEquation, solveEquationStepWise, randomEquation, randomizeHundredEquations, chooseDecimals, endProgram }, new bool[] { true, false, false, true, true, true, true });

            do
            {
                // Rensar konsolfönstret
                Console.Clear();

                // Skriver ut en rubrik
                ConsoleGraphics.ShowHeader("Equationsolver 1.0");
                Console.WriteLine("(c) Henrik Nilsson 2014");
                Console.WriteLine();
                
                // Skriver ut vald ekvation om den finns
                if (_equation != null)
                {
                    Console.WriteLine("Inmatad ekvation är: {0}", _equation);
                    Console.WriteLine();
                }

                // Visar menyn
                EquationSolverMenu.ShowMenu();

                // Väljer från menyn
                EquationSolverMenu.MakeChoice();

                // Anropar metoden för att hantera valet
                try
                {
                    HandleOption(EquationSolverMenu.ChoosenOption);
                }
                catch (Exception ex)
                {
                    ConsoleGraphics.ViewMessage(ex.Message, ConsoleColor.Red);
                    PressAnyKeyToContinue();
                }

            } while (true);
        }
        private static void PressAnyKeyToContinue()
        {
            Console.WriteLine();
            ConsoleGraphics.ViewMessage("Tryck tangent för att fortsätt.");

            Console.ReadKey();
        }

        private static void HandleOption(MenuOption option)
        {

            if (option.OptionName == endProgram)
                System.Environment.Exit(-1);

            if (option.OptionName == newEquation)
            {
                _equation = EquationReader.ConsoleEquationsRead();
                EquationSolverMenu.Options[1].Choosable = true;
                EquationSolverMenu.Options[2].Choosable = true;
            }
            else if (option.OptionName == solveEquation)
            {
                Console.WriteLine();
                Console.WriteLine(SolveEquation(_equation, false));

                PressAnyKeyToContinue();
            }
            else if (option.OptionName == solveEquationStepWise)
            {
                Console.WriteLine();
                Console.WriteLine(_equation); 
                Console.WriteLine();

                SolveEquation(_equation, true);

                PressAnyKeyToContinue();
            }
            else if (option.OptionName == randomEquation)
            {
                _equation = RandomizeEquation();
                EquationSolverMenu.Options[1].Choosable = true;
                EquationSolverMenu.Options[2].Choosable = true;
            }
            else if (option.OptionName == chooseDecimals)
            {
                Console.WriteLine();
                while (true)
                {
                    string temp = "";

                    try
                    {
                        Console.WriteLine("Ange antalet deicmaler: ");
                        temp = Console.ReadLine();

                        Term.Decimals = int.Parse(temp);
                        break;
                    }
                    catch
                    {
                        ConsoleGraphics.ViewMessage(String.Format("FEL! {0} kunde inte tolkas som ett heltal.", temp), ConsoleColor.Red);
                    }
                }
                PressAnyKeyToContinue();
            }
            else if (option.OptionName == randomizeHundredEquations)
            {
                for (int i = 1; i < 100; i++)
                {
                    Equation test = RandomizeEquation();

                    while (!test.IsSolveble)
                        test = RandomizeEquation();

                    Console.WriteLine();
                    Console.WriteLine(test);
                    try
                    {
                        Console.WriteLine(SolveEquation(test,false));
                    }
                    catch
                    {
                        ConsoleGraphics.ViewMessage(String.Format("FEL! Ett fel inträffade när {0} skulle lösas.", test), ConsoleColor.Red);
                        PressAnyKeyToContinue();
                    }

                    // Pausar efter 10 ekvationer
                    if ( i % 10 == 0 && i != 1)
                        PressAnyKeyToContinue();
                }
                PressAnyKeyToContinue();
            }
        }
        /// <summary>
        /// Slumpar fram en ekvation
        /// </summary>
        /// <returns>ekvationen som slumpats fram</returns>
        private static Equation RandomizeEquation()
        {
            Expression leftSide;
            Expression rightSide;
            char sign = '0';
            string temp = "";

            // Slumpar vänsterled
            for (int i = 0; i < randomizer.Next(1, 6); i++)
            {
                if (randomizer.Next(0, 11) > 4)
                    temp += "+";
                else
                    temp += "-";

                if (randomizer.Next(0, 11) > 4)
                    temp += randomizer.Next(1, 50) + "x";
                else
                    temp += randomizer.Next(1, 50) * 100;
            }
            leftSide = new Expression(temp);

            // Slumpar högerled
            temp = "";
            for (int i = 0; i < randomizer.Next(1, 6); i++)
            {
                if (randomizer.Next(0, 11) > 4)
                    temp += "+";
                else
                    temp += "-";

                if (randomizer.Next(0, 11) > 4)
                    temp += randomizer.Next(1, 50) * 100 + "x";
                else
                    temp += randomizer.Next(1, 50) * 100;
            }
            rightSide = new Expression(temp);

            int signNumber = randomizer.Next(1, 4);

            switch (signNumber)
            {
                case 1:
                    sign = '<';
                    break;
                case 2:
                    sign = '>';
                    break;
                case 3:
                    sign = '=';
                    break;
            }

            return new Equation(leftSide, rightSide, sign);
        }
        /// <summary>
        /// Löser en ekvation
        /// </summary>
        /// <param name="equation">ekvation som ska lösas</param>
        /// <param name="stepByStep">true om den ska lösas steg för steg</param>
        /// <returns>löst ekvation</returns>
        private static Equation SolveEquation(Equation equation, bool stepByStep)
        {
            Equation tempEquation = (Equation)equation.Clone();
            
            if (!equation.IsSolveble)
                    ConsoleGraphics.ViewMessage("Ekvationen kan inte lösas", ConsoleColor.Red);
            else if (equation.IsSolved)
                ConsoleGraphics.ViewMessage("Ekvationen är redan löst");
            else if (!stepByStep)
            {
                tempEquation.Solve();
            }
            else
            {
                while (!tempEquation.IsSolved)
                {
                    Console.WriteLine("{0:-8} <= {1}", tempEquation, tempEquation.SolveNextStep());
                }
            }

            return tempEquation;
        }
    }
}
