using System;
namespace VyasApi.Data.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public string ActivationId { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
