using System.Data;
using System.Text.RegularExpressions;
using MathNet.Numerics;

namespace WpfApp.Utils;


/*
 * Класс StringHelper служит для преобразования пользовательского ввода
 * в такой формат, который можно было бы посчитать с помощью возможностей C#
 */
public class StringHelper
{
    public static string SanitizeInput(string input)
    {
        // Замена π/e на числовое значение
        input = Regex.Replace(
            input,
            @"(π|e)",
            match =>
            {
                var mathConst = match.Groups[1].Value;
                
                switch (mathConst)
                {
                    case "π":
                        return Math.PI.ToString();
                    case "e":
                        return Math.E.ToString();
                    default:
                        return match.Value;
                }
            }
        );
        
        input = ReplaceBaseSymbols(input);
        
        // Обработка sin, cos и tg
        input = Regex.Replace(
            input,
            @"(sin|cos|tg)\(([^()]+)\)",
            match =>
            {
                var function = match.Groups[1].Value;
                var expression = match.Groups[2].Value; 
                var value = new DataTable().Compute(expression, null);
                
                switch (function)
                {
                    case "sin":
                        return Math.Sin(Convert.ToDouble(value)).ToString();
                    case "cos":
                        return Math.Cos(Convert.ToDouble(value)).ToString();
                    case "tg":
                        return Math.Tan(Convert.ToDouble(value)).ToString();
                    default:
                        return match.Value;
                }

            }
        );
        
        input = ReplaceBaseSymbols(input);
        
        // Замена ln на вычесленное выражение
        input = Regex.Replace(
            input,
            @"ln\(([^()]+)\)",
            match =>
            {
                var expression = match.Groups[1].Value;
                var value = new DataTable().Compute(expression, null);
                
                var res = Math.Log(Convert.ToDouble(value)).ToString();
                Console.WriteLine(res);

                return Math.Log(Convert.ToDouble(value)).ToString();
            }
        );
        
        input = ReplaceBaseSymbols(input);
        
        // Замена "²" на выражение (number*number)
        input = Regex.Replace(
            input,
            @"(\d+)²",
            match =>
            {
                var number = match.Groups[1].Value;
                return $"({number}*{number})";
            }
        );
        
        // Замена "^" на выражение (base*)
        input = Regex.Replace(
            input,
            @"(\d+|\([^()]+\))\^(\d+|\([^()]+\))",
            match =>
            {
                var number = int.Parse(match.Groups[1].Value);
                var exponentPart = int.Parse(match.Groups[2].Value);
                return Math.Pow(number, exponentPart).ToString();
            }
        );
        
        // Замена "!" на вычесленный факториал
        input = Regex.Replace(
            input,
            @"(\d+)!",
            match =>
            {
                var number = int.Parse(match.Groups[1].Value);
                return SpecialFunctions.Factorial(number).ToString();
            }
        );
        
        // Замена "%" на выражение <число>/100
        input = Regex.Replace(
            input,
            @"(\d+(\.\d+)?)%",
            match =>
            {
                var number = match.Groups[1].Value;
                return $"({number}/100)";
            }
        );
        return input;
    }

    public static string FormatOutput(string output)
    {
        // Замена "." на "," в output
        return Regex.Replace(output, @"\.", ",");
    }
}