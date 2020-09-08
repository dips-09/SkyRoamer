using Sky_Roamer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sky_Roamer.Controllers
{
    public class HomeController : Controller
    {
        DBContext context;
        public static User_Registeration User;
        public ActionResult Index()
        {
            context = new DBContext();
            foreach (var item in context.Book_Packages)
            {
                if(item.Date_of_Travelling >= DateTime.Now)
                {
                    item.Status = "Completed";
                }
            }
            context.SaveChanges();
            return View();
        }
        // GET: Home
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User_Registeration user)
        {
            /*if(ModelState.IsValid)
            {*/
                context = new DBContext();
                if (user != null)
                {
                    context.User_Registerations.Add(user);
                    
                if (user.UserCategory == Category.Customer)
                {
                    Customer_Wallet wallet = new Customer_Wallet()
                    {
                        CustomerId = user.UserId,
                        Balance = 0
                    };
                    context.Customer_Wallets.Add(wallet);
                }
                    context.SaveChanges();
                    ViewBag.Message = String.Format("New User Created Succesfully");
                }


                return View();
            /*}
            ViewBag.Message = "UserId Already Taken";
            return View();*/
            
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User_Registeration user)
        {
            User = user;
            Session["Customer"] = user;
            context = new DBContext();
            User_Registeration user_ = context.User_Registerations.FirstOrDefault(User => User.UserId == user.UserId && User.Password == user.Password);
            if(user_!=null)
            {
                if(user_.UserCategory == Category.Customer)
                {
                    CustomerController Cust = new CustomerController();
                    return RedirectToAction("CustomerProfile","Customer");
                }
                else if(user_.UserCategory == Category.Travel_Agent)
                {
                    return RedirectToAction("AgentProfile","TravelAgent", user_);
                }
                else
                {
                    return RedirectToAction("AdminProfile","Admin", user_);
                }
                
            }
            else
            {
                ViewBag.Message = String.Format("Either User Name or Password is incorrect!");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        

    }
}