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
    
    public partial class Rooms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rooms()
        {
            this.OurControllers = new HashSet<OurControllers>();
        }
    
        public int IDRooms { get; set; }
        public int IDFlat { get; set; }
        public string NameRoom { get; set; }
        public Nullable<double> WidthRoom { get; set; }
        public Nullable<double> HeightRoom { get; set; }
    
        public virtual Flat Flat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OurControllers> OurControllers { get; set; }
    }
}
