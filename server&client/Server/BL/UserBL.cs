using DAL;
using Entities.DTO;

namespace BL
{
    public static class UserBL
    {

        public static UserDto Login(string userMail,string password)
        {
            var user = UserDAL.Login(userMail, password);
            return user;
        }
        public static UserDto AddNewUser(UserDto userDto)
        {
            var user = UserDAL.AddNewUser(userDto);
            return user;
        }
    }
}

