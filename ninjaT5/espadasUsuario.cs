//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ninjaT5
{
    using System;
    using System.Collections.Generic;
    
    public partial class espadasUsuario
    {
        public int idEspada { get; set; }
        public int idUsuario { get; set; }
        public int id { get; set; }
    
        public virtual Espada Espada { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
