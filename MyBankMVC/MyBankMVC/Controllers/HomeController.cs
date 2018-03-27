using MyBankMVC.Business_Layer;
using MyBankMVC.Models.Domain_Models;
using MyBankMVC.Models.View_Models;
using MyBankMVC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBankMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        BusinessLayer b1 = new BusinessLayer();
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel lm = new LoginModel();
            return View(lm);
        }

        [Authorize(Roles ="Manager")]
        public ActionResult NewAccounts()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login (LoginModel L1)
        {
            
            if (ModelState.IsValid)
            {
                //Getting the user inputs
                string uname = L1.UserName;
                string pass = L1.Password;
                //Retriving the CheckingAccNumber & Login Validation
                string accno = b1.IsValidUser(uname, pass);
                if (accno != null)
                {
                    //Store the user information in the cookie and send it to browser
                    UserInfo ui = new UserInfo();
                    ui.username = uname;
                    ui.chkAccNumber = accno;
                    HttpCookie hc = new HttpCookie("UserInfo");
                    hc["USERDATA"] = ui.LosSerialize();
                    Response.Cookies.Add(hc);
                    //Getting the user roles and depositing in the cookie using form authentication technique
                    string roles = b1.GetUserRoles(uname);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, uname, DateTime.Now, DateTime.Now.AddMinutes(30), false, roles);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);
                   string redirurl = FormsAuthentication.GetRedirectUrl(uname, true);
                    if (redirurl == "/default.aspx")
                        redirurl = "/home/index";
                    return Redirect(redirurl);
                    
                }
                else
                {
                    ViewBag.Message = "Invalid Login";
                }
            }
            return View(L1);
        }

        [Authorize(Roles ="Customer")]
        public ActionResult TransferHistory()
        {
            XferHistory xf = new XferHistory();
            int accno = 0;
            //Retriving Data from cookie
            HttpCookie ck = Request.Cookies["UserInfo"];
            if (ck != null)
            {
                UserInfo ui = (UserInfo)ck["USERDATA"].LosDeserialize();
                accno = int.Parse(ui.chkAccNumber);
                ViewBag.One = ui.username;
                ViewBag.two = ui.chkAccNumber;
            }
            List<TransferHistory> xferlist = b1.GetTransferHistory(accno);
            return View(xferlist);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult ViewBalance()
        {
            Balances b = new Balances();
            return View(b);
        }
        [HttpPost]
        [Authorize(Roles ="Customer")]
        public ViewResult ViewBalance(Balances b2)
        {
            string accno = null;
            Balances b = new Balances();
            //Retriving Data from cookie
            HttpCookie ck = Request.Cookies["UserInfo"];
            if (ck != null)
            {
                UserInfo ui = (UserInfo)ck["USERDATA"].LosDeserialize();
                accno = ui.chkAccNumber;
            }
            b.savbalanceno = b1.getSavAccNo(accno);
            string acc = b.savbalanceno;
            b.chkbalance = b1.chkCheckingAcc(accno);
            b.savbalance = b1.chkSavingsAcc(acc);
            ViewBag.chk = b.chkbalance;
            ViewBag.save = b.savbalance;
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult TransferAmount()
        {
            return View();
        }

        [HttpPost]
        public ViewResult TransferAmount(Transfer t1)
        {
            try
            {
                bool ans;
                string Chkaccno = "";
                string Savaccno = "";
                HttpCookie ck = Request.Cookies["UserInfo"];
                if (ck != null)
                {
                    UserInfo ui = (UserInfo)ck["USERDATA"].LosDeserialize();
                    Chkaccno = ui.chkAccNumber;
                    Savaccno = b1.getSavAccNo(Chkaccno);
                }
                decimal amount = t1.amount;
                ans = b1.TransferCheckingToSaving(amount, Chkaccno, Savaccno);
                if (ans)
                {
                    ViewBag.Message = "Transfer Successful";
                }
                else
                {
                    ViewBag.Message = "Unsuccessful Transfer";
                }
            }

            catch(Exception Ex)
            {
                ViewBag.Message = Ex.Message;
            }
            
            return View(t1);
        }

    }
}