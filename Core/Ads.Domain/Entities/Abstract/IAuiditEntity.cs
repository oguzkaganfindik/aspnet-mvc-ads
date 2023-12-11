namespace Ads.Domain.Entities.Abstract
{
    public interface IAuiditEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}