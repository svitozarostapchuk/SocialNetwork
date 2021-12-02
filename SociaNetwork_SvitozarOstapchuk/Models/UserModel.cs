namespace WebAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }    
        public string Password { get; set; } 
        public string Role { get; set; }     
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string School { get; set; }
        public string University { get; set; }
        public string AboutUser { get; set; }
        public int Age { get; set; }
    }
}
