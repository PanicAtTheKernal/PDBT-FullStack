namespace PDBT_CompleteStack.Models;

public class RefreshTokenDTO
{
    public string Token { get; set; } = string.Empty;
    public string JWT { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime Expires { get; set; }
}