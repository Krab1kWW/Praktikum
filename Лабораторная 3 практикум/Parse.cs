internal class Parse
{
    public static int GetInt(string message)
    {
        Console.WriteLine(message);
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("Введите корректное значение");
        }
        return result;
    }

    public static double ToDouble(string text)
    {
        Console.WriteLine($"{text}");
        string message = Console.ReadLine();

        if (!double.TryParse(message, out double res))
            throw new ArgumentException("Введено нечисловое значение.");

        if (res <= 0)
            throw new ArgumentException("Отрицательные числа не допускаются.");

        if (res > 1000000000000)
            throw new OverflowException("Слишком большое число.");

        return res;
    }
}
