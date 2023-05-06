using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityNew.Localizations
{
	public class LocalizationIdentityErrorDescriber:IdentityErrorDescriber
	{
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() {Code= "DuplicateUserName",Description=$" '{userName}' Kullanılmaktadır. Lütfen Başka Kullanıcı Adı Seçiniz" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description=$"'{email}' Kullanılmaktadır. Lütfen Başka Bir E Posta Adresi Seçiniz" };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() { Code= "PasswordRequiresNonAlphanumeric", Description="!'^+%&/()=., vb Karakterlerden En Az Biri Bulunmalıdır"};
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code= "PasswordTooShort",Description="Şifreniz En Az 6 Karakterden Oluşmalıdır" };
        }
    }
}

