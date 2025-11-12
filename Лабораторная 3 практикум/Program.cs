using System;

class Program
{
    static bool exit = false;

    static void Main()
    {
        while (!exit)
        {
            try
            {
                Console.WriteLine("=== Калькулятор кредита ===");
                Console.WriteLine("1. Рассчитать новый кредит");
                Console.WriteLine("2. Просмотреть историю расчётов");
                Console.WriteLine("3. Анализ кредитной истории");
                Console.WriteLine("4. Поиск в истории");
                Console.WriteLine("5. Выйти");

                int choice = ToInt("\nВыберите действие:");

                if (choice <= 0 || choice >= 5)
                {
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 4.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        CalculateLoan();
                        break;
                    case 2:
                        Console.Clear();
                        
                        break;
                    case 3:
                        Console.Clear();
                        
                        break;
                    case 4:
                        Console.Clear();
                        
                        break;
                    case 5:
                        Console.Clear();
                        exit = ExitProgram();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                if (!exit)
                    Console.WriteLine("Спасибо за использование калькулятора!\n");
            }
        }
    }

    static void CalculateLoan()
    {
        int type = Type();

        if (type == 1)
        {
            Annuity();
        }
        else
        {
            Diff();
        }

        Console.WriteLine("\n\n\nХотите сделать ещё один расчёт? (да/нет): ");
        string ans = Console.ReadLine().ToLower();
        if (ans == "да")
        {
            CalculateLoan();
        }
    }





    static void CalculationHistory()
    {
        string[] credits 
    }





    static int Type()
    {
        int type;
        do
        {
            type = ToInt("Выберите тип платежа (1 - аннуитетный, 2 - дифференцированный)");
            if (type != 1 && type != 2)
            {
                Console.WriteLine("Ошибка! Введите 1 или 2");
            }
        } while (type != 1 && type != 2);

        return type;
    }


    static double ToDouble(string text)
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

    static int ToInt(string text)
    {
        Console.WriteLine($"{text}");
        string message = Console.ReadLine();

        if (!int.TryParse(message, out int res))
            throw new ArgumentException("Введено нечисловое значение.");

        if (res <= 0)
            throw new ArgumentException("Отрицательные числа не допускаются.");

        return res;
    }

    static (double loanAmount, double annualRate, int years) GetLoanParameters()
    {
        double loanAmount = ToDouble("Введите сумму кредита: ");
        double annualRate = ToDouble("Введите годовую процентную ставку (%):");
        int years = ToInt("Введите срок кредита (лет): ");

        if (years == 0)
            throw new DivideByZeroException("Срок кредита не может быть равен нулю.");

        return (loanAmount, annualRate, years);
    }

    static double IsOld(double annualRate, int years)
    {
        Console.WriteLine("Вы пенсионер? (да/нет):");
        string old = Console.ReadLine().ToLower();

        while (old != "да" && old != "нет")
        {
            Console.WriteLine("Введите корректное значение");
            old = Console.ReadLine().ToLower();
        }

        if (old == "да") annualRate -= 0.5;
        if (years >= 5) annualRate += 1;

        return annualRate;
    }

    static void Annuity()
    {
        const int MONTHS_IN_YEAR = 12;
        const int PERCENT_DIVISOR = 100;
        (double loanAmount, double annualRate, int years) = GetLoanParameters();
        annualRate = IsOld(annualRate, years);

        double annualRateForYears = annualRate * years;
        double totalAmount = loanAmount * (1 + annualRateForYears / PERCENT_DIVISOR);
        double monthlyPayment = totalAmount / (years * MONTHS_IN_YEAR);
        double overpayment = totalAmount - loanAmount;

        Console.WriteLine("\n=== Результаты расчета ===");
        Console.WriteLine($"Сумма кредита: {loanAmount:F2} руб.");
        Console.WriteLine($"Общая сумма выплат: {totalAmount:F2} руб.");
        Console.WriteLine($"Переплата: {overpayment:F2} руб.");
        Console.WriteLine($"Ежемесячный платеж: {monthlyPayment:F2} руб.");
        Console.WriteLine($"Срок кредита: {years} лет");
    }

    static void Diff()
    {
        const int MONTHS_IN_YEAR = 12;
        const int PERCENT_DIVISOR = 100;

        (double loanAmount, double annualRate, int years) = GetLoanParameters();
        annualRate = IsOld(annualRate, years);

        double monthlyRate = annualRate / PERCENT_DIVISOR / MONTHS_IN_YEAR;
        int totalMonths = years * MONTHS_IN_YEAR;
        double totalPayment = 0;

        double principalPayment = loanAmount / totalMonths;
        double remainingDebt = loanAmount;

        for (int month = 1; month <= totalMonths; month++)
        {
            double interestPayment = remainingDebt * monthlyRate;
            double monthlyPayment = principalPayment + interestPayment;
            totalPayment += monthlyPayment;
            remainingDebt -= principalPayment;
        }
        double overpayment = totalPayment - loanAmount;

        Console.WriteLine("\n=== Результаты расчета ===");
        Console.WriteLine($"Сумма кредита: {loanAmount:F2} руб.");
        Console.WriteLine($"Годовая ставка: {annualRate:F2}%");
        Console.WriteLine($"Срок: {years} лет ({totalMonths} месяцев)");
        Console.WriteLine($"Общая сумма выплат: {totalPayment:F2} руб.");
        Console.WriteLine($"Переплата: {overpayment:F2} руб.");
        Console.WriteLine($"Первый платеж: {(principalPayment + loanAmount * monthlyRate):F2} руб.");
        Console.WriteLine($"Последний платеж: {(principalPayment + principalPayment * monthlyRate):F2} руб.");
    }


    




    static void CalculateOverpayment()
    {

    }









    static bool ExitProgram()
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