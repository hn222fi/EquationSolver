using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EquationSolver
{
    class Equation
    {
        // Deklarerar lokala fält
        private Expression _leftHandSide;
        private Expression _rightHandSide;
        private char _equalityType;

        // Egenskaper
        public char EqualityType
        {
            set 
            { 
                // Möjliga värden
                char[] equalityType = new char[]{'=','<','>'};

                if (equalityType.Contains(value))
                    _equalityType = value;
                else
                    throw new ArgumentException("Only =, <, > charachters can be used to represent an equation.");
            }
            get
            {
                return _equalityType;
            }

        }
        public Expression LeftHandSide { set { _leftHandSide = value; } get{return _leftHandSide;} }
        public Expression RightHandSide { set { _rightHandSide = value; } get{return _rightHandSide;} }

        public bool IsSimplified
        {
            get
            {
                if (LeftHandSide.Simplified() == LeftHandSide && RightHandSide.Simplified() == RightHandSide)
                    return true;
                else
                    return false;
            }
        }
        public bool IsSolved { private set; get; }
        public bool IsLinear
        {
            get 
            {
                List<char> varibles = new List<char>();
                List<Algebraic> combinedSides = new List<Algebraic>();
                combinedSides.AddRange(LeftHandSide.ClonedExpressionParts);
                combinedSides.AddRange(RightHandSide.ClonedExpressionParts);

                foreach(Term terms in combinedSides)
                {
                    if (!varibles.Contains(terms.Varible) && terms.Varible != 0)
                        varibles.Add(terms.Varible);
                }

                if (varibles.Count != 1)
                    return false;
                else
                    return true;
            }
        }
        // Konstruktorer
        public Equation(Expression left, Expression right, char equalitySign)
        {
            Initialize(left, right, equalitySign);
        }
        public Equation(string equationExpression)
        {
            // Tar bort mellanslag som kan finnas
            equationExpression.Trim();

            // Skapar en lista med strängar av den sträng som angetts för att skapa objektet. Den strängen delas av alla + eller - tecken
            List<string> sides = Regex.Split(equationExpression, @"(?=[=<>])").ToList();

            // Tar bort eventuellt tomma objekt i listan 
            sides.Remove("");

            // Minskar listans storlek till antalet objekt som finns
            sides.TrimExcess();

            // Anropar initialize
            Initialize(new Expression(sides[0]), new Expression(sides[1].Substring(1)), sides[1].ElementAt(0));
        }
        private void Initialize(Expression left, Expression right, char equalitySign)
        {
            EqualityType = equalitySign;
            LeftHandSide = left;
            RightHandSide = right;
            IsSolved = false;
        }

        // Methods for solving
        /// <summary>
        /// Solves the equation completely
        /// </summary>
        public void Solve()
        {
            while (!IsSolved)
            {
                SolveNextStep();
            }
        }
        /// <summary>
        /// Löser en ekvation i fyra steg. 
        /// 1: Förenklar båda leden
        /// 2: Bestämmer sidan med högst variabletermskoefficient och för alla variabeltermer till den sidan
        /// 3: Bestämmer sidan utan x och för all konstanter till den sidan
        /// 4: Dividerar båda sidor med koefficienten framför variabeln
        /// </summary>
        public void SolveNextStep()
        {
            if (!IsLinear)
                throw new ApplicationException("Only linear equations can be solved.");

            int stepSwitch = 0;

            // Kontrollerar om båda sidor är förenklade
            if (!IsSimplified)
            {
                stepSwitch = 1;
            }
            // Kontrollerar om båda sidor innehåller variabler
            else if(!LeftHandSide.IsConstant && !RightHandSide.IsConstant)
            {
                stepSwitch = 2;
            }
            // Kontrollerar om variabeltermen är ensam    
            else if((!LeftHandSide.IsConstant && LeftHandSide.ExpressionParts.Count > 1)||(!RightHandSide.IsConstant && RightHandSide.ExpressionParts.Count > 1))
            {
                stepSwitch = 3;
            }
            else
            {
                // Kontrollerar om koefficienten framför variabeltermen är ett
                if (!LeftHandSide.IsConstant)
                {
                    Term variableTerm = LeftHandSide.ExpressionParts[0] as Term;
                    if (variableTerm.Koefficent != 1)
                        stepSwitch = 4;
                }
                if (!RightHandSide.IsConstant)
                {
                    Term variableTerm = RightHandSide.ExpressionParts[0] as Term;
                    if (variableTerm.Koefficent != 1)
                        stepSwitch = 4;
                }

            }
            
            switch(stepSwitch)
            {
                // Ekvationen är redan löst
                case 0:
                    IsSolved = true;
                    break;
                // Ekvationen ska förenklas i båda led
                case 1:
                    LeftHandSide.Simplify();
                    RightHandSide.Simplify();
                    break;
                // Ekvatioen ska samla alla variabeltermer på samma sida
                case 2:
                    CollectVariabelsOnOneSide();
                    break;
                // Tar bort konstanter från den sida där variabeln finns
                case 3:
                    RemoveConstantsFromVaribleSide();
                    break;
                // Dividerar båda sidor med koefficienten framför varibeltermen
                case 4:
                    DivideKoefficent();
                    IsSolved = true;
                    break;
            }
        }
        /// <summary>
        /// Step 2 of solving equations. Collect all varibleterms on the side with the most varible terms.
        /// </summary>
        private void CollectVariabelsOnOneSide()
        {
            decimal left = 0;
            decimal right = 0;

            foreach (Term part in LeftHandSide.ExpressionParts)
            {
                if (!part.IsConstant)
                    left += part.Koefficent;
            }
            foreach (Term part in RightHandSide.ExpressionParts)
            {
                if (!part.IsConstant)
                    right += part.Koefficent;
            }
            if (left > right)
            {
                for (int i = 0; i < RightHandSide.ExpressionParts.Count && !RightHandSide.ExpressionParts[i].IsConstant; i++)
                {
                    Term temp = RightHandSide.ExpressionParts[i] as Term;

                    if (temp.Koefficent < 0)
                        LeftHandSide += temp;
                    else
                        LeftHandSide -= temp;

                    RightHandSide.ExpressionParts.RemoveAt(i);
                    LeftHandSide.Simplify();
                }
            }
            else if(left < right)
            {
                for (int i = 0; i < LeftHandSide.ExpressionParts.Count && !LeftHandSide.ExpressionParts[i].IsConstant; i++)
                {
                    Term temp = LeftHandSide.ExpressionParts[i] as Term;

                    if (temp.Koefficent < 0)
                        RightHandSide += temp;
                    else
                        RightHandSide -= temp;

                    LeftHandSide.ExpressionParts.RemoveAt(i);
                    RightHandSide.Simplify();
                }
            }
            else
            {
                throw new ApplicationException("The equation is not solvable.");
            }

        }
        /// <summary>
        /// Step 3 in solving equation. Removes constants from the side with varibles.
        /// </summary>
        private void RemoveConstantsFromVaribleSide()
        {
            if (LeftHandSide.IsConstant)
            {
                for (int i = 0; i < RightHandSide.ExpressionParts.Count;i++ )
                { 
                    if(RightHandSide.ExpressionParts[i].IsConstant)
                    {
                        Term temp = RightHandSide.ExpressionParts[i] as Term;
                        temp.Koefficent *= -1;
                        LeftHandSide += temp;
                        RightHandSide.ExpressionParts.RemoveAt(i);
                        LeftHandSide.Simplify();
                    }
                }
            }
            if (RightHandSide.IsConstant)
            {
                for (int i = 0; i < LeftHandSide.ExpressionParts.Count; i++)
                {
                    if (LeftHandSide.ExpressionParts[i].IsConstant)
                    {
                        Term temp = LeftHandSide.ExpressionParts[i] as Term;
                        temp.Koefficent *= -1;
                        RightHandSide += temp;
                        LeftHandSide.ExpressionParts.RemoveAt(i);
                        RightHandSide.Simplify();
                    }
                }
            }

        }
        /// <summary>
        /// Step 4 in solving equations. Divide by the koefficent infront of the varible term.
        /// </summary>
        private void DivideKoefficent()
        {
            Term temp; 
            
            if (LeftHandSide.IsConstant)
            {
                temp = RightHandSide.ExpressionParts[0] as Term;
            }
            else
            {
                temp = LeftHandSide.ExpressionParts[0] as Term;
            }
            
            if (temp.Koefficent == 0)
                throw new DivideByZeroException("Division by zero is undefined");

            decimal divider = temp.Koefficent;

            LeftHandSide /= divider;
            RightHandSide /= divider;

            if (divider < 0)
            {
                if (EqualityType == '<')
                    EqualityType = '>';
                else if (EqualityType == '>')
                    EqualityType = '<';
            }
        }
        // Overriding object methods
        public override string ToString()
        {
            return LeftHandSide.ToString() + EqualityType + RightHandSide.ToString();
        }

        // Clone interface members
        public object Clone()
        {
            Expression leftClone = this.LeftHandSide.Clone() as Expression;
            Expression rightClone = this.RightHandSide.Clone() as Expression;

            return new Equation(leftClone,rightClone, this.EqualityType);
        }

       
    }
}
