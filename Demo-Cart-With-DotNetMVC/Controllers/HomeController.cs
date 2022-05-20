using Demo_Cart_With_DotNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Cart_With_DotNetMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IConfiguration server;
        public HomeController(IConfiguration server)
        {
            this.server = server;
        }

        public IActionResult HomePage()
        {
            List<BookModel> listBook = new List<BookModel>();

            string sql = "SELECT * FROM book";
            Database db = new Database(sql, this.server);
            if (db.data.HasRows)
            {
                while (db.data.Read())
                {
                    listBook.Add(new BookModel
                    {
                        book_id = long.Parse(db.data[0].ToString()),
                        book_name = db.data[1].ToString(),
                        book_author = db.data[2].ToString(),
                        price = long.Parse(db.data[3].ToString())
                    });
                }
            }
            db.Close();
            return View(listBook);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
