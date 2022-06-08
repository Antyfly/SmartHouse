using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SmartHouse.Entity
{
   public partial class UserLogins
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int IsDelete { get; set; }
        public string Phone { get; set; }

    }

    public partial class Home
    {
        public string Image
        {  
            get
            {
                string image = ImagePath;
                return Directory.GetCurrentDirectory() + image.ToString();
            }
        }
    }
}
