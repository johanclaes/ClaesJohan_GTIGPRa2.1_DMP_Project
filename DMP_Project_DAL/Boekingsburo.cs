//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMP_Project_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Boekingsburo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Boekingsburo()
        {
            this.Comedians = new HashSet<Comedian>();
        }
    
        public int id { get; set; }
        public string naam { get; set; }
        public string telefoon { get; set; }
        public string email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comedian> Comedians { get; set; }
    }
}
