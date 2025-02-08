using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController :BaseAPiController
    {
        private DataContext _dataContext = null;
        private ITokenService tokenService = null;

        public AccountController(DataContext dataContext ,ITokenService token)
        {
            _dataContext = dataContext;
            tokenService = token;
        }

        [HttpPost("register")] //account/register
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();

            if (await CheckUserExist(registerDto.UserName)) return BadRequest("User exist !");

            var user = new AppUser
            {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();

            var userDto = new UserDto()
            {
                UserName = registerDto.UserName,
                Token = tokenService.CreateToken(user)
            };

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid UserName");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0;i < computedhash.Length; i++) {
                if (computedhash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            var userDto = new UserDto()
            {
                UserName = loginDto.UserName,
                Token = tokenService.CreateToken(user)
            };

            return Ok(userDto);
        }

        private async Task<bool> CheckUserExist(string UserName)
        {
           return await _dataContext.Users.AnyAsync(x=>x.UserName.ToLower() == UserName.ToLower());
        }
    }
}
