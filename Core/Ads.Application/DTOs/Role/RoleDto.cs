using Ads.Application.DTOs.User;

namespace Ads.Application.DTOs.Role
{
    public class RoleDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ICollection<UserDto> Users { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
