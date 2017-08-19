using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoutineGenerator.ENTITY;
using RoutineGenerator.LOGL;

namespace RoutineGenerator.UI
{
    public partial class RoomAdding : MetroFramework.Forms.MetroForm
    {
        private Start upForm = new Start();

        public RoomAdding(Start upForm)
        {
            this.upForm = upForm;// main from ar reference
            InitializeComponent();
        }

        private void Select_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            this.upForm.Activate();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            var temp = this.gridRoom.Rows;
            Operations x = new Operations(); 
            List<Room> list = new List<Room>();

            for (int i = 0; i < this.gridRoom.Rows.Count - 1; i++)
            {
                Room r = new Room();
                r.RoomNo = temp[i].Cells[0].Value.ToString().ToUpper();
                r.Duration = Convert.ToDouble(temp[i].Cells[1].Value);
                r.Lab = Convert.ToBoolean(temp[i].Cells[2].Value);
                r.Department = temp[i].Cells[3].Value.ToString().ToUpper();
                r.TimeSlots = Convert.ToInt32(temp[i].Cells[4].Value);

                
                if (r.Duration == 1.5 && r.TimeSlots > 7)
                {
                    MessageBox.Show("for rooms duration of 1.5hours can have at most 7 timeslots per day");
                    return;
                }
                else if (r.Duration == 2 && r.TimeSlots > 5)
                {
                    MessageBox.Show("for rooms duration of 2hours can have at most 5 timeslots per day");
                    return;
                }
                else if (r.Duration == 3 && r.TimeSlots > 4)
                {
                    MessageBox.Show("for labs timeslots can be at most 4 per day");
                    return;
                }

                if (r.Duration == 1.5 || r.Duration == 2 || r.Duration == 3)
                    list.Add(r);
                else
                {
                    MessageBox.Show("durations can be 1.5/ 2/ 3 hours");
                }
            }
            int count = x.AddRoom(list);
            string s = string.Format("{0} rows inserted in rooms", count);
            MessageBox.Show(s);
        }
        
        private void RoomAdding_Load(object sender, EventArgs e)
        {
            Operations o = new Operations();
            List<Room> rs = o.ShowAllRooms();
            this.gridShowRooms.DataSource = rs;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = this.gridShowRooms.SelectedRows;
            int count = this.gridShowRooms.Rows.GetRowCount(DataGridViewElementStates.Selected);

            for (int i = 0; i < count; i++)
            {
                string name = row[i].Cells[0].Value.ToString();
                
                new Operations().DeleteRoom(name);
            }
        }

        private void btnChangeSlot_Click(object sender, EventArgs e)
        {
            Operations op = new Operations();
            var rows = this.gridShowRooms.SelectedRows;

            int count = this.gridShowRooms.Rows.GetRowCount(DataGridViewElementStates.Selected);

            for(int i=0; i<count; i++)
            {
                string id = Convert.ToString(rows[i].Cells[0].Value);
                int x = Convert.ToInt32(rows[i].Cells[4].Value);
                int timeSlots=0;

                if (Convert.ToDouble(rows[i].Cells[2].Value) == 1.5 && x <= 7)
                    timeSlots = x;
                else if(Convert.ToDouble(rows[i].Cells[2].Value) == 1.5 && x > 7)
                {
                    MessageBox.Show("for rooms duration of 1.5hours can have at most 7 timeslots per day");
                    return;
                }

                else if (Convert.ToDouble(rows[i].Cells[2].Value) == 2 && x <= 5)
                    timeSlots = x;
                else if(Convert.ToDouble(rows[i].Cells[2].Value) == 2 && x > 5)
                {
                    MessageBox.Show("for rooms duration of 2hours can have at most 5 timeslots per day");
                    return;
                }

                else if (Convert.ToDouble(rows[i].Cells[2].Value) == 3 && x <= 4)
                    timeSlots = x;
                else if(Convert.ToDouble(rows[i].Cells[2].Value) == 3 && x > 4)
                {
                    MessageBox.Show("for labs timeslots can be at most 4 per day");
                    return;
                }

                op.ChangeRoomTimeSlot(id, timeSlots);
            }
        }
    }
}
