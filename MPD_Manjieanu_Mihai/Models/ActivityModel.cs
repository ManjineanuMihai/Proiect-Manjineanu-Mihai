using System.ComponentModel.DataAnnotations;

namespace MPD_Manjineanu_Mihai.Models
{
    public class ActivityModel
    {
        [Key]
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? UserID { get; set; }
        public  UserModel? User { get; set; }
        public int? CategoryID { get; set; }
        public  CategoryModel? Category { get; set; }
    }
}
