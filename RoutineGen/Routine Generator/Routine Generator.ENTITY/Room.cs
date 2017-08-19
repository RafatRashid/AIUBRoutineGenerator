using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoutineGenerator.ENTITY
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public string RoomNo { get; set; }
        public string Department { get; set; }
        public double Duration { get; set; }
        public bool Lab { get; set; }
        public int TimeSlots { get; set; }
        public int SlotCount { get; set; }
    }
}
