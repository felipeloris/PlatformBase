using Loris.Common;
using Loris.Common.Extensions;
using Loris.Resources;
using System.ComponentModel.DataAnnotations;

namespace Loris.Application.Dtos
{
    public class RoleResourceDto
    {
        public int RoleId { get; set; }

        [Display(Name = "lbl_role", ResourceType = typeof(BASERES))]
        public virtual RoleDto Role { get; set; }

        public int ResourceId { get; set; }

        [Display(Name = "lbl_resource", ResourceType = typeof(BASERES))]
        public virtual ResourceDto Resource { get; set; }

        [Display(Name = "lbl_access_permission", ResourceType = typeof(BASERES))]
        public AccessPermission Permissions { get; set; }

        #region AccessPermission's

        [Display(Name = "lbl_can_create", ResourceType = typeof(BASERES))]
        public bool CanCreate => Permissions.CanCreate();

        [Display(Name = "lbl_can_read", ResourceType = typeof(BASERES))]
        public bool CanRead => Permissions.CanRead();

        [Display(Name = "lbl_can_update", ResourceType = typeof(BASERES))]
        public bool CanUpdate => Permissions.CanUpdate();

        [Display(Name = "lbl_can_delete", ResourceType = typeof(BASERES))]
        public bool CanDelete => Permissions.CanDelete();

        [Display(Name = "lbl_can_report", ResourceType = typeof(BASERES))]
        public bool CanReport => Permissions.CanReport();

        [Display(Name = "lbl_can_all", ResourceType = typeof(BASERES))]
        public bool CanAll => Permissions.CanAll();

        #endregion
    }
}
