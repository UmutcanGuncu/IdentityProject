using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNew.Areas.Admin.Models
{
	public class CreateRoleViewModel
	{
		[Display(Name="Rol İsmi")]
		[Required(ErrorMessage ="Rol İsmi Boş Geçilemez")]
		public string? Name { get; set; }

	}
}

