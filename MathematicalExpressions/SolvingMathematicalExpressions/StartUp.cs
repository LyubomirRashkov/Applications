using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvingMathematicalExpressions
{
    public class StartUp
    {
        private const string AllowedOperators = "^*/+-";

        static void Main()
        {
            Console.Title = "Mathemetical Expressions Solver";

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("Type the expression to evaluate or 'exit' for exit and press [Enter]:");

                Console.ForegroundColor = ConsoleColor.Gray;

                string expression = Console.ReadLine();

                if (expression == "exit")
                {
                    return;
                }

                try
                {
                    ValidateExpression(expression);

                    double result = Evaluate(expression);

                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine(result);

                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(ex.Message);

                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.WriteLine();
            }


        }

        private static void ValidateExpression(string expression)
        {
            if (AllowedOperators.Contains(expression[0]))
            {
                throw new InvalidOperationException("Invalid expression!");
            }

            if (AllowedOperators.Contains(expression[expression.Length - 1]))
            {
                throw new InvalidOperationException("Invalid expression!");
            }

            for (int i = 2; i < expression.Length - 1; i++)
            {
                if (AllowedOperators.Contains(expression[i]) && AllowedOperators.Contains(expression[i - 1]))
                {
                    throw new InvalidOperationException("Invalid expression!");
                }
            }
        }

        private static double Evaluate(string expression)
        {
            Stack<double> operands = new Stack<double>();

            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                char symbol = expression[i];

                if (symbol == '(')
                {
                    operators.Push(symbol);
                }
                else if (char.IsDigit(symbol) || symbol == '.')
                {
                    StringBuilder currentOperand = new StringBuilder();

                    while (char.IsDigit(symbol) || symbol == '.' || symbol == ' ')
                    {
                        if (symbol == ' ')
                        {
                            i++;

                            if (i == expression.Length)
                            {
                                break;
                            }

                            symbol = expression[i];

                            continue;
                        }

                        currentOperand.Append(symbol);

                        i++;

                        if (i == expression.Length)
                        {
                            break;
                        }

                        symbol = expression[i];
                    }

                    i--;

                    operands.Push(double.Parse(currentOperand.ToString()));
                }
                else if (AllowedOperators.Contains(symbol))
                {
                    while (operators.Any() && Priority(operators.Peek()) >= Priority(symbol))
                    {
                        char currentOperator = operators.Pop();

                        double operand2 = operands.Pop();
                        double operand1 = operands.Pop();

                        double resultOperand = ApplyOperation(currentOperator, operand1, operand2);

                        operands.Push(resultOperand);
                    }

                    operators.Push(symbol);
                }
                else if (symbol == ')')
                {
                    while (operators.Peek() != '(')
                    {
                        char currentOperation = operators.Pop();

                        double innerOperand2 = operands.Pop();
                        double innerOperand1 = operands.Pop();

                        double resultOperand = ApplyOperation(currentOperation, innerOperand1, innerOperand2);

                        operands.Push(resultOperand);
                    }

                    operators.Pop();
                }
                else if (symbol == ' ')
                {
                    continue;
                }
                else
                {
                    throw new ArgumentException($"Invalid symbol: {symbol}");
                }
            }

            while (operators.Any())
            {
                char currentOperation = operators.Pop();

                double operand2 = operands.Pop();
                double operand1 = operands.Pop();

                double currentResult = ApplyOperation(currentOperation, operand1, operand2);

                operands.Push(currentResult);
            }

            return operands.Pop();
        }

        private static double ApplyOperation(char @operator, double operand1, double operand2)
        {
            switch (@operator)
            {
                case '^':
                    return Math.Pow(operand1, operand2);

                case '*':
                    return operand1 * operand2;

                case '/':
                    if (operand2 == 0)
                    {
                        throw new InvalidOperationException("Can not divide by zero!");
                    }

                    return operand1 / operand2;

                case '+':
                    return operand1 + operand2;

                case '-':
                    return operand1 - operand2;

                default: return 0;
            }
        }

        private static int Priority(char @operator)
        {
            switch (@operator)
            {
                case '^':
                    return 3;

                case '*':
                    return 2;

                case '/':
                    return 2;

                case '+':
                    return 1;

                case '-':
                    return 1;

                default: return 0;
            }
        }
    }
}
