using System;
namespace IdentityNew.Services
{
	public interface IEmailService
	{
		public Task SendResetPasswordEmail(string resetEmailLink, string To);
	}
}

