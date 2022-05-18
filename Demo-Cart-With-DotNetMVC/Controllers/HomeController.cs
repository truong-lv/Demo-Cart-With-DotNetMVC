using Demo_Cart_With_DotNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult HomePage()
        {
            BookModel listBook = new BookModel();
            listBook.ListBookModels.Add(new BookModel
            {
                book_id=1,
                book_name="Đắt nhân tâm",
                book_author="Nguyễn Nhật Ánh",
                price=100000
            });
            listBook.ListBookModels.Add(new BookModel
            {
                book_id = 2,
                book_name = "Nhà giả kim",
                book_author = "Nguyễn Nhật Ánh",
                price = 100000
            });
            listBook.ListBookModels.Add(new BookModel
            {
                book_id = 3,
                book_name = "Chiếc thuyền ngoài xa",
                book_author = "Tô Hoài",
                price = 50000
            });
            listBook.ListBookModels.Add(new BookModel
            {
                book_id = 4,
                book_name = "Code dạo ký sự",
                book_author = "Phạm Huy Hoàng",
                price = 100000
            });
            listBook.ListBookModels.Add(new BookModel
            {
                book_id = 5,
                book_name = "Tony buổi sáng",
                book_author = "Nguyễn Nhật Ánh",
                price = 150000
            });
            List<BookModel> model = listBook.ListBookModels.ToList();
            return View(model);
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
