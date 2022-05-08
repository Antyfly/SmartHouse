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
        public static SMEntities context = new SMEntities();

        public static void SaveChange()
        {
            context.SaveChanges();
        }
    }

    
}
