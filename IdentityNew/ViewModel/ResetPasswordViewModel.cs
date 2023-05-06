using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNew.ViewModel
{
	public class ResetPasswordViewModel
	{
		
		[Required(ErrorMessage ="Şifre Alanı Boş Geçilemez")]
		[MinLength(6,ErrorMessage ="Şifreniz En Az 6 Karakter Uzunluğunda Olmalıdır")]
		public string? Password { get; set; }
        [Required(ErrorMessage = "Şifre Tekrar Alanı Boş Geçilemez")]
        [MinLength(6, ErrorMessage = "Şifreniz En Az 6 Karakter Uzunluğunda Olmalıdır")]
		[Compare("Password",ErrorMessage ="Şifreler Birbiri İle Uyuşmuyor")]
        public string? ConfirmPassword { get; set; }
		public string? UserId { get; set; }
		public string? Token { get; set; }
	}
}

