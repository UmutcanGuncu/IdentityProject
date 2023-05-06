using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNew.ViewModel
{
	public class SignInViewModel
	{
		[Required(ErrorMessage ="E Posta Adresinizi Giriniz")]
		
		public string? Email { get; set; }
		[Required(ErrorMessage ="Şifrenizi Giriniz")]
        [MinLength(6,ErrorMessage ="Şifreniz Minimum 6 Karakter Olmalıdır")]
        public string? Password { get; set; }
		public bool RememberMe { get; set; }
	}
}

