//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartHouse.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Home
    {
        public int IDRooms { get; set; }
        public int IDFlat { get; set; }
        public string NameRoom { get; set; }
        public Nullable<double> WidthRoom { get; set; }
        public Nullable<double> HeightRoom { get; set; }
        public int IDOurControllers { get; set; }
        public int IDScenarios { get; set; }
        public int IDDevice { get; set; }
        public System.TimeSpan Time { get; set; }
        public bool Action { get; set; }
        public int DayWeek { get; set; }
        public string Title { get; set; }
        public int IDTypeConnection { get; set; }
        public string FlatName { get; set; }
        public int IDUser { get; set; }
        public double Size { get; set; }
    }
}