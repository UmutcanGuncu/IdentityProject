using System;
using System.ComponentModel.DataAnnotations;
using IdentityNew.Models;

namespace IdentityNew.ViewModel
{
	public class UserEditViewModel
	{
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Lütfen E Posta Adresinizi Giriniz")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Lütfen Telefon Numaranızı Giriniz")]
        public string? Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }
        public string? City { get; set; }
        public IFormFile? Picture { get; set; }
        public Gender? Gender { get; set; }
        
    }
}

