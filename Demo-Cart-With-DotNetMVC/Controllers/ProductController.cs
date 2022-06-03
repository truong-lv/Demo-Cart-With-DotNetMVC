using Demo_Cart_With_DotNetMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Cart_With_DotNetMVC.Controllers
{
    public class ProductController : Controller
    {
        public IConfiguration server;
        private IMemoryCache memoryCache;
        public ProductController(IConfiguration server, IMemoryCache memoryCache) {
            this.server = server;
            this.memoryCache = memoryCache;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }
        public String test() => "xin chao productDetail";
        // GET: ProductController/Details/5
        
        
        public ActionResult ProductDetail(int id)
        {
            BookModel bookModel;
            if(!memoryCache.TryGetValue<BookModel>("bookModel", out BookModel bookModelCache)){
                bookModel = new BookModel();
                string sql = "SELECT * FROM book WHERE book_id=" + id;
                Database db = new Database(sql, this.server);
                if (db.data.HasRows)
                {
                    while (db.data.Read())
                    {
                        bookModel.book_id = long.Parse(db.data[0].ToString());
                        bookModel.book_name = db.data[1].ToString();
                        bookModel.book_author = db.data[2].ToString();
                        bookModel.price = long.Parse(db.data[3].ToString());
                    }
                }
                db.Close();

                //cấu hình tgian cho cache
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(5);// hết hạn sau 5p dù có truy cập hay ko
                options.SlidingExpiration = TimeSpan.FromMinutes(2);// hết hạn sau 2p nếu ko có truy cập nào

                memoryCache.Set<BookModel>("bookModel", bookModel);
                ViewBag.checkCache = "Lấy từ DATABASE";
            }
            else
            {
                ViewBag.checkCache = "Lấy từ CACHE";
                bookModel = memoryCache.Get<BookModel>("bookModel");
            }
            
            //other: bookModel=memoryCache.GetOrCreate<BookModel>("bookModel", entry =>{.....})

            ViewData["Title"] = bookModel.book_name;
            return View(bookModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
