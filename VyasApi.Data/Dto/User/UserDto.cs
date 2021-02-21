using System;
namespace VyasApi.Data.Dto.User
{
    public class UserDto
    {
        public long? Id {get;set;}
		public string Email {get;set;}
		public string FullName {get;set;}
		public bool IsActive {get;set;}
		public string ActivationId {get;set;}
		public DateTime? LastLogin {get;set;}
		public DateTime? CreatedDate {get;set;}
		public string Password {get;set;}
    }
}
