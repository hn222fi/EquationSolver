using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EquationSolver
{
    class Expression : Algebraic
    {
        // Deklarerar fält
        private List<Algebraic> _expressionParts;

        public Expression(string expression)
        {
            // Tar bort mellanslag som kan finnas
            expression.Trim();

            // Kontrollerar om den inmatade strängen kan tolkas som ett uttryck
            if (!Regex.IsMatch(expression, @"^([+-]?\d*[.,]?\d*\w?)([+-]\d*[.,]?\d*\w?)*$"))
                throw new ArgumentException("Strängen kan inte tolkas som en uttryck");

            // Skapar en lista med strängar av den sträng som angetts för att skapa objektet. Den strängen delas av alla + eller - tecken
            List<string> parts = Regex.Split(expression, @"(?=[+-])").ToList();
            
            // Tar bort eventuellt tomma objekt i listan 
            parts.Remove("");

            // Minskar listans storlek till antalet objekt som finns
            parts.TrimExcess();

            // Initierar lisatn som göms i _expressionParts
            ExpressionParts = new List<Algebraic>();

            // Lägger till varje string objekt från listan som et termobjekt i ExpressionParts
            foreach (string part in parts)
            {
                    ExpressionParts.Add(new Term(part));
            }
        }
        public Expression(List<Algebraic> expressionParts)
        {
            ExpressionParts = expressionParts;
        }

        // Egenskaper
        public List<Algebraic> ExpressionParts
            {
                get {return _expressionParts;}
                set {_expressionParts = value;}
            }
        public List<Algebraic> ClonedExpressionParts
            {
                get 
                { 
                    List<Algebraic> clonedExpressionParts = new List<Algebraic>();

                    for (int i = 0; i < ExpressionParts.Count; i++ )
                    {
                        Algebraic temp = ExpressionParts[i].Clone() as Algebraic;
                        clonedExpressionParts.Add(temp);
                    }

                    return clonedExpressionParts; 
                }
            }
        // Metoder
        /// <summary>
        /// Förenklar uttrycket så långt smo möjligt
        /// </summary>
        public void Simplify()
        {
            ExpressionParts = Simplified().ExpressionParts;
        }
        /// <summary>
        /// Returnerar en clon av uttrycket som är förenklat
        /// </summary>
        /// <returns>En colnad förenklad kopia av uttrycket</returns>
        public Expression Simplified()
        {
            // Skapar en ny clonad lista för att kunna returnera ett clonat förenklat uttryck
            List<Algebraic> clonedExpressionParts = ClonedExpressionParts;
            List<Algebraic> clonedExpression = new List<Algebraic>();

            // Går igenom varje term i uttrycket
            for (int n = 0; n < clonedExpressionParts.Count; n++ )
            {
                // Skapar en ny term för att kontrollera den term som vi ska undersöka
                Term termPart = clonedExpressionParts[n] as Term;


                for (int i = n+1; i < clonedExpressionParts.Count; i++)
                {
                    Term nextTerm = clonedExpressionParts[i] as Term;

                    if (termPart.Varible == nextTerm.Varible)
                    {
                        termPart += nextTerm;
                        clonedExpressionParts.RemoveAt(i);
                        i--;
                    }
                }
                clonedExpression.Add(termPart);
            }

            return new Expression(clonedExpression);
        }

        // Overriding object methods
        public override string ToString()
        {
            if (ExpressionParts.Count == 0)
                return "0";
            
            string expression = "";
            
            foreach (Algebraic part in ExpressionParts)
                expression += part.ToString();

           expression = expression.TrimStart('+');

           return expression;
        }

        public override bool Equals(object obj)
        {
            Expression objExpression = obj as Expression;

            if ((object)objExpression == null)
                throw new ArgumentException("Equals method need to compare two expression objects");

            return (this.ExpressionParts.All(objExpression.ExpressionParts.Contains) && objExpression.ExpressionParts.All(this.ExpressionParts.Contains));
        }

        public override int GetHashCode()
        {
            int hash = 486143452;
            foreach(Term part in ExpressionParts)
            {
                hash = hash * 17 + part.GetHashCode();
            }

            return hash;
        }

        // Overriding operators
        public static bool operator == (Expression ex1, Expression ex2)
        {
            return ex1.Equals(ex2);
        }
        public static bool operator != (Expression ex1, Expression ex2)
        {
            return !(ex1 == ex2);
        }
        public static Expression operator + (Expression ex, Term term)
        {
            Expression tempExpression = new Expression(ex.ClonedExpressionParts);
            tempExpression.ExpressionParts.Add(term);

            return tempExpression;
        }
        public static Expression operator -(Expression ex, Term term)
        {
            Expression tempExpression = new Expression(ex.ClonedExpressionParts);
            term.Koefficent = -1 * term.Koefficent;
            tempExpression.ExpressionParts.Add(term);

            return tempExpression;
        }
        public static Expression operator *(Expression ex, decimal dec)
        {
            Expression tempExpression = new Expression(ex.ClonedExpressionParts);
            
            foreach(Term part in tempExpression.ExpressionParts)
            {
                part.Koefficent = part.Koefficent * dec;
            }

            return tempExpression;
        }
        public static Expression operator /(Expression ex, decimal dec)
        {
            Expression tempExpression = new Expression(ex.ClonedExpressionParts);

            foreach (Term part in tempExpression.ExpressionParts)
            {
                part.Koefficent = part.Koefficent / dec;
            }

            return tempExpression;
        }
        // Algebraic methods and properties
        public override bool IsConstant
        {
            get
            {
                foreach (Term part in ExpressionParts)
                {
                    if (!part.IsConstant)
                        return false;
                }
                return true;
            }
        }
        
        // Clone interface method
        public override object Clone()
        {
            return new Expression(this.ClonedExpressionParts);
        }
    }
}
