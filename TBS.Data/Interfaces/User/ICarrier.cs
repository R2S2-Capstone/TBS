using System.Collections.Generic;
using TBS.Data.Models.User;

namespace TBS.Data.Interfaces.User
{
    public interface ICarrierService
    {
        Carrier Get(int id);

        IEnumerable<Carrier> GetAll();
    }
}
