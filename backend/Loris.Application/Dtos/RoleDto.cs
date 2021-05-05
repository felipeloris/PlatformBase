using Loris.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Loris.Application.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }

        [Display(Name = "lbl_role_name", ResourceType = typeof(BASERES))]
        [Required(ErrorMessageResourceName = "msg_field_not_informed", ErrorMessageResourceType = typeof(BASERES))]
        [StringLength(50, MinimumLength = 5, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string Name { get; set; }


        [Display(Name = "lbl_resources", ResourceType = typeof(BASERES))]
        public ICollection<RoleResourceDto> Resources { get; set; }
    }
}
