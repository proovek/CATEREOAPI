using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CatereoAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using CatereoAPI.Repository;
using Microsoft.AspNetCore.Http;
using CatereoAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using CatereoAPI.Data;
using Newtonsoft.Json;

namespace CatereoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {


        private readonly ApplicationDBContext _context;

        private readonly UserRepositoryInterface _UserRepositoryInterface;
        private readonly ICustomerCompanyRepository _CustomerComapnyInterface;
        private readonly IUserDataRepositoryDTO _UserDataRepositoryDTO;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly CreditsCalculationService _creditsService;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            UserRepositoryInterface userRepositoryInterface,
            ICustomerCompanyRepository customerCompanyRepository,
            IUserDataRepositoryDTO userDataRepositoryDTO,
            CreditsCalculationService creditsService,
            ApplicationDBContext context,
            IConfiguration configuration)
        {
            _context = context;
            _UserRepositoryInterface = userRepositoryInterface;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _CustomerComapnyInterface = customerCompanyRepository;
            _UserDataRepositoryDTO = userDataRepositoryDTO;
            _creditsService = creditsService;
        }

        [HttpGet]
        [Route("ping")]
        public async Task<string> Ping()
        {
            return "Pong";
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserName",user.Name),
                    new Claim("UserSurname",user.Surname),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("UserPosition",user.Position),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = new
                    {
                        userId = user.Id,
                        userName = user.UserName,
                        userEmail = user.Email,
                        phone = user.PhoneNumber,
                        displayName = user.Name + " " + user.Surname,
                        role = user.Role,
                        position = user.Position,
                        credits = user.Credits,
                        workDays = user.WorkDays,
                        dailyCredits = user.DailyCredits,
                    }
                });

            } 
            return Unauthorized();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser
            {
                Name = model.Name,
                Surname = model.Surname,
                Position = model.Position,
                Role = model.Role,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                MarginalRate = model.MarginalRate ?? 0.00,
                IsActive = model.IsActive ?? true,
                Credits = model.Credits,
                PhoneNumber = model.PhoneNumber,
                CustomerCompanyDTO = new List<CustomerCompanyDTO>(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            // Add user to roles
            await EnsureRolesAsync(user, _roleManager, _userManager);

            if (model.CustomerCompanyDTO != null && model.CustomerCompanyDTO.Any())
            {
                user.CustomerCompanyDTO = new List<CustomerCompanyDTO>();

                foreach (var companyDto in model.CustomerCompanyDTO)
                {
                    var company = await _context.CustomerCompanyDTO
                        .FirstOrDefaultAsync(c => c.Name == companyDto.Name);

                    if (company == null)
                    {
                        // Jeśli firma nie istnieje, utwórz nową i dodaj do kontekstu
                        company = new CustomerCompanyDTO { Name = companyDto.Name, Address = companyDto.Address };
                        _context.CustomerCompanyDTO.Add(company);
                    }

                    // Powiązanie firmy z użytkownikiem
                    user.CustomerCompanyDTO.Add(company);
                }

                // Ponieważ _userManager.CreateAsync nie zapisuje powiązań wiele-do-wielu,
                // musimy ręcznie zaktualizować użytkownika w kontekście i zapisać zmiany.
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private async Task EnsureRolesAsync(ApplicationUser user, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            var roles = new List<string> { UserRoles.Admin, UserRoles.User, UserRoles.Customer };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (await roleManager.RoleExistsAsync(user.Role))
            {
                await userManager.AddToRoleAsync(user, user.Role);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var usersList = await _userManager.Users.Include(u => u.CustomerCompanyDTO).ToListAsync(); // Załóżmy, że _userManager to Twoja usługa zarządzania użytkownikami

                var usersData = new List<object>(); // Lista do przechowywania danych wszystkich użytkowników

                foreach (var user in usersList)
                {

                    var userData = new
                    {
                        userId = user.Id,
                        userName = user.UserName,
                        userEmail = user.Email,
                        phone = user.PhoneNumber,
                        displayName = user.Name + " " + user.Surname,
                        role = user.Role,
                        position = user.Position,
                        isActive = user.IsActive,
                        customerCompanyDTO = user.CustomerCompanyDTO,
                        credits = user.Credits,
                        workDays = user.WorkDays,
                        dailyCredits = user.DailyCredits,
                    };

                    usersData.Add(userData);
                }

                return Ok(new { users = usersData });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        private UserData GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserData
                {
                    UserId = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type.Equals("UserName", StringComparison.InvariantCultureIgnoreCase))?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type.Equals("UserSurname", StringComparison.InvariantCultureIgnoreCase))?.Value,
                    MarginalRate = Convert.ToDouble(userClaims.FirstOrDefault(o => o.Type.Equals("MarginalRate", StringComparison.InvariantCultureIgnoreCase))?.Value),
            };
            }
            return null;
        }

        [Authorize]
        [HttpGet]
        [Route("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUserData()
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                var userData = await _userManager.Users
                .Include(u => u.CustomerCompanyDTO)
                .Where(u => u.Id == user.Id)
                .ToListAsync();

                if (user == null)
                {
                    return NotFound("Nie znaleziono użytkownika.");
                }

                return Ok(new { user = userData });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {

                var userId = GetCurrentUser().Username;

                var user = await _userManager.FindByNameAsync(userId);

                var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach(var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(", ", errors) });
            }
                
            return Ok(new Response { Status="Success", Message="Hasło zostało zmienione"});
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("GenerateAccessTokenForAppUsage")]
        public string GenerateAccessTokenForAppUsage()
        {
            string[] roles = { "Admin", "User" };
            var credentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "subject"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64)
            };

            foreach (var role in roles)
            {
                claims.Append(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddYears(10),
                signingCredentials: new SigningCredentials(credentials, SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("deleteUser/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound(new Response { Status = "Error", Message = "User not found!" });

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User deletion failed!" });

            return Ok(new Response { Status = "Success", Message = "User deleted successfully!" });
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("updateUser/{username}")]
        public async Task<IActionResult> UpdateUser(string username, [FromBody] RegisterModel model)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound(new Response { Status = "Error", Message = "User not found!" });

            // Aktualizuj właściwości użytkownika tylko jeśli zostały dostarczone
            user.Name = model.Name ?? user.Name;
            user.Surname = model.Surname ?? user.Surname;
            user.Position = model.Position ?? user.Position;
            // Uwaga: Role i inne krytyczne informacje powinny być aktualizowane ostrożnie
            user.Role = model.Role ?? user.Role;
            user.Email = model.Email ?? user.Email;
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
            user.IsActive = model.IsActive ?? user.IsActive;
            user.MarginalRate = model.MarginalRate ?? user.MarginalRate;
            user.Credits = model.Credits ?? user.Credits;

            // Dla CustomerCompanyDTO, upewnij się, że logika jest odpowiednia
            model.CustomerCompanyDTO = new List<CustomerCompanyDTO>();
            if (model.CustomerCompanyDTO != null)
            {
                if (user.CustomerCompanyDTO == null)
                {
                    user.CustomerCompanyDTO = new List<CustomerCompanyDTO>();
                }

                // Usuń stare powiązania CustomerCompanyDTO
                user.CustomerCompanyDTO.Clear();

                // Dodaj nowe powiązania CustomerCompanyDTO
                foreach (var companyDto in model.CustomerCompanyDTO)
                {
                    user.CustomerCompanyDTO.Add(companyDto);
                }
            }
            else
            {
                user.CustomerCompanyDTO = null;
            }

            // Zakładając, że nie aktualizujesz hasła. Jeśli tak, należy to zrobić oddzielnie.

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User update failed!" });

            return Ok(new Response { Status = "Success", Message = "User updated successfully!" });
        }



        // GET: api/User/credits
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("credits")]
        public async Task<ActionResult<IEnumerable<UserCreditsDto>>> GetAllUserCredits()
        {
            var users = await _userManager.Users.ToListAsync();
            var userCredits = users.Select(user => new UserCreditsDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Credits = user.Credits
            }).ToList();

            return Ok(userCredits);
        }


        // POST: api/User/update-credits
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("update-credits")]
        public async Task<IActionResult> UpdateAllUserCredits([FromBody] CreditsUpdateModel model)
        {
            var users = await _userManager.Users.ToListAsync();
            // Oblicz WorkDays za pomocą CalculateWorkDays
            var workDays = await _creditsService.CalculateWorkDays();

            foreach (var user in users)
            {
                // Użyj obliczonych WorkDays do obliczenia MonthlyCredits
                user.Credits =  _creditsService.CalculateMonthlyCredits(model.DailyCredits, workDays);
                user.DailyCredits = model.DailyCredits;
                user.WorkDays = workDays;
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }

    }
}