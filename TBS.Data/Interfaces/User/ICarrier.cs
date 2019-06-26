using System.Collections.Generic;
using TBS.Data.Models.User;

namespace TBS.Data.Interfaces.User
{
    public interface ICarrier
    {
        Carrier Get(int id);

        IEnumerable<Carrier> GetAll();

        //Get all posts

        //Get all bids


    }
}
