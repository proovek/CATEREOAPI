using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CatereoAPI.Model
{
    public class CreditsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CreditsCalculationService _creditsService;

        public CreditsService(UserManager<ApplicationUser> userManager, CreditsCalculationService creditsService)
        {
            _userManager = userManager;
            _creditsService = creditsService;
        }

        public async Task RenewCreditsForAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new CreditsUpdateModel(); // Tworzymy nowy obiekt modelu
                                                  // Oblicz WorkDays za pomocą CalculateWorkDays
            var workDays = await _creditsService.CalculateWorkDays();

            foreach (var user in users)
            {
                // Użyj obliczonych WorkDays do obliczenia MonthlyCredits
                user.Credits = _creditsService.CalculateMonthlyCredits(model.DailyCredits, workDays);
                user.DailyCredits = model.DailyCredits;
                user.WorkDays = workDays;
                await _userManager.UpdateAsync(user);
            }
        }
    }

}
