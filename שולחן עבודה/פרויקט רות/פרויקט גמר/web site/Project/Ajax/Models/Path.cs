//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ajax.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Path
    {
        public int requestId { get; set; }
        public int ordinalNum { get; set; }
        public string path1 { get; set; }
    
        public virtual Request Request { get; set; }
    }
}
