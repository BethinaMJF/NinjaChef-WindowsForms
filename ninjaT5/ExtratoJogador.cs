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
    
    public partial class ExtratoJogador
    {
        public int id { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<int> pontos { get; set; }
        public Nullable<int> idOrigemPontos { get; set; }
    
        public virtual OrigemPontos OrigemPontos { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
