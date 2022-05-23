using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Cart_With_DotNetMVC.Models
{
    public class ReviewModel
    {
        public String userName { get; set; }
        public long book_id { get; set; }
        public DateTime time { get; set; }
        public String content { get; set; }
    }
}
