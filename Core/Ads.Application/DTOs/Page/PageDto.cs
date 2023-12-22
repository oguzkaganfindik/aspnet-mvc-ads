using Ads.Application.DTOs.Setting;

namespace Ads.Application.DTOs.Page
{
    public class PageDto
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string Title1 { get; set; }
        public string? Title2 { get; set; }
        public string Content1 { get; set; }
        public string? Content2 { get; set; }
        public string? PageImagePath { get; set; }
		public bool IsActive { get; set; }
		public string? Link { get; set; }
		public int SettingId { get; set; }
		public ICollection<SettingDto>? Settings { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		
		// public string IsActiveString => IsActive ? "Active" : "Passive";
	}

}
