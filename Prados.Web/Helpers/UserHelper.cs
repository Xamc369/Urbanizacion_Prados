using Microsoft.AspNetCore.Identity;
using Prados.Web.Data;
using Prados.Web.Data.Entities;
using Prados.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Prados.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<Userstbl> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Userstbl> _signInManager;
        private readonly DataContext _datacontext;
        private readonly IFlashMessage _flashMessage;

        public UserHelper(
            DataContext datacontext,
            UserManager<Userstbl> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Userstbl> signInManager,
            IFlashMessage flashMessage)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _datacontext = datacontext;
            _flashMessage = flashMessage;
        }

        //eliminar un propietario

        public async Task<bool> DeleteUserAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
            {
                return true;
            }

            //user.Pro_Estado = 'I';
            //_datacontext.Users.Remove(user);
            //_flashMessage.Confirmation("El usuario fue borrado");
            //await _context.SaveChangesAsync();
            //return RedirectToAction($"{nameof(Details)}/{pago.Propietario.Id}");


            var response = await _userManager.DeleteAsync(user);
            return response.Succeeded;
        }

        // sing in result para el lgoueo y deslogueo
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        //  las interfaces de agregar o adicionar usuarips
        public async Task<IdentityResult> AddUserAsync(Userstbl user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(Userstbl user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<Userstbl> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);

        }

        public async Task<bool> IsUserInRoleAsync(Userstbl user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> UpdateUserAsync(Userstbl user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(Userstbl user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Userstbl user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Userstbl user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<Userstbl> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> ChangePasswordAsync(Userstbl user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Userstbl user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(Userstbl user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }



    }
}
