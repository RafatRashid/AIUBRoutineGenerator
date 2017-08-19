using RoutineGenerator.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoutineGenerator.DAL
{
    public class MapLabAccess
    {
        public int PushEntry(List<ENTITY.MapLab> listL)
        {
            RoutineGenContext context = new RoutineGenContext();

            this.DeletePrevious(listL[0].RoomNo);

            context.MapLabs.AddRange(listL);
            int x = context.SaveChanges();

            return x;
        }

        public string[,] Check(Room room)
        {
            RoutineGenContext context = new RoutineGenContext();

            var map = from m in context.MapLabs
                      where m.RoomNo == room.RoomNo
                      select m;

            if (map.Count<MapLab>() == 0)
                return null;

            string[,] arr = new string[4, 5];
            int i=0;
            foreach (MapLab m in map)
            {
                if (m.DaySlot == "s")
                    i = 0;
                else if (m.DaySlot == "m")
                    i = 1;
                else if (m.DaySlot == "t")
                    i = 2;
                else if(m.DaySlot == "w")
                    i = 3;

                arr[i, 0] = m.s8_11;
                arr[i, 1] = m.s11_2;
                arr[i, 2] = m.s2_5;
                arr[i, 3] = m.s5_8;
                arr[i, 4] = null;
            }

            return arr;
        }

        private void DeletePrevious(string roomNo)
        {
            RoutineGenContext context = new RoutineGenContext();

            var query = from r in context.MapLabs
                        where r.RoomNo == roomNo
                        select r;

            context.MapLabs.RemoveRange(query);
            context.SaveChanges();
        }


        public List<MapLab> GetAllDatas()
        {
            RoutineGenContext ctxt = new RoutineGenContext();

            var q = from c in ctxt.MapLabs
                    select c;
            return q.ToList();
        }
    }
}
