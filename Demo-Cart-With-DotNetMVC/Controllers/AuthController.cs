using Demo_Cart_With_DotNetMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Cart_With_DotNetMVC.Controllers
{
    public class AuthController : Controller
    {
        public IConfiguration server;

        public AuthController(IConfiguration server)
        {
            this.server = server;
        }
        // GET: AuthController
        public ActionResult Signin()
        {
            return View("Views/Login/LoginPage.cshtml");
        }

       

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(AccountModel account)
        {
            string sql = "SELECT * FROM account WHERE username='" + account.userName +"' AND password='"+account.passWord+"'";
            Database db = new Database(sql, this.server);
            if (db.data.HasRows)
            {
         
                HttpContext.Session.SetString("username",account.userName);
                HttpContext.Session.SetString("password",account.passWord);
                return Redirect(Url.Action("HomePage", "Home"));
            }
            else
            {
                TempData["ThongBao"] = "Tài khoản hoặc mật khẩu không hợp lệ";
                return RedirectToAction(nameof(Signin));
                
            }

        }


        // GET: AuthController/Delete/5
        public ActionResult Signout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction(nameof(Signin));
        }

        // POST: AuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
