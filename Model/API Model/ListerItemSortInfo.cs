using System.ComponentModel.DataAnnotations;

namespace EazyOnRent.Model.API_Model
{
    public class ListerItemSortInfo
    {
        public ListerItemSortInfo()
        {
            ItemImageList = new List<ItemImageResult>();
           
        }
        public int ListerItemId { get; set; }
        public int ListerId { get; set; }
        public string? ItemName { get; set; }
        public string? CompanyName { get; set; }
        
        public int? CategoryId { get; set; }
        public string? Location { get; set; }
        public List<ItemImageResult> ItemImageList { get; set; }
       
        
    }
}
