using Sky_Roamer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace Sky_Roamer.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public static Book_Package package;
        public static User_Registeration Customer;
        public static List<Notification> Notifications = new List<Notification>();

        public ActionResult CustomerProfile()
        {
            User_Registeration user = Session["Customer"] as User_Registeration;
            ViewBag.CustomerName = user.First_name + " " + user.Last_name;
            Session["Customer ID"] = user.UserId;

            return View();
        }

        [HttpGet]
        public ActionResult View_Package(Tour_Package package)
        {
            DBContext context = new DBContext();
            /*Tour_Package tour_Package = context.Tour_Packages.FirstOrDefault(
                m => m.Source == package.Source && m.Destination == package.Destination);*/
            Session["Source"] = package.Source;
            Session["Destination"] = package.Destination;
            Session["SearchParam"] = package;
            var tour_Package = (from d in context.Tour_Packages
                                where d.Source == package.Source
                                && d.Destination == package.Destination
                                select d).ToList();
            if(tour_Package == null)
            {
                ViewBag.Error = "No Package Exist for these places";
            }
            Session["SearchList"] = tour_Package;
            return View(tour_Package);
        }

        public ActionResult Details(int id)
        {
            DBContext db = new DBContext();
            Tour_Package package = db.Tour_Packages.FirstOrDefault(m => m.PackageId == id);

            return View(package);
        }

        public ActionResult Book_Package(int id)
        {
            Session["PackageID"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult Book_Package(Book_Package booking)
        {
            /*DBContext context = new DBContext();
            context.Book_Packages.Add(booking);
            context.SaveChanges();*/
            package = booking;


            return RedirectToAction("Generate_Bill", booking);
        }

        [HttpGet]
        public ActionResult View_Booking()
        {
            DBContext context = new DBContext();
            string id = Session["Customer ID"].ToString();
            var booking = (from d in context.Book_Packages
                           where d.CustomerId == id
                           select d).ToList();
            return View(booking);
        }

        public ActionResult BookingDetails(int id)
        {
            DBContext context = new DBContext();
            Book_Package book = context.Book_Packages.FirstOrDefault(m => m.BookingId == id);

            return View(book);
        }

        public ActionResult Generate_Bill(Book_Package booking)
        {
            DBContext context = new DBContext();
            Session["BookingID"] = booking.BookingId;
            Session["BillDate"] = DateTime.Now;
            int number = booking.Number_of_persons;
            Tour_Package nop = context.Tour_Packages.FirstOrDefault(m => m.PackageId == booking.PackageId);
            int amt = Convert.ToInt32(nop.Per_head_cost);
            Session["TotalAmount"] = number * amt;
            return View();
        }

        public ActionResult CancelBooking(int id)
        {
            DBContext context = new DBContext();
            context.Book_Packages.FirstOrDefault(m => m.BookingId == id).Status = "Cancelled";
            Book_Package book = context.Book_Packages.FirstOrDefault(m => m.BookingId == id);
            Tour_Package pack = context.Tour_Packages.FirstOrDefault(m => m.PackageId == book.PackageId);
            ViewBag.CancelMessage = "Booking Cancelled! Your money will soon be refunded to your wallet.";
            context.SaveChanges();

            return RedirectToAction("View_Booking");
        }

        public ActionResult Edit(int id)
        {
            DBContext context = new DBContext();
            Book_Package booking = context.Book_Packages.FirstOrDefault(m => m.BookingId == id);
            Session["PackageId"] = id;
            Session["Source"] = booking.Source;
            Session["Destination"] = booking.Destination;
            return View(booking);
        }

        [HttpPost]
        public ActionResult Edit(Book_Package package)
        {
            DBContext context = new DBContext();
            context.Book_Packages.FirstOrDefault(m => m.BookingId == package.BookingId).Date_of_Travelling = package.Date_of_Travelling;
            context.Book_Packages.FirstOrDefault(m => m.BookingId == package.BookingId).Number_of_persons = package.Number_of_persons;
            Tour_Package pack = context.Tour_Packages.FirstOrDefault(m => m.PackageId == package.PackageId);
            context.SaveChanges();
            ViewBag.EditMessage = "Details Updated Successfully";
            return View();
        }

        public ActionResult Add_Money()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Money(PaymentCard card)
        {
            DBContext context = new DBContext();

            if(card!=null)
            {
                Customer_Wallet wallet = context.Customer_Wallets.FirstOrDefault(m => m.CustomerId == Session["Customer ID"].ToString());
            }
            return View();
        }
    }
}