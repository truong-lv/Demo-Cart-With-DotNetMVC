using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Cart_With_DotNetMVC.Models
{
    public class AccountModel
    {
        public List<AccountModel> ListAccountModels = new List<AccountModel>();

        public String userName { get; set; }
        public String passWord { get; set; }
    }
}
