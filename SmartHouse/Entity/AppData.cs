using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Entity;

namespace SmartHouse.Entity
{
    public class AppData
    {
        public static SmartHome context = new SmartHome();

        public static void SaveChange()
        {
            context.SaveChanges();
        }
    }

    
}
