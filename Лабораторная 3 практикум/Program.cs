using System;
class Program
{
    static bool exit = false;
        
    static double[] amounts = new double[50];       // суммы кредитов
    static double[] rates = new double[50];         // процентные ставки
    static int[] terms = new int[50];               // сроки в годах
    static string[] types = new string[50];         // типы платежей
    static double[] overpayments = new double[50];  // переплаты
    static DateTime[] dates = new DateTime[50];     // даты расчётов
    static int historySize = 0;                     // текущее количество

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

                int choice = Parse.GetInt("\nВыберите действие:");

                while (choice <= 0 || choice >= 6)
                {
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 5.");
                    choice = Parse.GetInt("");
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        CreditCalculator.CalculateLoan();
                        break;
                    case 2:
                        Console.Clear();
                        ShowHistory();
                        break;
                    case 3:
                        Console.Clear();
                        AnalizeCredit();
                        break;
                    case 4:
                        Console.Clear();
                        SearchInHistory();
                        break;
                    case 5:
                        Console.Clear();
                        exit = Exit.ExitProgram();
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
                    Console.WriteLine("\n\nСпасибо за использование калькулятора!\n");
            }
        }
    }


    static void ShowHistory()
    {
        Console.WriteLine("==== История расчётов ====");
        Console.WriteLine($"Количество кредитов: {historySize}\n");
        if (historySize == 0)
        {
            Console.WriteLine("История пуста.");
            return;
        }
        
       for(int i = historySize-1;i >= 0 ; i--)
        {
            Console.WriteLine($"Рассчёт номер {historySize - i}.");
            Console.WriteLine($"Сумма кредита: {amounts[i]:F2} руб.");
            Console.WriteLine($"Процентная ставка: {rates[i]:F2}%.");
            Console.WriteLine($"Срок (лет): {terms[i]}.");
            Console.WriteLine($"Тип платежа: {types[i]}.");
            Console.WriteLine($"Переплата: {overpayments[i]:F2} руб.\n");

        }

    }



    public static void SaveCalculationHistory(double amount, double rate, int term, string type, double overpayment)
    {

        DateTime yearAgo = DateTime.Now.AddYears(-1);

        for (int i = historySize - 1; i >= 0; i--)
        {
            if (dates[i] < yearAgo)
            {
                for (int j = i; j < historySize - 1; j++)
                {
                    amounts[j] = amounts[j + 1];
                    rates[j] = rates[j + 1];
                    terms[j] = terms[j + 1];
                    types[j] = types[j + 1];
                    overpayments[j] = overpayments[j + 1];
                    dates[j] = dates[j + 1];
                }
                historySize--;
            }
        }
        if (historySize < 50)
        {
            
            amounts[historySize] = amount;
            rates[historySize] = rate;
            terms[historySize] = term;
            types[historySize] = type;
            overpayments[historySize] = overpayment;
            dates[historySize] = DateTime.Now;
            historySize++;
        }
        else
        {
            for (int i = 0; i < 49; i++) 
            {
                amounts[i] = amounts[i + 1];
                rates[i] = rates[i + 1];
                terms[i] = terms[i + 1];
                types[i] = types[i + 1];
                overpayments[i] = overpayments[i + 1];
                dates[i] = dates[i + 1];
            }

            
            amounts[49] = amount;
            rates[49] = rate;
            terms[49] = term;
            types[49] = type;
            overpayments[49] = overpayment;
            dates[49] = DateTime.Now;

        }
    }



    static void AnalizeCredit()
    {
        Console.WriteLine("==== Анализ кредитной истории =====");

        if (historySize == 0)
        {
            Console.WriteLine("История пуста.");
            return;
        }

        int mxI = 0, mnI = 0;

        for (int i = 1; i < historySize; i++)
        {
            if (overpayments[i] > overpayments[mxI]) mxI = i;
            if (overpayments[i] < overpayments[mnI]) mnI = i;
        }

        Console.WriteLine($"Средняя ставка за период: {CalculateRates():F1}%");
        Console.WriteLine($"Максимальная переплата: {overpayments[mxI]:F0} руб. (кредит {amounts[mxI]:F0} руб. под {rates[mxI]}%)");
        Console.WriteLine($"Минимальная переплата: {overpayments[mnI]:F0}  руб. (кредит  {amounts[mnI]:F0} руб. под {rates[mnI]}%)");
        Console.WriteLine($"Изменение ставки относительно последнего расчёта: {LastCalculateRate():F1}%");
    }

    static double LastCalculateRate()
    {
        if (historySize < 2)
        {
            Console.WriteLine("Недостаточно данных для сравнения (нужно 2 расчёта)");
            return 0;
        }

        double lastRate = rates[historySize - 1];
        double previousRate = rates[historySize - 2];

        double rateChange = lastRate - previousRate;

        return rateChange;
    }

    static double MaxOverpay()
    {
        double MaxOverpay = overpayments[0];
       
        for (int i = historySize - 1; i >= 0; i--)
        {
            if (MaxOverpay < overpayments[i]) {MaxOverpay = overpayments[i];}
        }

        return MaxOverpay;
    }

    static double MinOverpay()
    {
        double MinOverpay = overpayments[0]; 
      
        for (int i = historySize - 1; i >= 0; i--)
        {
            if (MinOverpay > overpayments[i]) {MinOverpay = overpayments[i];}

        }
        return MinOverpay;
    }


    static double CalculateRates()
    {
        double sumRates = 0;
        for (int i = historySize - 1; i >= 0; i--)
        { 
            sumRates += rates[i];

        }
        double midleOverpay = sumRates / historySize;

        return midleOverpay;
    }


    static void SearchInHistory()
    {
        if (historySize == 0)
        {
            Console.WriteLine("История пуста.");
            return;
        }

        Console.WriteLine("=== Поиск в истории ===");
        Console.WriteLine("1. Поиск по сумме кредита");
        Console.WriteLine("2. Поиск по сроку кредита");

        int searchType = Parse.GetInt("Выберите тип поиска (1 или 2):");

        bool found = false;

        if (searchType == 1)
        {
            // Поиск по сумме
            double minAmount = Parse.ToDouble("Введите минимальную сумму кредита: ");
            double maxAmount = Parse.ToDouble("Введите максимальную сумму кредита: ");

            for (int i = 0; i < historySize; i++)
            {
                if (amounts[i] >= minAmount && amounts[i] <= maxAmount)
                {
                    Console.WriteLine($"Найден: {amounts[i]:F0} руб., ставка {rates[i]}%, срок (лет) {terms[i]}, переплата {overpayments[i]:F0} руб.");
                    found = true;
                }
            }
        }
        else if (searchType == 2)
        {
            // Поиск по сроку
            int maxYears = Parse.GetInt("Введите максимальный срок (лет): ");

            for (int i = 0; i < historySize; i++)
            {
                if (terms[i] <= maxYears)
                {
                    Console.WriteLine($"Срок (лет) {terms[i]}: {amounts[i]:F0} руб. под {rates[i]}%");
                    found = true;
                }
            }
        }
        else
        {
            Console.WriteLine("Неверный выбор.");
            return;
        }

        if (!found) { Console.WriteLine("Кредиты по вашему запросу не найдены."); }
    }

}