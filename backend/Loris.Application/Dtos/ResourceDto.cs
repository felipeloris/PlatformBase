using Loris.Resources;
using System.ComponentModel.DataAnnotations;

namespace Loris.Application.Dtos
{
    public class ResourceDto
    {
        public int Id { get; set; }

        [Display(Name = "lbl_resource_code", ResourceType = typeof(BASERES))]
        [Required(ErrorMessageResourceName = "msg_field_not_informed", ErrorMessageResourceType = typeof(BASERES))]
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string Code { get; set; }

        [Display(Name = "lbl_resource_dictionary", ResourceType = typeof(BASERES))]
        [Required(ErrorMessageResourceName = "msg_field_not_informed", ErrorMessageResourceType = typeof(BASERES))]
        [StringLength(30, MinimumLength = 0, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string Dictionary { get; set; }

        [Display(Name = "lbl_resource_description", ResourceType = typeof(BASERES))]
        [StringLength(500, MinimumLength = 0, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string Description { get; set; }
    }
}
