using Sky_Roamer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sky_Roamer
{
    public class DBContext: DbContext
    {
        public DBContext()
            : base("name=DBContextConnection")
        {

        }
        public virtual DbSet<User_Registeration> User_Registerations { get; set; }
        public virtual DbSet<Tour_Package> Tour_Packages { get; set; }
        public virtual DbSet<Book_Package> Book_Packages { get; set; }
        public virtual DbSet<Billing> Billings { get; set; }
        public virtual DbSet<Customer_Wallet> Customer_Wallets { get; set; }
    }
}