//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Collect.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class CE
    {
        public int Id { get; set; }
        public int cid { get; set; }
        public int eid { get; set; }
    
        public virtual Creq Creq { get; set; }
        public virtual Emp Emp { get; set; }
    }
}