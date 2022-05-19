using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SmartHouse.Entity.AppData;

namespace SmartHouse.Entity
{
    public partial class ScenariosView
    {
        public string WeeksDay
        {
            get
            {
                List<ScenariosView> productSales = context.ScenariosView.ToList();
                int dayWeek = 0;
                foreach (var i in productSales)
                {
                    dayWeek = productSales.Select(s => s.DayWeek).FirstOrDefault();
                }

                if (dayWeek == 0)
                {
                    return "Понедельник";
                }
                else if (dayWeek == 1)
                {
                    return "Вторник";
                }
                else if (dayWeek == 2)
                {
                    return "Среда";
                }
                else if (dayWeek == 3)
                {
                    return "Четверг";
                }
                else if (dayWeek == 4)
                {
                    return "Пятница";
                }
                else if (dayWeek == 5)
                {
                    return "Суббота";
                }
                else
                {
                    return "Воскресенье";
                }
            }
        }
    }
}
