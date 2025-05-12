using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Business.Response
{
	public  class BaseResponse<T> where T:class
	
	{
        public T Data { get; set; }
        public List<string> Errors { get; init; }
        public bool Success => Errors.Count == 0 || Errors == null;
        public bool Error => !Success;
        public static BaseResponse<T> Successfully(T data)
		{
			return new BaseResponse<T>
			{
				Data = data,
				Errors = default
			};
		}
		public static BaseResponse<T> Failed(string error)
		{
			return new BaseResponse<T>
			{
				Data = default,
				Errors = new List<string> { error }
			};
		}
		public static BaseResponse<T> Failed(List<string> error)
		{
			return new BaseResponse<T>
			{
				Data = default,
				Errors= error
			};
		}
	}
}
