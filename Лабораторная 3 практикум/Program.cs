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
                        
                        break;
                    case 4:
                        Console.Clear();
                        
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
                    Console.WriteLine("Спасибо за использование калькулятора!\n");
            }
        }
    }


    static void ShowHistory()
    {
       if (historySize == 0)
        {
            Console.WriteLine("История пуста.");
            return;
        }
        
       for(int i = )
            Console.WriteLine("==== История расчётов ====");
            Console.WriteLine($"Количество кредитов: {historySize}");

            Console.WriteLine($"Процентная ставка: {rates[i]}");
            Console.WriteLine($"Срок кредита: {terms}");
            Console.WriteLine($"Тип платежей: {types}");
            Console.WriteLine($"Переплата по кредиту: {overpayments}");
        
        

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





    static void CalculateOverpayment()
    {

    }






  
   
    











 
}