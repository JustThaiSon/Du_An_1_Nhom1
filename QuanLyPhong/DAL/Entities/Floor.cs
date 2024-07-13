using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Floor
    {
        public Guid Id { get; set; }
        public string? FloorName { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
