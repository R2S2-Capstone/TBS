using System.Collections.Generic;
using TBS.Data.Models.User;

namespace TBS.Data.Interfaces.User
{
    public interface IShipper
    {
        Shipper Get(int id);

        IEnumerable<Shipper> GetAll();

        //Get all posts

        //Get all bids


    }
}
