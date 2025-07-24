namespace TriviaRoyaleGame.Client.Domain.Models
{
    public class UserViewModel : Entity
    {
        public string? Token { get; set; }
        public int MemberId { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }

        public bool IsOnLine { get; set; }
        public Role Role { get; set; }
        public MemberViewModel? MemberViewModel { get; set; }
        public ICollection<ScoreBoardViewModel>? ScoreBoardViewModels { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (UserViewModel)obj;
            return Id == other.Id && Email == other.Email;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ (Email?.GetHashCode() ?? 0);
        }
    }

    public enum Role
    {
        User = 1,
        Admin = 2,
        SuperAdmin = 3
    }
}
