namespace BLL.DTOs
{
    public class UserSearchFilterDTO : BaseDTO 
    {
        public string City { get; set; }
        public string School { get; set; }
        public string University { get; set; }
        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
    }
}

