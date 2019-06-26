using TBS.Data.Models.User;

namespace TBS.Data.Interfaces.User
{
    public interface ICarrier
    {
        Carrier Get(int id);
    }
}
