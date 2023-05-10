using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNew.ViewModel
{
	public class PasswordChangeViewModel
	{
		[Required(ErrorMessage ="Lütfen Eski Şifrenizi Giriniz")]
		[MinLength(6,ErrorMessage ="Lütfen En Az 6 Karakter Giriniz")]
		public string? OldPassword { get; set; }
        [Required(ErrorMessage = "Lütfen Yeni Şifrenizi Giriniz")]
        [MinLength(6, ErrorMessage = "Lütfen En Az 6 Karakter Giriniz")]
        public string? NewPassword { get; set; }
		[Compare("NewPassword",ErrorMessage ="Şifreler Birbiri İle Uyuşmuyor")]
        [MinLength(6, ErrorMessage = "Lütfen En Az 6 Karakter Giriniz")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Giriniz")]
        public string? ConfirmPassword { get; set; }
	}
}

