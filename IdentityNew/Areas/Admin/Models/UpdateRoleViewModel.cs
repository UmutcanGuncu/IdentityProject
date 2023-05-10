using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityNew.Areas.Admin.Models
{
	public class UpdateRoleViewModel
	{

        public string Id { get; set; } = null!;
        [Display(Name = "Rol İsmi")]
        [Required(ErrorMessage = "Rol İsmi Boş Geçilemez")]
        public string Name { get; set; } = null!;
    }
}

