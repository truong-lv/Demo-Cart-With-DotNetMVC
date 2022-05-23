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
    public class CartController : Controller
    {
        public IConfiguration server;

        public CartController(IConfiguration server)
        {
            this.server = server;
        }

        // GET: CartController/Details/5
        public ActionResult Cart()
        {
            String username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                return Unauthorized("Bạn chưa đăng nhập");
            }

            List<BookModel> listBook= new List<BookModel>();
            string sql = "SELECT book.* FROM cart,book WHERE cart.username='"+username+"' AND cart.book_id=book.book_id";
            Database db = new Database(sql, this.server);
            if (db.data.HasRows)
            {
                while (db.data.Read())
                {

                    listBook.Add(new BookModel{
                        book_id = long.Parse(db.data[0].ToString()),
                        book_name = db.data[1].ToString(),
                        book_author = db.data[2].ToString(),
                        price = long.Parse(db.data[3].ToString())
                    });
                }
            }
            db.Close();
            ViewData["Title"] = "Giỏ hàng "+username;
            return Ok(listBook);
        }

        // POST: CartController/Create
        [HttpPost]
        public ActionResult Order([FromQuery]long bookId)
        {
            String username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                return Unauthorized("Bạn chưa đăng nhập");
            }

            string sql = "INSERT INTO cart VALUES ('" + username+"',"+bookId+")";
            try
            {
                Database db = new Database(sql, this.server);
                db.Close();
                return Ok("Thêm giỏ hàng thành công");
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //Thêm nhiều giỏ hàng với tham số là chuổi gồm các id của book
        [HttpPost]
        public ActionResult Orders([FromQuery]string listBookId)
        {
            String username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                return Unauthorized("Bạn chưa đăng nhập");
            }

            string sql = "INSERT INTO cart SELECT (SELECT username FROM account WHERE username='"+username+"'), book_id " +
                                            "FROM book WHERE book_id IN("+ listBookId+")";
            try
            {
                Database db = new Database(sql, this.server);
                db.Close();
                return Ok("Thêm giỏ hàng thành công");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: CartController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromQuery]long bookId)
        {
            String username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                return Unauthorized("Bạn chưa đăng nhập");
            }

            string sql = "DELETE FROM Cart WHERE username='"+username+"' AND book_id="+bookId;
            try
            {
                Database db = new Database(sql, this.server);
                db.Close();
                return Ok("Xóa giỏ hàng thành công");
            }
            catch (Exception e)
            {
                return NotFound("Xóa giỏ hàng thất bại");
            }
        }
    }
}
