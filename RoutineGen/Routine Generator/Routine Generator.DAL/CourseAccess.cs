using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutineGenerator.ENTITY;

namespace RoutineGenerator.DAL
{
    public class CourseAccess
    {
        public int InsertCourses(List<Course> list)
        {
            using (RoutineGenContext context = new RoutineGenContext())
            {
                context.Courses.AddRange(list);
                try
                {
                    int c = context.SaveChanges();
                    return c;
                }
                catch(Exception e)
                {
                    return -999;
                }
                
            }
        }

        public List<Course> GetCourses(string s)
        {
            RoutineGenContext context = new RoutineGenContext();

            var crs = from c in context.Courses
                      where c.Name == s
                      select c;
            return crs.ToList<Course>();
        }

        public List<Course> GetEEECourses()
        {
            RoutineGenContext context = new RoutineGenContext();

            var crs = from c in context.Courses
                      where c.Department == "EEE"
                      select c;

            return crs.ToList<Course>();
        }

        public void UpdateMapStatus(Course c)
        {
            RoutineGenContext con = new RoutineGenContext();

            var temp = from x in con.Courses
                       where x.Id == c.Id
                       select x;
            temp.First<Course>().isMapped = true;
            con.SaveChanges();
        }

        public void DeleteSections(string name, string section)
        {
            RoutineGenContext context = new RoutineGenContext();

            
            var query = (from c in context.Courses
                        where c.Name == name && c.Section == section
                        select c).FirstOrDefault();
            context.Courses.Remove(query);
            
            context.SaveChanges();
        }

        public List<Course> ShowAllCourses()
        {
            RoutineGenContext context = new RoutineGenContext();

            var crs = from c in context.Courses
                      select c;

            return crs.ToList<Course>();
        }

        public List<Course> ShowBBACourses()
        {
            RoutineGenContext context = new RoutineGenContext();

            var crs = from c in context.Courses
                      where c.Department == "BBA"
                      select c;

            return crs.ToList<Course>();
        }

        public List<Course> ShowCSECourses()
        {
            RoutineGenContext context = new RoutineGenContext();

            var crs = from c in context.Courses
                      where c.Department == "CSE"
                      select c;

            return crs.ToList<Course>();
        }

        public bool isAllMapped(string v)
        {
            RoutineGenContext con = new RoutineGenContext();

            var g = from c in con.Courses
                    where c.Name == v
                    select c;

            foreach(Course c in g)
            {
                if (!c.isMapped)
                    return false;
            }
            return true;
        }

        public List<Course> GetARTCourses()
        {
            RoutineGenContext context = new RoutineGenContext();

            var crs = from c in context.Courses
                      where c.Department == "ARTS"
                      select c;

            return crs.ToList<Course>();
        }
    }
}
