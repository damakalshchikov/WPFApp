using System.Text.RegularExpressions;

namespace WpfApp.Utils;


/*
 * Класс StringHelper служит для преобразования пользовательского ввода
 * в такой формат, который можно было бы посчитать с помощью возможностей C#
 */
public class StringHelper
{
    public static string SanitizeInput(string input)
    {
        // Замена символов "+×," на "/*."
        input =  Regex.Replace(
            input,
            @"[÷×,]",
            match =>
            {
                return match.Value switch
                {
                    "÷" => "/",
                    "×" => "*",
                    "," => ".",
                    _ => match.Value
                };
            }
        );
        
        // Замена символа % на выражение <число>/100
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
        // Замена . на , в output
        return Regex.Replace(output, @"\.", ",");
    }
}