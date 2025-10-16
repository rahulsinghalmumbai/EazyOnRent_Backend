using System.ComponentModel.DataAnnotations;

namespace EazyOnRent.Model
{
    public class dbViewed
    {
        [Key]
        public int viewedID { get; set; }

        public int? viewerID { get; set; }

        public int? ViewerCategory { get; set; }

        public DateTime? ViewDate { get; set; }= DateTime.Now;

        public int? ListerItemID { get; set; }
    }
}
