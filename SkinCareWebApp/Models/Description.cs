//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkinCareWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Description
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Description()
        {
            this.ActionDatas = new HashSet<ActionData>();
        }
    
        public int DescriptionId { get; set; }
        public string Description1 { get; set; }
        public Nullable<int> Type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActionData> ActionDatas { get; set; }
        public virtual DescriptionType DescriptionType { get; set; }
    }
}
