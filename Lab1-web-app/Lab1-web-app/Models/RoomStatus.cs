using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class RoomStatus
    {
        public RoomStatus()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
