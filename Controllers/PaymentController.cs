using Sky_Roamer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sky_Roamer.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public static DBContext context = new DBContext();
        public static Billing Bill;

        [HttpGet]
        public ActionResult InitializePayment(Billing bill)
        {
            Bill = bill;
            if (bill.Mode_Of_Payment == "Debit Card" || bill.Mode_Of_Payment == "Credit Card")
                return RedirectToAction("AddMoneyViaCard",bill);
            return RedirectToAction("AddMoneyViaNet",bill);
        }

        public ActionResult AddMoneyViaCard(Billing bill)
        {
            User_Registeration cust = Session["Customer"] as User_Registeration;
            string custId = cust.UserId;
            Customer_Wallet wallet = context.Customer_Wallets.FirstOrDefault(w => w.CustomerId == custId);
            Session["WalletBalance"] = wallet.Balance;
            if(wallet.Balance >= bill.total_Amt)
            {
                int newBal = wallet.Balance - bill.total_Amt;
                context.Customer_Wallets.FirstOrDefault(w => w.CustomerId == custId).Balance = newBal;
                context.SaveChanges();
                Session["Display"] = "Rs. "+bill.total_Amt + " has been Debited from your Wallet. Your remaining Balance is Rs." + newBal;

                return RedirectToAction("ConfirmBooking");
            }
            else
            {
                int reqMoney = bill.total_Amt - wallet.Balance;
                Session["Display"] = "Add Rs. "+reqMoney +" to your card.";
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddMoneyViaCard(PaymentCard card)
        {
            if(card!=null)
            {
                CustomerController.package.Status = "Booked";
                context.Book_Packages.Add(CustomerController.package);
                context.Billings.Add(Bill);
                context.SaveChanges();
                ViewBag.Message = "Booking Successful";
            }
            return View();
        }

        public ActionResult ConfirmBooking()
        {
            CustomerController.package.Status = "Booked";
            context.Book_Packages.Add(CustomerController.package);
            context.Billings.Add(Bill);
            context.SaveChanges();
            ViewBag.ConfirmMessage = "Booking Successful";

            return View();
        }
    }
}