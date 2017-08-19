using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutineGenerator.ENTITY;

namespace RoutineGenerator.DAL
{
    public class MapTheoryLabAccess
    {
        public int PushEntry(List<ENTITY.MapTheoryLab> list)
        {
            int x;
            using (RoutineGenContext context = new RoutineGenContext())
            {
                this.DeletePrevious(list[0].RoomNo);

                context.MapTheoryLabs.AddRange(list);
                x = context.SaveChanges(); 
                
            }
            return x;
        }

        public string[,] Check(Room room)
        {
            RoutineGenContext context = new RoutineGenContext();

            var map = from m in context.MapTheoryLabs
                      where m.RoomNo == room.RoomNo
                      select m;

            if (map.Count<MapTheoryLab>() == 0)
                return null;

            string[,] arr = new string[4, 5];
            int i = 0;
            foreach (MapTheoryLab m in map)
            {
                if (m.DaySlot == "s")
                    i = 0;
                else if (m.DaySlot == "m")
                    i = 1;
                else if (m.DaySlot == "t")
                    i = 2;
                else if (m.DaySlot == "w")
                    i = 3;

                arr[i, 0] = m.s8_10;
                arr[i, 1] = m.s10_12;
                arr[i, 2] = m.s12_2;
                arr[i, 3] = m.s2_4;
                arr[i, 4] = m.s4_6;
            }
            return arr;
        }
        
        private void DeletePrevious(string roomNo)
        {
            RoutineGenContext context = new RoutineGenContext();

            var query = from r in context.MapTheoryLabs
                        where r.RoomNo == roomNo
                        select r;

            context.MapTheoryLabs.RemoveRange(query);
            context.SaveChanges();
        }

        public List<MapTheoryLab> GetAllDatas()
        {
            RoutineGenContext ctxt = new RoutineGenContext();

            var q = from c in ctxt.MapTheoryLabs
                    select c;
            return q.ToList();
        }
    }
}
