using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Fstorch.User.Application.Dtos.User
{
    public class UserCreation : UserCreationAndUpdationDto
    {

        /// <summary>
        /// 账号id
        /// </summary>
        public long Aid { get; set; }
    }
}
