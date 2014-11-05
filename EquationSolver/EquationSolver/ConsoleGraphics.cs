using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    static class ConsoleGraphics
    {
        /// <summary>
        /// Writes a header in the console window
        /// </summary>
        /// <param name="header">String to write as header</param>
        /// <param name="backgroundColor">Backgroundcolor(default = blue)</param>
        /// <param name="foregroundColor">Foregroundcolor(default = white)</param>
        public static void ShowHeader(string header,  ConsoleColor backgroundColor = ConsoleColor.Blue, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-                                                        -");
            Console.WriteLine("-"+CenterAlignString(header,56)+"-");
            Console.WriteLine("-                                                        -");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();
        }

                    /// <summary>
        /// Anropas för att visa meddelanden i consolfönstret
        /// </summary>
        /// <param name="message">Meddelande som ska visas</param>
        /// <param name="backgroundColor">Bakgrundsfärg på meddelandet (default är blå)</param>
        /// <param name="foregroundColor">Textfärg på meddelande (default är vit)</param>
        public static void ViewMessage(string message, ConsoleColor backgroundColor = ConsoleColor.Blue, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        /// <summary>
        /// Centers a string in a space with a fixed number of characters
        /// </summary>
        /// <param name="s">The string to center</param>
        /// <param name="other">width of the place to center it in</param>
        /// <returns>a copy of the string with spaces before and after</returns>
        public static string CenterAlignString(this string s, int width)
        {
            // Deklarerar variabler
            string temp = (string)s.Clone();

            for (int i = 0; i < width - s.Length; i++)
            {
                if (i < width / 2 - s.Length / 2)
                    temp = temp.Insert(0, " ");
                else
                    temp += " ";
            }
            return temp;
        }
    }
}
