using Explorer.BuildingBlocks.Core.Domain;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.Domain;

public class Person : Entity
{
    public long UserId { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string ProfilePictureUrl { get; init; }
    public string Biography { get; init; }
    public string Motto { get; init; }
    public string Email { get; init; }
    public int TouristLevel { get; init; }
    public int TouristXp { get; init; }
    public List<int> Followers { get; init; } = new List<int>();
    public List<int> Following { get; init; } = new List<int>();

    public Person(long userId, string name, string surname, string email, string profilePictureUrl, string biography, string motto,int touristLevel, int touristXp)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        ProfilePictureUrl = profilePictureUrl;
        Biography = biography;
        Motto = motto;
        TouristLevel = touristLevel;
        TouristXp = touristXp;
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
        if (!MailAddress.TryCreate(Email, out _)) throw new ArgumentException("Invalid Email");
    }

}