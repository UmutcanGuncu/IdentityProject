using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNew.ViewModel
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="Lütfen Kullanıcı Adınızı Giriniz")]
		public string? UserName { get; set; }
        [Required(ErrorMessage = "Lütfen E Posta Adresinizi Giriniz")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Lütfen Telefon Numaranızı Giriniz")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        [MinLength(6, ErrorMessage = "Şifreniz Minimum 6 Karakter Olmalıdır")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Giriniz")]
        [MinLength(6, ErrorMessage = "Şifreniz Minimum 6 Karakter Olmalıdır")]
        [Compare("Password",ErrorMessage ="Şifreler Birbiri İle Uyuşmuyor")]
        public string? ConfirmPassword { get; set; }
	}
}

