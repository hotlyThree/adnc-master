using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.User
{
    public class UserChangeStatusDto : IDto
    {
        public long[] Ids { get; set; }

        public string Status { get; set; }
    }
}
