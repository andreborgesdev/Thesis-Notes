using System;
using System.Collections.Generic;

namespace NetFxStackCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> values = new Stack<int>();

            foreach (string token in args)
            {
                int value;
                if (int.TryParse(token, out value))
                {
                    values.Push(value);
                }
                else
                {
                    int rhs = values.Pop();
                    int lhs = values.Pop();

                    switch (token)
                    {
                        case "+":
                            values.Push(lhs + rhs);
                            break;
                        case "-":
                            values.Push(lhs - rhs);
                            break;
                        case "*":
                            values.Push(lhs * rhs);
                            break;
                        case "/":
                            values.Push(lhs / rhs);
                            break;
                        case "%":
                            values.Push(lhs % rhs);
                            break;
                        default:
                            throw new ArgumentException(string.Format("Unrecognized token: {0}", token));
                    }
                }
            }

            Console.WriteLine(values.Pop());
        }
    }
}
