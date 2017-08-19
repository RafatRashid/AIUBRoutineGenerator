using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutineGenerator.ENTITY;

namespace RoutineGenerator.DAL
{
    public class MapTheoryAccess
    {
        public int PushEntry(List<MapTheory> list)
        {
            RoutineGenContext context = new RoutineGenContext();

            this.DeletePrevious(list[0].RoomNo);

            context.MapTheorys.AddRange(list);
            int x = context.SaveChanges();
            return x;
        }

        public string[,] Check(Room room)
        {
            RoutineGenContext context = new RoutineGenContext();

            var map = from m in context.MapTheorys
                      where m.RoomNo == room.RoomNo
                      select m;

            if (map.Count<MapTheory>() == 0)
                return null;

            string[,] arr = new string[2, 7];
            int i = 0;
            foreach (MapTheory m in map)
            {
                if (m.DaySlot == "s-t")
                    i = 0;
                else if (m.DaySlot == "m-w")
                    i = 1;
                
                arr[i, 0] = m.s8_930;
                arr[i, 1] = m.s930_11;
                arr[i, 2] = m.s11_1230;
                arr[i, 3] = m.s1230_2;
                arr[i, 4] = m.s2_330;
                arr[i, 5] = m.s330_5;
                arr[i, 6] = m.s5_630;
            }

            return arr;
        }

        private void DeletePrevious(string roomNo)
        {
            RoutineGenContext context = new RoutineGenContext();

            var query = from r in context.MapTheorys
                        where r.RoomNo == roomNo
                        select r;

            context.MapTheorys.RemoveRange(query);
            context.SaveChanges();
        }

        public List<MapTheory> GetAllDatas()
        {
            RoutineGenContext ctxt = new RoutineGenContext();

            var q = from c in ctxt.MapTheorys
                    select c;
            return q.ToList();
        }
    }
}
