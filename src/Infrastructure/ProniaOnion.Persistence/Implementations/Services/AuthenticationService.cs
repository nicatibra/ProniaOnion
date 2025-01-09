using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.AppUsers;
using ProniaOnion.Domain.Entities;
using System.Text;

namespace ProniaOnion.Persistence.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterDto userDto)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == userDto.UserName || u.Email == userDto.Email))
                throw new Exception("User already exists");

            var result = await _userManager.CreateAsync(_mapper.Map<AppUser>(userDto), userDto.Password);

            //User yaranmadigi halda erroru gostermek ucun
            if (!result.Succeeded)
            {
                StringBuilder str = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    str.AppendLine(error.Description);
                }
                throw new Exception(str.ToString());
            }
        }


        public async Task LoginAsync(LoginDto userDto)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Name == userDto.UserNameOrEmail || u.Email == userDto.UserNameOrEmail);

            if (user == null)
                throw new Exception("User not found");

            var result = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(user);
                throw new Exception("Password is incorrect");
            }
        }
    }
}
