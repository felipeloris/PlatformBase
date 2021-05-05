using Loris.Common;
using Loris.Resources;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Loris.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public int? PersonId { get; set; }

        [Display(Name = "lbl_user_external_id", ResourceType = typeof(BASERES))]
        [Required(ErrorMessageResourceName = "msg_field_not_informed", ErrorMessageResourceType = typeof(BASERES))]
        [StringLength(60, MinimumLength = 5, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string ExtenalId { get; set; }

        [Display(Name = "lbl_user_pwd", ResourceType = typeof(BASERES))]
        [Required(ErrorMessageResourceName = "msg_field_not_informed", ErrorMessageResourceType = typeof(BASERES))]
        [StringLength(24, MinimumLength = 6, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string Password { get; set; }

        [Display(Name = "lbl_user_nickname", ResourceType = typeof(BASERES))]
        [Required(ErrorMessageResourceName = "msg_field_not_informed", ErrorMessageResourceType = typeof(BASERES))]
        [StringLength(30, MinimumLength = 5, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string Nickname { get; set; }

        [Display(Name = "lbl_user_email", ResourceType = typeof(BASERES))]
        [Required(ErrorMessageResourceName = "msg_field_not_informed", ErrorMessageResourceType = typeof(BASERES))]
        [StringLength(30, MinimumLength = 5, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        [EmailAddress(ErrorMessageResourceName = "msg_invalid_format_email", ErrorMessageResourceType = typeof(BASERES))]
        public string Email { get; set; }

        [Display(Name = "lbl_user_language", ResourceType = typeof(BASERES))]
        public byte Language { get; set; } = (byte)Languages.Portuguese;

        [Display(Name = "lbl_user_note", ResourceType = typeof(BASERES))]
        [StringLength(500, MinimumLength = 0, ErrorMessageResourceName = "msg_invalid_field", ErrorMessageResourceType = typeof(BASERES))]
        public string Note { get; set; }

        [Display(Name = "lbl_roles", ResourceType = typeof(BASERES))]
        public ICollection<RoleDto> Roles { get; set; }
    }
}
