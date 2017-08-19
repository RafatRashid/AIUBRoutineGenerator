using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutineGenerator.ENTITY;


namespace RoutineGenerator.DAL
{
    public class RoomAccess
    {
        public int InsertRooms(List<Room> list)
        {
            using (RoutineGenContext context = new RoutineGenContext())
            {
                context.Rooms.AddRange(list);
                int c = context.SaveChanges();
                return c;
            }
        }

        public Room GetRooms(string num)
        {
            RoutineGenContext context = new RoutineGenContext();
            var room = from r in context.Rooms
                       where r.RoomNo == num
                       select r;

            return room.FirstOrDefault();
        }
        public List<Room> GetAllRooms()
        {
            RoutineGenContext context = new RoutineGenContext();
            var room = from r in context.Rooms
                       select r;

            return room.ToList<Room>();
        }

        public int GetAllLabCount()
        {
            RoutineGenContext context = new RoutineGenContext();
            var c = (from n in context.Rooms
                     where n.Lab == true
                     select n).Count<Room>();
            return c;
        }

        public List<Room> GetLab()
        {
            RoutineGenContext context = new RoutineGenContext();
            var room = from r in context.Rooms
                       where r.Lab == true
                       select r;

            return room.ToList<Room>();
        }

        public int GetTimeSlot(Room room)
        {
            RoutineGenContext con = new RoutineGenContext();
            var query = (from c in con.Rooms
                         where c.RoomNo == room.RoomNo
                         select c.TimeSlots).First<int>();
            return query;
        }

        public void UpdateSlot(Room room)
        {
            using (RoutineGenContext con = new RoutineGenContext())
            {
                var temp = from l in con.Rooms
                           where l.RoomNo == room.RoomNo
                           select l;

                temp.First<Room>().SlotCount = room.SlotCount;
                con.SaveChanges();
            }
        }

        public void DeleteRoom(string p)
        {
            RoutineGenContext context = new RoutineGenContext();

            var q = (from c in context.Rooms
                     where c.RoomNo == p
                     select c).First();
            
            context.Rooms.Remove(q);
            context.SaveChanges();
        }

        public void ChangeTimeSlot(string id, int timeSlots)
        {
            RoutineGenContext context = new RoutineGenContext();

            var q = (from r in context.Rooms
                     where r.RoomNo == id
                     select r).First();

            q.TimeSlots = timeSlots;
            context.SaveChanges();
        }

        public bool isRoomFull(Room room)
        {
            RoutineGenContext con = new RoutineGenContext();
            var query = (from r in con.Rooms
                        where r.RoomNo == room.RoomNo
                        select r).First<Room>();
            if ((query.Duration == 1.5 && query.SlotCount == this.GetTimeSlot(room)*2) || (query.Duration == 2 && query.SlotCount == this.GetTimeSlot(room)*4) || (query.Duration == 3 && query.SlotCount == this.GetTimeSlot(room)*4))
                return true;
            else
                return false;
                
        }

        public List<Room> ShowARTRooms()
        {
            RoutineGenContext context = new RoutineGenContext();
            var room = from r in context.Rooms
                       where r.Department == "ARTS"
                       select r;

            return room.ToList<Room>();
        }

        public List<Room> ShowCSERooms()
        {
            RoutineGenContext context = new RoutineGenContext();
            var room = from r in context.Rooms
                       where r.Department == "CSE"
                       select r;

            return room.ToList<Room>();
        }

        public List<Room> ShowBBARooms()
        {
            RoutineGenContext context = new RoutineGenContext();
            var room = from r in context.Rooms
                       where r.Department == "BBA"
                       select r;

            return room.ToList<Room>();
        }

        public List<Room> ShowEEERooms()
        {
            RoutineGenContext context = new RoutineGenContext();
            var room = from r in context.Rooms
                       where r.Department == "EEE"
                       select r;

            return room.ToList<Room>();
        }
    }
}
