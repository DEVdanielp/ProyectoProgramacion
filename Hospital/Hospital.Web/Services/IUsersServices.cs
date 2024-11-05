using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.DTOs;
using Hospital.Web.Helpers;

public interface IUsersService
{
    public Task<IdentityResult> AddUserAsync(User user, string password);
    public Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    public Task<string> GenerateEmailConfirmationTokenAsync(User user);
    public Task<User> GetUserAsync(string email);
    public Task<SignInResult> LoginAsync(LoginDTO dto);
    public Task LogoutAsync();

}

public class UsersService : IUsersService
{
    private readonly DataContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConverterHelper _converterHelper;

    public UsersService(DataContext context, SignInManager<User> signInManager, UserManager<User> userManager, IConverterHelper converterHelper)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _converterHelper = converterHelper;
    }

    public async Task<IdentityResult> AddUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
        return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }
    
    public async Task<User> GetUserAsync(string email)
    {
        User? user = await _context.Users.Include(u => u.HospitalRole)
                                         .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<SignInResult> LoginAsync(LoginDTO dto)
    {
        return await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
