using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Entity;
using RoutineGenerator.ENTITY;

namespace RoutineGenerator.DAL
{
    public class RoutineGenContext: DbContext
    {
        private const string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\RFTx003\OneDrive\7th sem\C#\Codes\PROJECT final updated\RoutinGenDb\CourseRoomMapperDB.mdf;Integrated Security=True;Connect Timeout=30";
        private static System.Data.Entity.SqlServer.SqlProviderServices _instance =
               System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        public RoutineGenContext() : base(conn) {}
        
        public DbSet<Course> Courses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<MapTheoryLab> MapTheoryLabs { get; set; }
        public DbSet<MapLab> MapLabs { get; set; }
        public DbSet<MapTheory> MapTheorys { get; set; }
    }
}
