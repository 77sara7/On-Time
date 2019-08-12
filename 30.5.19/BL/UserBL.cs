using DAL;
using Entities.DTO;

namespace BL
{
    public static class UserBL
    {

        public static UserDto Login(string userName, string password)
        {
            var user = UserDAL.Login(userName, password);
          //  if (user.IsAuthorized)

                //UserEntranceDAL.AddUserEntrance(user.UserId);
            return user;
        }
        public static UserDto AddNewUser(UserDto userDto)
        {
            var user = UserDAL.AddNewUser(userDto);
            return user;
        }
    }
}

