namespace Ads.Application.DTOs.User
{
    public class ChangePasswordDto
	{
		public int UserId { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}