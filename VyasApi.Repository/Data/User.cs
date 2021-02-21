using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VyasApi.Repository.Data
{
	[Table("users", Schema = "dbo")]
	public class User
	{
		[Column("id")] public long Id {get;set;}
		[Required] [Column("email")] public string Email {get;set;}
		[Required] [Column("fullname")] public string FullName {get;set;}
		[Column("token")] public string Token {get;set;}
		[Column("is_active")] public bool IsActive {get;set;}
		[Column("activation_id")] public string ActivationId {get;set;}
		[Column("last_login")] public DateTime LastLogin {get;set;}
		[Column("create_date")] public DateTime CreatedDate {get;set;}
	}
}