namespace ConsoleApp1;

public class BudgetService
{
    public IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return 0;
        }

        var budgets = _budgetRepo.GetAll();
        decimal totalBudget = 0;

        foreach (var budget in budgets)
        {
            // 解析 YearMonth，例如 "202402" 轉換為 2024-02-01
            if (!DateTime.TryParseExact(budget.YearMonth, "yyyyMM", null, System.Globalization.DateTimeStyles.None, out DateTime budgetMonthStart))
            {
                continue; // 如果格式錯誤，跳過
            }

            int daysInMonth = DateTime.DaysInMonth(budgetMonthStart.Year, budgetMonthStart.Month);
            var budgetMonthEnd = budgetMonthStart.AddMonths(1).AddDays(-1);

            // 檢查這個 Budget 是否與查詢範圍有交集
            if (endDate < budgetMonthStart || startDate > budgetMonthEnd)
            {
                continue; // 如果不在範圍內，跳過
            }

            // 計算這個月份內要計算的天數
            var effectiveStart = startDate > budgetMonthStart ? startDate : budgetMonthStart;
            var effectiveEnd = endDate < budgetMonthEnd ? endDate : budgetMonthEnd;
            int daysToConsider = (effectiveEnd - effectiveStart).Days + 1;

            // 按天數比例計算預算
            decimal dailyBudget = (decimal)budget.Amount / daysInMonth;
            totalBudget += dailyBudget * daysToConsider;
        }

        return totalBudget;
    }


}