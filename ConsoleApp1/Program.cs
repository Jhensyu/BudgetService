namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }


    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }

    public class Budget
    {
        public string YearMonth;
        public int Amount;
    }
}
