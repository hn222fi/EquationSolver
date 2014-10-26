using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EquationSolver
{
    class Expression : IAlgebraic
    {
        // Deklarerar fält
        private List<IAlgebraic> _expressionParts;

        public Expression(string expression)
        {
            expression.Trim();
            expression = expression.Replace('.', ',');

            List<string> parts = Regex.Split(expression, @"(?=[+-])").ToList();
            parts.Remove("");
            parts.TrimExcess();

            _expressionParts = new List<IAlgebraic>();

            foreach (string part in parts)
            {
                    _expressionParts.Add(new Term(part));
            }
        }

        // Egenskaper

        // Overriding object methods
        public override string ToString()
        {
            string expression = "";

            foreach (IAlgebraic part in _expressionParts)
                expression += part.ToString();

            return expression;
        }
    }
}
