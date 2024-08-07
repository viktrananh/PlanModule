using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.CreatPlan
{
    internal class LinearEquationSolver
    {
        List<LinearEquation> rows = new List<LinearEquation>();
        decimal[] solution;

        public void AddLinearEquation(decimal result, params decimal[] coefficients)
        {
            rows.Add(new LinearEquation(result, coefficients));
        }

        public IList<decimal> Solve()       //Returns a list of coefficients for the variables in the same order they were entered
        {
            solution = new decimal[rows[0].Coefficients.Count()];

            for (int pivotM = 0; pivotM < rows.Count() - 1; pivotM++)
            {
                int pivotN = rows[pivotM].IndexOfFirstNonZero;

                for (int i = pivotN + 1; i < rows.Count(); i++)
                {
                    LinearEquation rowToReduce = rows[i];
                    decimal pivotFactor = rowToReduce[pivotN] / -rows[pivotM][pivotN];
                    rowToReduce.AddCoefficients(rows[pivotM], pivotFactor);
                }
            }

            while (rows.Any(r => r.Result != 0))
            {
                LinearEquation row = rows.FirstOrDefault(r => r.NonZeroCount == 1);
                if (row == null)
                {
                    break;
                }

                int solvedIndex = row.IndexOfFirstNonZero;
                decimal newSolution = row.Result / row[solvedIndex];

                AddToSolution(solvedIndex, newSolution);
            }

            return solution;
        }

        private void AddToSolution(int index, decimal value)
        {
            foreach (LinearEquation row in rows)
            {
                decimal coefficient = row[index];
                row[index] -= coefficient;
                row.Result -= coefficient * value;
            }

            solution[index] = value;
        }

        private class LinearEquation
        {
            public decimal[] Coefficients;
            public decimal Result;

            public LinearEquation(decimal result, params decimal[] coefficients)
            {
                this.Coefficients = coefficients;
                this.Result = result;
            }

            public decimal this[int i]
            {
                get { return Coefficients[i]; }
                set { Coefficients[i] = value; }
            }

            public void AddCoefficients(LinearEquation pivotEquation, decimal factor)
            {
                for (int i = 0; i < this.Coefficients.Count(); i++)
                {
                    this[i] += pivotEquation[i] * factor;
                    if (Math.Abs(this[i]) < 0.000000001M)    //Because sometimes rounding errors mean it's not quite zero, and it needs to be
                    {
                        this[i] = 0;
                    }
                }

                this.Result += pivotEquation.Result * factor;
            }

            public int IndexOfFirstNonZero
            {
                get
                {
                    for (int i = 0; i < Coefficients.Count(); i++)
                    {
                        if (this[i] != 0) return i;
                    }
                    return -1;
                }
            }

            public int NonZeroCount
            {
                get
                {
                    int count = 0;
                    for (int i = 0; i < Coefficients.Count(); i++)
                    {
                        if (this[i] != 0) count++;
                    }
                    return count;
                }
            }
        }
    }
}
