using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityNew.Extensions
{
	public static class ModelSatateExtensions
	{
		public static void AddModelErrorList(this ModelStateDictionary modelState,List<string> errors)
		{
			errors.ForEach(x =>
			{
				modelState.AddModelError(string.Empty,x) ;
			});
			
		}
	}
}

