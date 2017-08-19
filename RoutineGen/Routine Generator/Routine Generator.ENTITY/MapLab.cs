using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineGenerator.ENTITY
{
    public class MapLab
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SlNo { get; set; }
        public string RoomNo { get; set; }
        public string DaySlot { get; set; }

        public string s8_11 { get; set; }
        public string s11_2 { get; set; }
        public string s2_5 { get; set; }
        public string s5_8 { get; set; }
    }
}
