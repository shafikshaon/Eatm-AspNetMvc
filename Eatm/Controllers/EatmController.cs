using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Eatm.Models;
using Microsoft.Ajax.Utilities;

namespace Eatm.Controllers
{
    public class EatmController : Controller
    {
        private ApplicationDbContext _context;
        public EatmController()
        {
            _context = new ApplicationDbContext();    
        }

        public ActionResult Login()
        {
            if (Session["LoginId"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            var customer = _context.Accounts.SingleOrDefault(c =>
                c.CardNumber == account.CardNumber && c.PinNumber == account.PinNumber);

            if (customer == null)
            {
                ViewBag.LoginError = "Invalid Card Number or Pin Number";
                return View("Login");
            }
            Session["LoginId"] = customer.Id;
            return RedirectToAction("Dashboard", new {id = Session["LoginId"] });
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult Dashboard(int id)
        {
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult BalanceCheck(int id)
        {
            var loginId = (int)Session["LoginId"];
            if (loginId != id)
            {
                ViewBag.BalanceError = "You are not authorized";
                return View();
            }
            var balance = _context.Accounts.SingleOrDefault(c => c.Id == id);
            return View(balance);
        }

        public ActionResult Withdraw(Transaction transaction, int id)
        {
            var loginId = (int)Session["LoginId"];
            if (loginId != id)
            {
                ViewBag.WithdrawError = "You are not authorized";
                return View();
            }

            var withdraw = _context.Accounts.SingleOrDefault(c => c.Id == loginId);
            var currentDate = DateTime.Today;
            var transactionAccount = _context.Transactions.FirstOrDefault(c => c.AccountId == loginId);
            if (transactionAccount != null)
            {
                var transactionDate = transactionAccount.DateTime.Date;
                var transactionNo = _context.Transactions.Count(c => c.AccountId == loginId && currentDate == transactionDate);
                if (transactionNo <= 3)
                {
                    return View();
                }
                else
                {
                    ViewBag.TransactionLimit = "Daily transaction limit exceed";
                    return RedirectToAction("Dashboard", new { id = Session["LoginId"] });
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(Account account, Transaction transaction, int id)
        {
            var loginId = (int)Session["LoginId"];
            if (loginId != id)
            {
                ViewBag.WithdrawError = "You are not authorized";
                return View();
            }

            var withdraw = _context.Accounts.SingleOrDefault(c => c.Id == loginId);
            //var currentDate = DateTime.Today;
            //var transactionAccount = _context.Transactions.FirstOrDefault(c => c.AccountId == loginId);
            //var transactionDate = transactionAccount.DateTime.Date;
            //var transactionNo = _context.Transactions.Count(c => c.AccountId == loginId && currentDate == transactionDate);
            var exceedBalance = withdraw.Balance - account.Balance;
            if (withdraw != null)
            {
                if (exceedBalance >= 500)
                {
                    withdraw.Balance -= account.Balance;
                    transaction.AccountId = account.Id;
                    transaction.Amount = account.Balance;
                    transaction.DateTime = DateTime.Now;
                    _context.Transactions.Add(transaction);
                    _context.SaveChanges();
                }
                else
                {
                    ViewBag.LowBalance = "You must keep at least 500 Tk";
                }
            }

            ViewBag.WithdrawBalance = account.Balance + "balance withdraw successfull";
            return RedirectToAction("Dashboard", new { id = Session["LoginId"] });
        }
    }
}