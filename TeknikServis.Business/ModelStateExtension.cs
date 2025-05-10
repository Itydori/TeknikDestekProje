using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Business
{
	public static class ModelStateExtension

	{

		public static  List<string> AddModelStateExtension(this ModelStateDictionary keyValues)
		{
			var errors=keyValues.Values.SelectMany(v=>v.Errors).Select(e=>e.ErrorMessage).ToList();


			errors.ForEach(e => System.Console.WriteLine("❌ Hata: " + e));

			return errors;
		}
	}
}
