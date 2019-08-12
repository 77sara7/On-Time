using Entities;
using System;
using System.Linq;
using Entities.DTO;
using AutoMapper;

namespace DAL
{
    public static partial class UserDAL
    {
    public static UserDto Login(string userMail,string password)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var user = db.Users.FirstOrDefault(user_found =>user_found.mail.Equals(userMail)&& user_found.password.Equals(password));
                    if (user != null)
                        return Mapper.Map<User, UserDto>(user);
                    else
                        return null;
                       
                }
                catch (Exception ex)
                {
                    return null;
                  
                }
            }
        }

        public static UserDto AddNewUser(UserDto userDto)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var user = Mapper.Map<UserDto, User>(userDto);
                    db.Users.Add(user);
                    db.SaveChanges();
                    return userDto;
                  
                }
                catch (Exception ex)
                {
                    return null;
                   
                }
            }
        }
    }
}