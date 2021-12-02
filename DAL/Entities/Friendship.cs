namespace DAL.Entities
{
    public class Friendship : BaseEntity
    {
        public int UserProfileId { get; set; }
        public int FriendProfileId { get; set; }
    }
}
