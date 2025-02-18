namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }


    interface IBudgetRepo
    {
        List<Budget> GetAll();
    }

    class Budget
    {
        public string YearMonth;
        public int Amount;
    }
}
