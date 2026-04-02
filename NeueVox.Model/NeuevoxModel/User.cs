using NeueVox.Model.NeuevoxModel.enums;

namespace NeueVox.Model.NeuevoxModel;

public class User
{

    public Guid UserId {get; set;}
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string SchoolEmail {get; set;}
    public required string PasswordHash {get; set;}
    public UserRole Role {get; set;}

    public string? ProfilePictureUrl { get; set; }

    public string? PhoneNumber {get; set;}
    public string? Coordination {get; set;}

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public DateTime CreatedAt { get; set; }

}
