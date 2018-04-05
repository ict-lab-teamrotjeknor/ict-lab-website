using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models
{
    public interface IRoomRepository
    {
        IQueryable<Room> Rooms { get; }
        Room GetById(string id);
        Room GetByName(string name);
        void Add(Room room);
        void Delete(Room room);

    }
}
