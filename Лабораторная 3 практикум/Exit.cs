internal class Exit
{
    public static bool ExitProgram()
    {
        string input;
        do
        {
            Console.Write("Вы уверены, что хотите выйти? (y/n): ");
            input = Console.ReadLine().ToLower();

            if (input != "y" && input != "n")
            {
                Console.WriteLine("Ошибка ввода. Введите 'y' или 'n'.");
            }
        } while (input != "y" && input != "n");

        Console.Clear();
        return input == "y";
    }
}