using Ads.Domain.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Domain.Entities.Concrete
{
    public class AppRole : IdentityRole<int>,IAuiditEntity
    {
		public virtual ICollection<AppUser> Users { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
