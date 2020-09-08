using Sky_Roamer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Sky_Roamer.Controllers
{
    public class TravelAgentController : Controller
    {
        public string UserId;
         
        // GET: TravelAgent
        public ActionResult AgentProfile()
        {
            /*List<SelectListItem> item = new List<SelectListItem>()
            {
                new SelectListItem {Text = user.UserId, Value = user.UserId}
            };*/
            if (Session["Customer"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            User_Registeration user = Session["Customer"] as User_Registeration;
            Session["Travel_Agent_Id"] = user.UserId;
            UserId = user.UserId;
            return View(user);
        }

        [HttpGet]
        public ActionResult ViewNotifications()
        {
            /*List<Notification> Notifications = new List<Notification>();
            foreach (var item in CustomerController.Notifications)
            {
                if(item.AgentId == Session["Travel_Agent_Id"].ToString())
                {
                    Notifications.Add(item);
                }
            }
            Session["Notifications"] = Notifications;
            Notifications = null;*/
            DBContext context = new DBContext();
            if(Session["Travel_Agent_Id"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }
            string agentId = Session["Travel_Agent_Id"].ToString();
            var list = (from d in context.Book_Packages
                        where d.Tour_Package.Travel_Agent_Id == agentId
                        select d).ToList();
            
            return View(list);
        }

        [HttpGet]
        public ActionResult New_Package()
        {
            DBContext context = new DBContext();
            List<int> tour_Packages;
            tour_Packages= context.Tour_Packages.Select(c => c.PackageId).ToList();

            if (tour_Packages.Count == 0)
                return RedirectToAction("Add_Package");
            else
                return RedirectToAction("View_Package");
        }

        [HttpGet]
        public ActionResult Add_Package()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Add_Package(Tour_Package package)
        {
            DBContext context = new DBContext();
            context.Tour_Packages.Add(package);
            context.SaveChanges();
            User_Registeration user = context.User_Registerations.FirstOrDefault(m => m.UserId == package.Travel_Agent_Id);
            ViewBag.Message = String.Format("Details Added Succesfully");
            ViewBag.User = user;
            //return RedirectToAction("AgentProfile",user);
            return View();
        }

        public ActionResult View_Package()
        {
            DBContext db = new DBContext();
            string id = Session["Travel_Agent_Id"].ToString();
            var query = (from d in db.Tour_Packages
                         where d.Travel_Agent_Id == id
                         select d).ToList();          
            
            return View(query);
        }

        public ActionResult Details(int id)
        {
            DBContext db = new DBContext();
            Tour_Package package = db.Tour_Packages.FirstOrDefault(item => item.PackageId == id);
            return View(package);
        }

        public ActionResult Edit(int id)
        {
            DBContext context = new DBContext();
            Tour_Package tour = context.Tour_Packages.FirstOrDefault(m => m.PackageId == id);

            return View(tour);
        }

        [HttpPost]
        public ActionResult Edit(Tour_Package package)
        {
            DBContext context = new DBContext();
            context.Tour_Packages.FirstOrDefault(m => m.PackageId == package.PackageId).Package_Name = package.Package_Name;
            context.Tour_Packages.FirstOrDefault(m => m.PackageId == package.PackageId).Source = package.Source;
            context.Tour_Packages.FirstOrDefault(m => m.PackageId == package.PackageId).Destination = package.Destination;
            context.Tour_Packages.FirstOrDefault(m => m.PackageId == package.PackageId).Per_head_cost = package.Per_head_cost;
            context.Tour_Packages.FirstOrDefault(m => m.PackageId == package.PackageId).images = package.images;
            //Tour_Package pack = context.Tour_Packages.FirstOrDefault(m => m.PackageId == package.PackageId);
            context.SaveChanges();
            ViewBag.EditMessage = "Changes Saved Successfully";            
            return View();
        }

        public ActionResult User_Details(User_Registeration user_)
        {
            DBContext context = new DBContext();
            //User_Registeration user_ = context.User_Registerations.FirstOrDefault(m => m.UserId == userId);

            return View(user_);
        }
    }
}