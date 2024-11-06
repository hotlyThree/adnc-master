using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    public class MessageUpdationRto
    {
        public MessageUpdationRto(long id, string path) 
            => (Id, Path) = (id, path);

        public long Id { get; set; }

        public string Path { get; set; } = string.Empty;
    }
}
