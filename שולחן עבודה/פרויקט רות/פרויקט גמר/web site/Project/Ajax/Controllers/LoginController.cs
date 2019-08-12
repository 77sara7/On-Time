using Ajax.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ajax.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        TasksEntities db = new TasksEntities();

        public ActionResult Login()
        {
            ViewBag.message = 0;
            return View();
        }
        public ActionResult LoginUser(string userId, string password)
        {
            User user = db.User.FirstOrDefault(u => u.userId.Equals(userId));
            if ((user != null && password.Equals(user.password)) )
            {
                setCurrentUser(user);
                           }
            else if(user != null)
            {
                ViewBag.message = "wrong password!";
                return View("../Login/Login");
            }
             else
            {             
                return NewUser();
            }
            return View("../Home/Menu");
        }

    
        public ActionResult Logout()
        {
            Session["user"] = null;
            return View("../Home/Menu");
        }
        [HttpGet]
        public ActionResult NewUser()
        {
            ViewBag.CreditCompenyId = new SelectList(db.CreditCompeny, "CreditCompenyId", "name");
            return View("NewUser");
        }
        [HttpPost]
        public ActionResult NewUser(UserAPayment obj)
        {
            db.User.Add(obj.User);
            db.SaveChanges();
            obj.User.PaymentDetails = obj.payment;
            obj.payment.userId = obj.User.userId;
            obj.payment.CreditCompenyId = obj.CreditCompenyId;
            obj.payment.CreditCompeny = db.CreditCompeny.Find(obj.payment.CreditCompenyId);
            db.PaymentDetails.Add(obj.payment); 
            db.Entry(obj.User).State = System.Data.EntityState.Unchanged;
            db.SaveChanges();         
            setCurrentUser(obj.User);
           

            return View("../Home/Menu");
        }

        private void setCurrentUser(User user)
        {
            Session["user"] = user.userId;
            Session["userName"] = user.firstName + " " + user.lastName;
        }
    }
}
