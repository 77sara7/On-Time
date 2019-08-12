using Entities;
using System;
using System.Linq;
using Entities.DTO;
using AutoMapper;

namespace DAL
{
    public static partial class UserDAL
    {
    
    //   static public DBContext db = new DBContext();
    public static UserDto Login(string userMail, string password)
        {
        
            //לעשות את הבדיקה באנגולר
            if (string.IsNullOrEmpty(userMail) || string.IsNullOrEmpty(password))
                
                return new UserDto
                {
                    IsAuthorized = false,
                    ErrorMessage = "לא התקבלו נתונים"
                };
            using (var db = new DBContext())
            {
                try
                {

                    var user = db.Users.FirstOrDefault(user_found => user_found.password.Equals(password) && user_found.mail.Equals(userMail));
                    if (user != null)
                        return  Mapper.Map<User, UserDto>(user);              
                    else
                        return new UserDto
                        {
                            IsAuthorized = false,
                            ErrorMessage = "שם משתמש או סיסמה שגויים"
                        };
                }
                catch (Exception ex)
                {
                    return new UserDto
                    {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                    };
                }
            }
        }

        public static UserDto AddNewUser(UserDto userDto)
        {
            //לעשות את הבדיקה באנגולר
            //if (string.IsNullOrEmpty(userMail) || string.IsNullOrEmpty(password))
            //    return new UserDto
            //    {
            //        IsAuthorized = false,
            //        ErrorMessage = "לא התקבלו נתונים"
            //    };
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
                    return new UserDto
                    {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                    };
                }
            }
        }
    }
}