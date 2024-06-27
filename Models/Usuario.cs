namespace JwtAspNet.Models
{
    public record Usuario(int Id, string Email, string Nome, string Password, string[] Roles);
}
