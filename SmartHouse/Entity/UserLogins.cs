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
    
    public partial class UserLogins
    {
        public int IDUserLogins { get; set; }
        public int IDUsers { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string KeyWord { get; set; }
        public int IDRole { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Role Role { get; set; }
    }
}
