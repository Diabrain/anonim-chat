namespace anonim_chat.API.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Chat? Chat { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public required DateTime CreatedAt { get; set; }  

    }
}
