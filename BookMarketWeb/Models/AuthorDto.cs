namespace BookMarketWeb.Models;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class CreateAuthorDto
{
    public string Name { get; set; }
}

public class UpdateAuthorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}