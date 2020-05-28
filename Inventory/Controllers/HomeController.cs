using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;
namespace Inventory.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        StockDataDataContext sd = new StockDataDataContext();
        public ActionResult Index()
        {
            Session["Password"] = "Admin";
            return View();
        }
        public ActionResult Toys()
        {
            return View(sd.Stocks.Where(x=>x.Type=="Toys"));
        }
        public ActionResult PasswordAdd()
        {
            string s = Session["Password"].ToString();
            
            if (s!= null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult PasswordEdit(int id)
        {
            if (id>0)
            {
                string s = Session["Password"].ToString();
                Session["ID"] = id;
                if (s != null)
                {
                    return View(sd.Stocks.First(x => x.Id == id));
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Add()
        {
            Session["Password"] = null;
            string name = Request["Name"];
            string type = Request["Type"];
            string brand = Request["Brand"];
            string price = Request["Price"];
            Stock s = new Stock();
            s.Name = name;
            s.Type = type;
            s.Brand=brand;
            s.Price=price;
            sd.Stocks.InsertOnSubmit(s);
            sd.SubmitChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult AddDone()
        {
            if(Session["Password"]!=null)
            {
            string check_pass = Request["Pass"];
            string s=Session["Password"].ToString();
                if (s== check_pass)
                {
                    Session["Password"] = null;
                    return View();
                }
                else
                {
                    return RedirectToAction("PasswordAdd");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        
           
        }
        public ActionResult EditDone(int id)
        {
            if (id >0)
            {
                if(Session["Password"]!=null)
                { 
                string check_pass = Request["Pass"];
                string s = Session["Password"].ToString();
              
                    if (s == check_pass)
                    {
                        Session["Password"] = null;
                        return View(sd.Stocks.First(x => x.Id == id));
                    }
                    else
                    {
                        return Redirect("/Home/PasswordEdit/" + id.ToString());
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            if (id>0)
            {
                var s = sd.Stocks.First(x => x.Id == id);
                // Session["ID"] = null;
                string name = Request["Name"];
                string type = Request["Type"];
                string brand = Request["Brand"];
                string price = Request["Price"];

                s.Name = name;
                s.Type = type;
                s.Brand = brand;
                s.Price = price;

                sd.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Cosmetics()
        {
            return View(sd.Stocks.Where(x => x.Type == "Cosmetics"));
        }
        public ActionResult Crockery()
        {
            return View(sd.Stocks.Where(x => x.Type == "Crockery"));
        }
    }
}
