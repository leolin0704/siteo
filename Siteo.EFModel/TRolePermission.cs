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
    
    public partial class TRolePermission
    {
        public int ID { get; set; }
        public int PermissionID { get; set; }
        public int RoleID { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    
        public virtual TPermission TPermission { get; set; }
        public virtual TRole TRole { get; set; }
    }
}
