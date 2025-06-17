using booklend.Models;

namespace booklend.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string Generate(User user);
    }
}