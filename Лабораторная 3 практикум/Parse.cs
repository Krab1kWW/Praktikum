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
        Console.WriteLine(text);
        while (true)
        {
            if (!double.TryParse(Console.ReadLine(), out double res))
            {
                Console.WriteLine("Введено нечисловое значение. Попробуйте еще раз:");
                continue;
            }
            if (res <= 0)
            {
                Console.WriteLine("Отрицательные числа и ноль не допускаются. Попробуйте еще раз:");
                continue;
            }

            if (res > 1000000000000)
            {
                Console.WriteLine("Слишком большое число. Попробуйте еще раз:");
                continue;
            }

            return res;
        }
    }


}
