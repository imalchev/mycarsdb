using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCarsDb.Data.Models
{
    public class UserToVehicle
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
