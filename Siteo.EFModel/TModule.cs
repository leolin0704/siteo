//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Siteo.EFModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TModule()
        {
            this.TModule1 = new HashSet<TModule>();
            this.TPermissionnModules = new HashSet<TPermissionnModule>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentModuleID { get; set; }
        public string Key { get; set; }
        public int Order { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TModule> TModule1 { get; set; }
        public virtual TModule TModule2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TPermissionnModule> TPermissionnModules { get; set; }
    }
}