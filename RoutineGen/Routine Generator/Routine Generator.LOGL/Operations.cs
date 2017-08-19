using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutineGenerator.ENTITY;
using RoutineGenerator.DAL;

namespace RoutineGenerator.LOGL
{
    public class Operations
    {
        Room selectedRoom;
        List<Course> selectedCourses;

        public int AddCourse(List<Course> x)
        {
            CourseAccess c = new CourseAccess();
            return c.InsertCourses(x);
        }

        public int AddRoom(List<Room> y) // new room add hoitese
        {
            RoomAccess r = new RoomAccess();
            return r.InsertRooms(y); // list pathaitase dal e
        }

        public Room GetRoom(string num)
        {
            RoomAccess r = new RoomAccess();
            return r.GetRooms(num);
        }
        
        public List<Course> GetCourse(string s)
        {
            CourseAccess c = new CourseAccess();
            return c.GetCourses(s);

        }
        
        public int Decide(List<Course> crs, Room rm)
        {
            this.selectedCourses = crs;
            this.selectedRoom = rm;
           
            if (this.selectedCourses[0].Duration == this.selectedRoom.Duration && !this.selectedCourses[0].isLab && !this.selectedRoom.Lab)
            {
                Map m = new Map(this.selectedCourses, this.selectedRoom);
                string s = m.MapTheory();
                return 0;
             }
            if (this.selectedCourses[0].Duration == this.selectedRoom.Duration && this.selectedCourses[0].isLab != this.selectedRoom.Lab)
            {
                Map m = new Map(this.selectedCourses, this.selectedRoom);
                string s = m.MapTheoryAndLab();
                return 0;
            }
            else if (this.selectedCourses[0].Duration == this.selectedRoom.Duration && this.selectedCourses[0].isLab && this.selectedRoom.Lab)
            {
                Map m = new Map(this.selectedCourses, this.selectedRoom);
                string s = m.MapLab();
                return 0;
            }
            else
                return 1;
        }

        public bool isRoomFull(Room room)
        {
            return new RoomAccess().isRoomFull(room);
        }

        public List<Room> ShowBBARooms()
        {
            return new RoomAccess().ShowBBARooms();
        }

        public List<Room> ShowCSERooms()
        {
            return new RoomAccess().ShowCSERooms();

        }

        public List<Course> ShowBBACourses()
        {
            return new CourseAccess().ShowBBACourses();
        }

        public List<Course> ShowCSECourses()
        {
            return new CourseAccess().ShowCSECourses();
        }

        public List<Room> ShowEEERooms()
        {
            return new RoomAccess().ShowEEERooms();
        }

        public bool isAllMapped(string v)
        {
            return new CourseAccess().isAllMapped(v);
        }

        public List<Course> ShowAllCourses()
        {
            return new CourseAccess().ShowAllCourses();
        }

        public List<Course> ShowEEECourses()
        {
            CourseAccess c = new CourseAccess();
            return c.GetEEECourses();
        }

        public List<Room> ShowARTRooms()
        {
            return new RoomAccess().ShowARTRooms();
        }

        public List<Course> ShowARTCourses()
        {
            CourseAccess c = new CourseAccess();
            return c.GetARTCourses();
        }

        public List<Room> ShowAllRooms()
        {
            RoomAccess r = new RoomAccess();
            return r.GetAllRooms();
        }

        public void DeleteSections(string name, string section)
        {
            CourseAccess c = new CourseAccess();
            c.DeleteSections(name, section);
        }

        public void ChangeRoomTimeSlot(string id, int timeSlots)
        {
            new RoomAccess().ChangeTimeSlot(id, timeSlots);
        }

        public List<MapLab> GetMapLabs()
        {
            MapLabAccess acc = new MapLabAccess();
            return acc.GetAllDatas();
        }

        public List<MapTheoryLab> GetMapTheoryLabs()
        {
            MapTheoryLabAccess acc = new MapTheoryLabAccess();
            return acc.GetAllDatas();
        }

        public List<MapTheory> GetTheories()
        {
            MapTheoryAccess acc = new MapTheoryAccess();
            return acc.GetAllDatas();
        }


        public void DeleteRoom(string p)
        {
            RoomAccess rm = new RoomAccess();
            rm.DeleteRoom(p);
        }

       
    }
}
