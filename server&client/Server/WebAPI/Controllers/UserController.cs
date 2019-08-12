using BL;
using Entities.DTO;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {

        [HttpGet]
        [Route("api/User/Login")]
        public UserDto Login(string userMail, string password)
        {
            UserDto userDto = UserBL.Login(userMail, password);         
            return userDto;
        }

        [HttpPost]
        [Route("api/User/AddNewUser")]
        public UserDto AddNewUser(UserDto userDto)
        {
            return UserBL.AddNewUser(userDto);
        }

    }
}
