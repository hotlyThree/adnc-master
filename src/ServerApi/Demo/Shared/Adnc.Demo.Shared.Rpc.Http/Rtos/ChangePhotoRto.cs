using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    public class ChangePhotoRto
    {
        public ChangePhotoRto(long id, string type, string photo) => (Id, Type, Photo) = (id, type, photo);

        public long Id { get; set; }

        public string Type { get; set; }

        public string Photo { get;set; }
    }
}
