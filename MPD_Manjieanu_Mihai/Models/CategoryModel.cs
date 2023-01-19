using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPD_Manjineanu_Mihai.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public  List<ActivityModel> Activities { get; set; }
    }
}
