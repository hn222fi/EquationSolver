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
            // Tar bort mellanslag som kan finnas
            expression.Trim();

            // Skapar en lista med strängar av den sträng som angetts för att skapa objektet. Den strängen delas av alla + eller - tecken
            List<string> parts = Regex.Split(expression, @"(?=[+-])").ToList();
            
            // Tar bort eventuellt tomma objekt i listan 
            parts.Remove("");

            // Minskar listans storlek till antalet objekt som finns
            parts.TrimExcess();

            // Initierar lisatn som göms i _expressionParts
            ExpressionParts = new List<IAlgebraic>();

            // Lägger till varje string objekt från listan som et termobjekt i ExpressionParts
            foreach (string part in parts)
            {
                    ExpressionParts.Add(new Term(part));
            }
        }
        public Expression(List<IAlgebraic> expressionParts)
        {
            ExpressionParts = expressionParts;
        }

        // Egenskaper
            public List<IAlgebraic> ExpressionParts
            {
                get {return _expressionParts;}
                set {_expressionParts = value;}
            }
        // Overriding object methods
        public override string ToString()
        {
            string expression = "";
            
            foreach (IAlgebraic part in ExpressionParts)
                expression += part.ToString();

           expression = expression.TrimStart('+');

           return expression;
        }

        // Overriding operators
        public static Expression operator + (Expression ex, Term term)
        {
            Expression tempExpression = new Expression(ex.ExpressionParts);
            tempExpression.ExpressionParts.Add(term);

            return tempExpression;
        }
    }
}
