using System.Collections.Generic;
using TBS.Data.Models.User;

namespace TBS.Data.Interfaces.User
{
    public interface IShipperService
    {
        Shipper Get(int id);

        IEnumerable<Shipper> GetAll();
    }
}
