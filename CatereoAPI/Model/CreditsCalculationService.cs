using Newtonsoft.Json;

namespace CatereoAPI.Model
{
    public class CreditsCalculationService
    {
        public double CalculateMonthlyCredits(double dailyCredits, int workDays)
        {
            return dailyCredits * workDays;
        }

        public async Task<List<Holiday>> GetHolidaysFromApi()
        {
            using (var client = new HttpClient())
            {
                var today = DateTime.Today;
                var lastDayOfMonth = DateTime.DaysInMonth(today.Year, today.Month);
                var url = $"https://openholidaysapi.org/PublicHolidays?countryIsoCode=PL&languageIsoCode=PL&validFrom={today:yyyy-MM-01}&validTo={today:yyyy-MM}-{lastDayOfMonth}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var holidays = JsonConvert.DeserializeObject<List<Holiday>>(content);
                return holidays;
            }
        }


        public async Task<int> CalculateWorkDays()
        {
            var holidays = await GetHolidaysFromApi();
            var holidayDates = holidays.SelectMany(h => Enumerable.Range(0, 1 + h.EndDate.Subtract(h.StartDate).Days).Select(offset => h.StartDate.AddDays(offset))).ToList();

            int workDays = 0;
            var today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var startOfMonth = new DateTime(year, month, 1);
            var daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = startOfMonth.AddDays(day - 1);
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday && !holidayDates.Contains(currentDate))
                {
                    workDays++;
                }
            }

            return workDays;
        }
    }
}
