using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineGenerator.ENTITY
{
    public class MapTheoryLab
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SlNo { get; set; }
        public string RoomNo { get; set; }
        public string DaySlot { get; set; }

        public string s8_10 { get; set; }
        public string s10_12 { get; set; }
        public string s12_2 { get; set; }
        public string s2_4 { get; set; }
        public string s4_6 { get; set; }
    }
}
