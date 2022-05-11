using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Entity;

namespace SmartHouse.Entity
{
    public class AppData
    {
        public static HomeEntities context = new HomeEntities();

        public static void SaveChange()
        {
            context.SaveChanges();
        }


    }


}
