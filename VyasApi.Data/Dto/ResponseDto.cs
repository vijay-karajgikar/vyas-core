using System;
using System.Collections.Generic;

namespace VyasApi.Data.Dto
{
	public class ResponseDto<T>
	{
		public T Results {get;set;}
		public int ResponseCode {get;set;}
		public IEnumerable<ErrorDto> Errors {get;set;}
	}
}