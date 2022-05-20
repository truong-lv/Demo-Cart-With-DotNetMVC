using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Cart_With_DotNetMVC.Models
{
    public class BookModel
    {
        public long book_id { get; set; }
        public String book_name { get; set; }
        public String book_author { get; set; }
        public long price { get; set; }
    }
}
