using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Repository.Entities
{
    public class Sys_Program : EfEntity
    {
        public string Appid { get; set; }

        public string AppSecret { get; set; }

        public string Label { get; set; }
    }
}
