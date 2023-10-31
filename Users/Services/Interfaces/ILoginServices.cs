using Users.Models.Dto;

namespace Users.Services.Interfaces;
public interface ILoginServices
{
    Task<Detail> Login(string userName, string password);
}
