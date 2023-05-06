using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNew.ViewModel
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage ="Lütfen E Posta Adresinizi Giriniz")]
		[EmailAddress(ErrorMessage ="Lütfen E Posta Adresinizi Formata Uygun Girinz")]
		public string? Email { get; set; }
	}
}

