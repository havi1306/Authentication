using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Web_API.Entities;
using Web_API.Dtos;
using Web_API.Helpers;

namespace Web_API.Services
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();

        UserDto GetUser(int? id);

        User GetUser(string userName, string password);

        User GetUserById(int? id);

        string GenerateJSONWebToken(User user);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DelectUser(User user);
    }
    
    public class UserService :IUserService
    {
        private readonly DBContext _context;

        public UserService(DBContext context)
        {
            _context = context;
        }

        public List<UserDto> GetAllUsers()
        {
            var user = _context.Users.Include(x => x.Role)
                .Select(x => new UserDto()
                {
                    UserName = x.UserName,
                    Password = x.Password,
                    RoleId = x.Role.Id,
                })
                .ToList();
            return user;
        }

        public UserDto GetUser(int? id)
        {
            var user = _context.Users.Where(u => u.Id == id)
                .Select(u => new UserDto()
                {
                    UserName = u.UserName,
                    Password = u.Password,
                    RoleId = u.RoleID,
                })
                .FirstOrDefault();

            return user;
        }
        
        public User GetUser(string userName, string password)
        {
            var user = _context.Users.Include(x => x.Role)
                .FirstOrDefault(u => u.UserName == userName && u.Password == password);

            return user;
        }
        
        public User GetUserById(int? id)
        {
            return _context.Users.Find(id);
        }
        
        public string GenerateJSONWebToken(User user)
        {
            var claims = new[] {
                new Claim("Id", user.Id.ToString()),
                new Claim("Role", user.Role.Name)
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret Key You Devise")),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        
        public void DelectUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}