//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterLibrary.Models.DataProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class DAYKE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DAYKE()
        {
            this.SACHes = new HashSet<SACH>();
        }
    
        public int MADAY { get; set; }
        public string TENDAY { get; set; }
        public Nullable<int> IDTANG { get; set; }
    
        public virtual TANG TANG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SACH> SACHes { get; set; }
    }
}
