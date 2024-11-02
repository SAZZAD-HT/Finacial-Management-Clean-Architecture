using Core.DTos;
using Core.Interfaces;
using Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public  class FinRepository: IFinRepository
    {
        private readonly FinManagementEntities _context;

        public FinRepository(FinManagementEntities context)
        {
            _context = context;
        }
        public async Task<IEnumerable< UserDto>>GetUser()
        {
            var data = await 
                (from user in _context.Users
                 select new UserDto
                 {  UserID=user.  UserID,
                    UserName=user.  UserName,
                    Email=user.  Email,
                    Password=user.  Password,
                    IsActive=user.IsActive,

                 }).ToListAsync();
            return data;    

        }
        public async Task<UserDto> GetUserById(int id)
        {
            var data = await
                         (from user in _context.Users
                          where user.UserID == id
                          select new UserDto
                          {
                              UserID = user.UserID,
                              UserName = user.UserName,
                              Email = user.Email,
                              Password = user.Password,
                              IsActive = user.IsActive,

                          }).FirstAsync();
            return data;
        }
        public async Task<string> AddUser(UserDto user)
        {
            string res;
            try
            {
                if (user.UserID == 0)
                {
                    _context.Users.Add(new User
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.Password,
                        IsActive = user.IsActive
                    });
                    res = "User added successfully";
                }
                else
                {
                    var data = await _context.Users.FindAsync(user.UserID);
                    if (data != null)
                    {
                        data.UserName = user.UserName;
                        data.Email = user.Email;
                        data.Password = user.Password;
                        data.IsActive = user.IsActive == null ? true : user.IsActive;
                        _context.Users.AddOrUpdate(data);
                        res = "User updated successfully";
                    }
                    else
                    {
                        res = "User not found";
                    }
                }

                await _context.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                return res=ex.Message;
            }
        }
    }
}
