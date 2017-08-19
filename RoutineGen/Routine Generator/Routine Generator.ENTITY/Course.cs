using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineGenerator.ENTITY
{
    public class Course
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        [Key]
        [Column(Order = 1)]
        public string Name { get; set; }
        public string Department { get; set;}
        [Key]
        [Column(Order = 2)]
        public string Section { get; set; }
        public double Duration { get; set; }
        public bool isLab { get; set; }
        public bool isMapped { get; set; }
    }
}
