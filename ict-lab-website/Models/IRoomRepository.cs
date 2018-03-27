using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models
{
    public interface IRoomRepository
    {
        IQueryable<Room> Rooms { get; }
        Room GetById(int id);
        void Add(Room room);
        void Delete(Room room);
    }
}
