namespace NLayer.Core.Models
{
    public abstract class BaseEntity //baseentity den yeni bir örnek alınmasın diye (new) abstract
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
