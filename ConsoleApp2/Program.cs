using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Program
{
    static void Main()
    {
        double currentValue = 0;
        bool done = false;
        string userInput = string.Empty;

        List<string> stack = new List<string>();

        while (!done)
        {
            if ((done = GetUserInput(ref userInput)) == false)
            {
                // Attempt to parse the command line arguments into our values and operands...
                ParseInput(userInput, ref stack);

                // Run the calculation represented within our stacks...
                RunCalculator(ref currentValue, ref stack);
            }
        }
    }

    static bool GetUserInput(ref string userInput)
    {
        bool userExit = false;

        try
        {
            Console.Write("> ");

            userInput = Console.ReadLine();

            if (userInput == "q")
            {
                userExit = true;
            }

            else if (IsValue(userInput))
            {
                Console.WriteLine(userInput);
            }
        }

        catch (Exception ex)
        {
            // Not a good idea but we'll "blindly eat" exceptions.
        }

        finally
        {

        }

        return (userExit);
    }

    static void RunCalculator(ref double currentValue, ref List<string> stack)
    {
        double? leftValue = 0;
        double? rightValue = 0;

        int numberOfOperations = 0;

        try
        {
            if (stack != null && stack.Count > 0)
            {
                string[] elements = stack.ToArray();
                string operand;

                string item;
                for (int i = 1; i < elements.Length; i++)
                {
                    item = elements[i];

                    if (IsOperand(item) && i >= 2)
                    {
                        numberOfOperations++;

                        leftValue = double.Parse(elements[i - 2]);
                        rightValue = double.Parse(elements[i - 1]);

                        operand = item;

                        double newValue = 0;
                        bool success = RunMiniCalculator(leftValue, rightValue, operand, ref newValue);

                        if (success)
                        {
                            // Remove the previous 2 elements.
                            RemoveAt<string>(ref elements, i - 2);
                            RemoveAt<string>(ref elements, i - 1);

                            // Adjust our index.
                            i -= 2;

                            // Replace the operand with the new value.
                            elements[i] = newValue.ToString();

                            currentValue = newValue;

                            // Rebuild our shared stack from our updated array.
                            stack = new List<string>(elements);
                        }

                    }
                }

                if (numberOfOperations > 0)
                {
                    Console.WriteLine(string.Format("{0:R}", currentValue));
                }

                // Rebuild our remaining stack;
                stack = new List<string>(elements);
            }
        }

        catch (Exception ex)
        {

        }

        finally
        {
        }
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }

    static bool RunMiniCalculator(double? leftValue, double? rightValue, string operand, ref double newValue)
    {
        bool success = true;

        try 
        {
            //Debug.WriteLine(string.Format("{0:R} {1} {2:R}", leftValue, operand, rightValue));
            //Console.WriteLine(string.Format("{0:R} {1} {2:R}", leftValue, operand, rightValue));

            if (leftValue.HasValue && rightValue.HasValue)
            {
                switch (operand)
                {
                    case "+":
                        newValue = (double) leftValue + (double) rightValue;
                        break;

                    case "-":
                        newValue = (double) leftValue - (double) rightValue;
                        break;

                    case "/":
                        newValue = (double) leftValue / (double) rightValue;
                        break;

                    case "*":
                        newValue = (double) leftValue * (double) rightValue;
                        break;

                    default:
                        success = false;
                        break;
                }
            }
            //Debug.WriteLine(string.Format("newValue {0:R}", newValue));
            //Console.WriteLine(string.Format("newValue {0:R}", newValue));
        }

        catch (Exception ex)
        {
            success = false;
        }

        finally
        {

        }

        return (success);
    }

    static bool ParseInput(string input, ref List<string> stack)
    {
        bool success = true;

        try
        {
            string[] args = input.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string arg in args)
            {
                stack.Add(arg);

            }

            //tack = InvertStack(stack);
        }

        catch (Exception ex)
        {
            success = false;
        }

        finally
        {

        }

        return (success);
    }

    /*
     * This method assumes we're inverting a 
     */
    static Stack<string> InvertStack(Stack<string> origStack)
    {
        Stack<string> newStack;

        try
        {
            string[] invertedArray = origStack.ToArray();

            //Array.Reverse(invertedArray);

            //origStack.Clear();

            newStack = new Stack<string>(invertedArray);
        }

        catch (Exception ex)
        {
            newStack = null;
        }

        finally
        {

        }

        return (newStack);
    }

    static bool IsValue(string arg)
    {
        bool isValue = false;

        try
        {
            int output = 0;

            isValue = int.TryParse(arg, out output);
        }

        catch (Exception ex)
        {
            isValue = false;
        }

        finally
        {

        }

        return (isValue);
    }
    static bool IsOperand(string arg)
    {
        bool IsOperand = false;

        try
        {
            if (arg == "+" || arg == "-" || arg == "*" || arg == "/")
                IsOperand = true;
        }

        catch (Exception ex)
        {
            IsOperand = false;
        }

        finally
        {

        }

        return (IsOperand);
    }
}
