using DAL.Entities;

namespace DAL
{
    public class UserProfile : BaseEntity
    {
        public string City { get; set; }
        public string School { get; set; }
        public string University { get; set; }
        public string AboutUser { get; set; }
        public int Age { get; set; }
    }
}
