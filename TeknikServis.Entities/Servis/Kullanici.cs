using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entities.Servis
{
    public class Kullanici:IdentityUser<string>
    {
        public string Ad { get; set; }
     
        public bool Aktif { get; set; }


    }
}
