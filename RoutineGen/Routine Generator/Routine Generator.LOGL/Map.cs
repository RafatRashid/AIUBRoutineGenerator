using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutineGenerator.ENTITY;
using RoutineGenerator.DAL;

namespace RoutineGenerator.LOGL
{
    public class Map
    {
        Room room;
        List<Course> crsList = new List<Course>();
        List<Room> labList = new List<Room>();
        Room lab;
        
        int labIndex = 0;
        private string[,] M;
        private string[,] L;
        private string[][,] AllLabs;
        private bool[] visited;
        private string[] labRoomNos;
        private int labRoomNosIndex = 0;
        private int visitingIndex = 0;
        private bool AllNull = true;

        public int TimeSlot;

        public Map(List<Course> list, Room room)
        {
            this.crsList = list;
            this.room = room;
            this.TimeSlot= new RoomAccess().GetTimeSlot(this.room);
            int labCount = new RoomAccess().GetAllLabCount();
            
            AllLabs = new string[labCount][,];
            visited = new bool[labCount];
            labRoomNos = new string[labCount];
            labRoomNos = new string[labCount];
        }
        
        public string MapTheoryAndLab()
        {
            this.BuildMapperTheoryAndLab(this.room);
            this.GetLabs();
            bool up = false, down = true;
            int secCount = 0;
            int i = 0, j = 0;
            string s = null;
            RoomAccess r = new RoomAccess();

            while (true)
            {
                
                if (this.room.SlotCount == this.TimeSlot*4)
                {
                    r.UpdateSlot(room);
                    s = "Room Full";
                    break;
                }
                else if (secCount == this.crsList.Count())
                {
                    r.UpdateSlot(room);
                    break;
                }

                if (i == 4)
                {
                    i = 2; up = true; down = false;
                }
                else if (i == -1)
                {
                    i = 1; down = true; up = false;
                }
                if (j == 5)
                    j = 0;

                if (down)
                {
                    if(j>=this.TimeSlot || M[i,j] != null)
                    {
                        i++; j++;
                    }
                    else if (M[i, j] == null)
                    {
                        if (!this.Find(string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section)))
                        {
                            this.AllNull = false;
                            secCount--;
                            M[i, j] = string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section);

                            i++; j++;
                            this.room.SlotCount++;
                        }
                    }
                }
                else if (up)
                {
                    if (j >= this.TimeSlot || M[i, j] != null)
                    {
                        i--; j++;
                    }
                    else if (M[i, j] == null)
                    {
                        if (!this.Find(string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section)))
                        {
                            this.AllNull = false;
                            secCount--;
                            M[i, j] = string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section);
                            
                            i--; j++;
                            this.room.SlotCount++;
                        }
                        
                    }
                }
            }
            if (AllNull)
                return "all mappable sections already mapped";
            else
            {
                if(s!=null)
                    return string.Format("inserted theory, lab:  " + this.InsertMapTheoryAndLab_Theory()+ " & " + s);

                return string.Format("inserted theory, lab:  " + this.InsertMapTheoryAndLab_Theory());
            }
            
        }

        public string InsertMapTheoryAndLab_Theory()
        {
            MapTheoryLabAccess ac = new MapTheoryLabAccess();
            RoomAccess r = new RoomAccess();
            List<MapTheoryLab> list = new List<MapTheoryLab>();
            int secCount = 0;
            
            for (int i = 0; i < 4; i++)
            {
                MapTheoryLab map = new MapTheoryLab();
                MapLab labMap = new MapLab();
                int k = 0;
                bool nullIdentifier = true;

                if (i == 0)
                {
                    map.DaySlot = "s";
                    labMap.DaySlot = "t";
                    k = 2;
                }

                else if (i == 1)
                {
                    map.DaySlot = "m";
                    labMap.DaySlot = "w";
                    k = 3;
                }

                else if (i == 2)
                {
                    map.DaySlot = "t";
                    labMap.DaySlot = "s";
                    k = 0;
                }

                else if (i == 3)
                {
                    map.DaySlot = "w";
                    labMap.DaySlot = "m";
                    k = 1;
                }
                map.RoomNo = this.room.RoomNo;

                if (M[i, 0] != null)
                {
                    map.s8_10 = M[i, 0];
                    nullIdentifier = false;
                    
                    bool found = this.Find(M[i, 0]);
                    while (true)
                    {
                        if (found)
                            break;
                        
                        if (AllLabs[labIndex][k,0] == null && this.lab.SlotCount < 16 && !found)
                        {
                            labMap.s8_11 = M[i, 0];
                            this.Inserted(M[i, 0]);
                            labMap.RoomNo = this.lab.RoomNo;
                            this.lab.SlotCount++;
                            r.UpdateSlot(this.lab);
                            AllLabs[labIndex][k, 0] = M[i, 0];
                            this.ResetLabs();
                            break;
                        }
                        else if ((AllLabs[labIndex][k, 0] != null || this.lab.SlotCount > 16) && !found)
                            this.ChangeLab();
                    }
                    
                }
                
                if (M[i, 1] != null)
                {
                    map.s10_12 = M[i, 1];
                    nullIdentifier = false;

                    bool found = this.Find(M[i, 1]);

                    while (true)
                    {
                        if (found)
                            break;
                        if (AllLabs[labIndex][k, 0] == null && this.lab.SlotCount < 16)
                        {
                            labMap.s8_11 = M[i, 1];
                            labMap.RoomNo = this.lab.RoomNo;
                            this.Inserted(M[i, 1]);
                            this.lab.SlotCount++;
                            r.UpdateSlot(this.lab);
                            AllLabs[labIndex][k, 0] = M[i, 1];
                            this.ResetLabs();
                            break;
                        }
                        else if (AllLabs[labIndex][k, 1] == null || this.lab.SlotCount > 16)
                        {
                            labMap.s11_2 = M[i, 1];
                            labMap.RoomNo = this.lab.RoomNo;
                            this.Inserted(M[i, 1]);
                            this.lab.SlotCount++;
                            r.UpdateSlot(this.lab);
                            AllLabs[labIndex][k, 1] = M[i, 1];
                            this.ResetLabs();
                            break;
                        }
                        else if (AllLabs[labIndex][k, 1] != null || this.lab.SlotCount > 16)
                            this.ChangeLab();
                    }
                    
                }
                
                if (M[i, 2] != null)
                {
                    map.s12_2 = M[i, 2];
                    nullIdentifier = false;

                    bool found = this.Find(M[i, 2]);
                    while(true)
                    {
                        if (found)
                            break;
                        if (AllLabs[labIndex][k, 1] == null && this.lab.SlotCount < 16)
                        {
                            labMap.s11_2 = M[i, 2];
                            labMap.RoomNo = this.lab.RoomNo;
                            this.Inserted(M[i, 2]);
                            this.lab.SlotCount++;
                            r.UpdateSlot(this.lab);
                            AllLabs[labIndex][k, 1] = M[i, 2];
                            this.ResetLabs();
                            break;
                        }
                        else if (AllLabs[labIndex][k, 1] != null || this.lab.SlotCount > 16)
                            this.ChangeLab();
                    }
                }  
                
                if (M[i, 3] != null)
                {
                    map.s2_4 = M[i, 3];
                    nullIdentifier = false;

                    bool found = this.Find(M[i, 3]);
                    while (true)
                    {
                        if (found)
                            break;
                        if (AllLabs[labIndex][k, 2] == null && this.lab.SlotCount < 16)
                        {
                            labMap.s2_5 = M[i, 3];
                            labMap.RoomNo = this.lab.RoomNo;
                            this.Inserted(M[i, 3]);
                            this.lab.SlotCount++;
                            r.UpdateSlot(this.lab);
                            AllLabs[labIndex][k, 2] = M[i, 3];
                            this.ResetLabs();
                            break;
                        }
                        else if (AllLabs[labIndex][k, 2] != null || this.lab.SlotCount > 16)
                            this.ChangeLab();
                    }
                }
                
                if (M[i, 4] != null)
                {
                    map.s4_6 = M[i, 4];
                    nullIdentifier = false;

                    bool found = this.Find(M[i, 4]);
                    while (true)
                    {
                        if (found)
                            break;
                        if (AllLabs[labIndex][k, 3] == null && this.lab.SlotCount < 16)
                        {
                            labMap.s5_8 = M[i, 4];
                            labMap.RoomNo = this.lab.RoomNo;
                            this.Inserted(M[i, 4]);
                            this.lab.SlotCount++;
                            r.UpdateSlot(this.lab);
                            AllLabs[labIndex][k, 3] = M[i, 4];
                            this.ResetLabs();
                            break;
                        }
                        else if (AllLabs[labIndex][k, 3] != null || this.lab.SlotCount > 16)
                            this.ChangeLab();
                    }
                }
                
                if(!nullIdentifier)
                    list.Add(map);
                
            }
            string th = ac.PushEntry(list).ToString();
            this.InsertMapTheorylab_Lab();

            return th;
        }

        private void InsertMapTheorylab_Lab()
        {
            
            for (int k = 0; k < AllLabs.Count(); k++ )
            {
                if (AllLabs[k] != null)
                {
                    MapLabAccess acc = new MapLabAccess();
                    List<MapLab> list = new List<MapLab>();
                    Room room = new Room();
                    
                    for (int i = 0; i < 4; i++)
                    {
                        bool nullIdentifier = true;
                        MapLab m = new MapLab();
                        m.RoomNo = this.labRoomNos[k];

                        if (AllLabs[k][i, 0] != null)
                        {
                            m.s8_11 = AllLabs[k][i, 0];
                            nullIdentifier = false;
                        }
                        else
                            m.s8_11 = null;

                        if (AllLabs[k][i, 1] != null)
                        {
                            m.s11_2 = AllLabs[k][i, 1];
                            nullIdentifier = false;
                        }
                        else
                            m.s11_2 = null;

                        if (AllLabs[k][i, 2] != null)
                        {
                            m.s2_5 = AllLabs[k][i, 2];
                            nullIdentifier = false;
                        }
                        else
                            m.s2_5 = null;

                        if (AllLabs[k][i, 3] != null)
                        {
                            m.s5_8 = AllLabs[k][i, 3];
                            nullIdentifier = false;
                        }
                        else
                            m.s5_8 = null;

                        if (i == 0)
                            m.DaySlot = "s";
                        else if (i == 1)
                            m.DaySlot = "m";
                        else if (i == 2)
                            m.DaySlot = "t";
                        else if (i == 3)
                            m.DaySlot = "w";

                        if(!nullIdentifier)
                            list.Add(m);

                    }
                    acc.PushEntry(list);
                }
            }
        }

        private void Inserted(string nullable)
        {
            CourseAccess crsAcc = new CourseAccess();

            string[] s = nullable.Split('-');
            string name = s[0], sec = s[1];
            foreach (Course c in this.crsList)
            {
                if (c.Name == name && c.Section == sec)
                {
                    crsAcc.UpdateMapStatus(c);
                    break;
                }
            }
        }

        private bool Find(string nullable)
        {
            bool temp = true;
            if (nullable == null)
                return true;

            string[] s = nullable.Split('-');
            string name = s[0], sec = s[1];
            foreach (Course c in this.crsList)
            {
                if (c.Name == name && c.Section == sec)
                {
                    temp = c.isMapped;
                    break;
                }
                
            }
            return temp;
        }

        private void ResetLabs()
        {
            this.labIndex=0;
            this.lab = this.labList[labIndex];
            visitingIndex = 0;
        }

        public void GetLabs()
        {
            RoomAccess r = new RoomAccess();
            this.labList = r.GetLab();
            this.lab = this.labList[labIndex];
            this.BuildMapperLab(this.lab, true);
            this.visited[visitingIndex] = true;
            this.labRoomNos[labRoomNosIndex] = this.lab.RoomNo;
        }

        public void ChangeLab()
        {
            this.lab = this.labList[++labIndex];
            if (!this.visited[++visitingIndex])
            {
                this.BuildMapperLab(this.lab, true);
            }
            this.visited[visitingIndex] = true;
            this.labRoomNos[++labRoomNosIndex] = this.lab.RoomNo;
        }

        public string MapTheory()
        {
            this.BuildMapperTheory(this.room);
            int secCount = 0;
            int i = 0, j = 0;
            string s = null;
            RoomAccess r = new RoomAccess();
            this.TimeSlot = new RoomAccess().GetTimeSlot(this.room);

            while (true)
            {
                if (this.room.SlotCount == this.TimeSlot * 2)
                {
                    r.UpdateSlot(room);
                    s = "Room Full";
                    break;
                }
                else if (secCount == this.crsList.Count())
                {
                    r.UpdateSlot(room);
                    break;
                }

                if (j == 7)
                    j = 0;

                if (i == 0)
                {
                    if(M[i,j]!=null || j>=this.TimeSlot)
                    {
                        i++; j++;
                    }
                    else if (M[i, j] == null)
                    {
                        AllNull = false;
                        if (!this.Find(string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section)))
                        {
                            secCount--;
                            M[i, j] = string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section);
                            
                            i++; j++;
                            this.room.SlotCount++;
                        }
                    }
                    
                }
                else if (i == 1)
                {
                    if(M[i,j] != null || j>=this.TimeSlot)
                    {
                        i--; j++;
                    }
                    else if (M[i, j] == null)
                    {
                        AllNull = false;
                        if (!this.Find(string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section)))
                        {
                            secCount--;
                            M[i, j] = string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section);
                            
                            i--; j++;
                            this.room.SlotCount++;
                        }
                    }
                    
                }
            }
            if (!AllNull)
            {
                if (s != null)
                    return string.Format("inserted theory:  " + this.InsertMapTheory() + " & " + s);
                else
                    return string.Format("inserted theory:  " + this.InsertMapTheory());
            }
            else
                return "All mappable sections are already mapped";
        }

        private string InsertMapTheory()
        {
            MapTheoryAccess ac = new MapTheoryAccess();
            List<MapTheory> list = new List<MapTheory>();

            for (int i = 0; i < 2; i++)
            {
                MapTheory map = new MapTheory();
                map.RoomNo = this.room.RoomNo;
                bool nullIdentifier = true;

                if (M[i, 0] != null)
                {
                    map.s8_930 = M[i, 0];
                    this.Inserted(M[i, 0]);
                    nullIdentifier = false;
                }
                else
                    map.s8_930 = null;

                if (M[i, 1] != null)
                {
                    map.s930_11 = M[i, 1];
                    this.Inserted(M[i, 1]);
                    nullIdentifier = false;
                }
                else
                    map.s930_11 = null;

                if (M[i, 2] != null)
                {
                    map.s11_1230 = M[i, 2];
                    this.Inserted(M[i, 2]);
                    nullIdentifier = false;
                }
                else
                    map.s11_1230 = null;

                if (M[i, 3] != null)
                {
                    map.s1230_2 = M[i, 3];
                    this.Inserted(M[i, 3]);
                    nullIdentifier = false;
                }
                else
                    map.s1230_2 = null;

                if (M[i, 4] != null)
                {
                    map.s2_330 = M[i, 4];
                    this.Inserted(M[i, 4]);
                    nullIdentifier = false;
                }
                else
                    map.s2_330 = null;

                if (M[i, 5] != null)
                {
                    map.s330_5 = M[i, 5];
                    this.Inserted(M[i, 5]);
                    nullIdentifier = false;
                }
                else
                    map.s330_5 = null;

                if (M[i, 6] != null)
                {
                    map.s5_630 = M[i, 6];
                    this.Inserted(M[i, 6]);
                    nullIdentifier = false;
                }
                else
                    map.s5_630 = null;


                if (i == 0)
                    map.DaySlot = "s-t";

                else if (i == 1)
                    map.DaySlot = "m-w";

                if(!nullIdentifier)
                    list.Add(map);
            }
            return ac.PushEntry(list).ToString();
        }
        
        public string MapLab()
        {
            this.BuildMapperLab(this.room, false);
            this.TimeSlot = new RoomAccess().GetTimeSlot(this.room);
            int flag = 1;
            int secCount = 0;
            int i = 0, j = 0;
            string s = null;
            bool up = false, down = true;
            RoomAccess r = new RoomAccess();

            while (true)
            {
                if (this.room.SlotCount == this.TimeSlot*4)
                {
                    r.UpdateSlot(room);
                    s = "Room Full";
                    break;
                }
                else if (secCount == this.crsList.Count())
                {
                    r.UpdateSlot(room);
                    break;
                }

                if (i == 4)
                {
                    i = 2; up = true; down = false;
                }
                else if (i == -1)
                {
                    i = 1; down = true; up = false;
                }
                if (j == 5)
                    j = 0;

                if (down)
                {
                    if (j >= this.TimeSlot || L[i, j] != null)
                    {
                        i++; j++;
                    }
                    else if (L[i, j] == null)
                    { 
                        if (!this.Find(string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section)))
                        {
                            this.AllNull = false;
                            secCount--;
                            L[i, j] = string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section);

                            i++; j++;
                            this.room.SlotCount++;
                        }
                    }
                    
                }
                else if (up)
                {
                    if(j>=this.TimeSlot || L[i, j] != null)
                    {
                        i--; j++;
                    }
                    else if (L[i, j] == null)
                    {
                        if (!this.Find(string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section)))
                        {
                            this.AllNull = false;
                            secCount--;
                            L[i, j] = string.Format(this.crsList[secCount].Name + "-" + this.crsList[secCount++].Section);

                            i--; j++;
                            this.room.SlotCount++;
                        }

                    }
                    
                }
            }
            if (!AllNull)
            {
                if (s != null)
                    return string.Format("inserted theory:  " + this.InsertLab(this.room) + " & " + s);
                else
                    return string.Format("inserted theory:  " + this.InsertLab(this.room));
            }
            else
                return "All mappable sections are already mapped";
            
        }

        private string InsertLab(Room room)
        {    
            MapLabAccess acc = new MapLabAccess();
            List<MapLab> list = new List<MapLab>();

            for (int i = 0; i < 4; i++)
            {
                MapLab m = new MapLab();
                m.RoomNo = room.RoomNo;
                bool nullIdentifier = true;

                if (L[i, 0] != null)
                {
                    m.s8_11 = L[i, 0];
                    this.Inserted(L[i, 0]);
                    nullIdentifier = false;
                }
                else
                    m.s8_11 = null;

                if (L[i, 1] != null)
                {
                    m.s11_2 = L[i, 1];
                    this.Inserted(L[i, 1]);
                    nullIdentifier = false;
                }
                else
                    m.s11_2 = null;

                if (L[i, 2] != null)
                {
                    m.s2_5 = L[i, 2];
                    this.Inserted(L[i, 2]);
                    nullIdentifier = false;
                }
                else
                    m.s2_5 = null;

                if (L[i, 3] != null)
                {
                    m.s5_8 = L[i, 3];
                    this.Inserted(L[i, 3]);
                    nullIdentifier = false;
                }
                else
                    m.s5_8 = null;

                if (i == 0)
                    m.DaySlot = "s";
                else if (i == 1)
                    m.DaySlot = "m";
                else if (i == 2)
                    m.DaySlot = "t";
                else if (i == 3)
                    m.DaySlot = "w";

                if(!nullIdentifier)
                    list.Add(m);

            }
            return acc.PushEntry(list).ToString();
        }

        private void BuildMapperLab(Room r, bool comboFlag)
        {
            if (comboFlag)
            {
                MapLabAccess acc = new MapLabAccess();
                this.AllLabs[this.labIndex] = acc.Check(r);

                if (this.AllLabs[this.labIndex] == null)
                    this.AllLabs[this.labIndex] = new string[4, 4];
                else 
                    return;
            }
            else 
            {
                MapLabAccess acc = new MapLabAccess();
                this.L = acc.Check(r);

                if (this.L == null)
                    this.L = new string[4, 5];
                else
                    return;
            }
        }

        private void BuildMapperTheory(Room r)
        {
            MapTheoryAccess acc = new MapTheoryAccess();
            this.M = acc.Check(r);

            if (this.M == null)
                this.M = new string[2, 7];
            else
                return;
        }

        private void BuildMapperTheoryAndLab(Room r)
        {
            MapTheoryLabAccess acc = new MapTheoryLabAccess();
            this.M = acc.Check(r);

            if (this.M == null)
                this.M = new string[4, 5];
            else
                return;
        }
    }
}
