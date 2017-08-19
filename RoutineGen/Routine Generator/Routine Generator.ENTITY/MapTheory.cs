using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineGenerator.ENTITY
{
    public class MapTheory
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SlNo { get; set; }
        public string RoomNo { get; set; }
        public string DaySlot { get; set; }

        public string s8_930 { get; set; }
        public string s930_11 { get; set; }
        public string s11_1230 { get; set; }
        public string s1230_2 { get; set; }
        public string s2_330 { get; set; }
        public string s330_5 { get; set; }
        public string s5_630 { get; set; }
    }
}
