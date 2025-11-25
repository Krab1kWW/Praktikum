internal class CreditCalculator
{
    public static void CalculateLoan()
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



    public static void Annuity()
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

        Program.SaveCalculationHistory(loanAmount, annualRate, years, "аннуитетный", overpayment);
    }

    public static void Diff()
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

        Program.SaveCalculationHistory(loanAmount, annualRate, years, "дифференцированный", overpayment);
    }



    

    static int Type()
    {
        int type;
        do
        {
            type = Parse.GetInt("Выберите тип платежа (1 - аннуитетный, 2 - дифференцированный)");
            if (type != 1 && type != 2)
            {
                Console.WriteLine("Ошибка! Введите 1 или 2");
            }
        } while (type != 1 && type != 2);

        return type;
    }


    static (double loanAmount, double annualRate, int years) GetLoanParameters()
    {
        double loanAmount = Parse.ToDouble("Введите сумму кредита: ");
        double annualRate = Parse.ToDouble("Введите годовую процентную ставку (%):");
        int years = Parse.GetInt("Введите срок кредита (лет): ");

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


}
