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
    
    public partial class Comedian
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comedian()
        {
            this.EventComedians = new HashSet<EventComedian>();
        }
    
        public int id { get; set; }
        public string naam { get; set; }
        public string voornaam { get; set; }
        public Nullable<System.DateTime> geboortedatum { get; set; }
        public Nullable<int> boekingsburoid { get; set; }
    
        public virtual Boekingsburo Boekingsburo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventComedian> EventComedians { get; set; }
    }
}