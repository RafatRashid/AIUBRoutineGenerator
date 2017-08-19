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
    public partial class CourseAdding : MetroFramework.Forms.MetroForm
    {
        private Start upForm;
        public CourseAdding(Start temp)
        {
            InitializeComponent();
            upForm = temp;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var temp = this.gridAddCourse.Rows;
            Operations x = new Operations();
            List<Course> list = new List<Course>();

            for (int i = 0; i < this.gridAddCourse.Rows.Count - 1; i++)
            {
                Course c = new Course(); 
                c.Name = temp[i].Cells[0].Value.ToString().ToUpper();
                c.Section = Convert.ToString(temp[i].Cells[1].Value).ToUpper();
                c.Duration = Convert.ToDouble(temp[i].Cells[2].Value);
                c.isLab = Convert.ToBoolean(temp[i].Cells[3].Value);
                c.Department = temp[i].Cells[4].Value.ToString().ToUpper();
                
                if(c.Duration == 1.5 || c.Duration == 2 || c.Duration == 3)
                    list.Add(c);
                else
                {
                    MessageBox.Show("durations can be 1.5/ 2/ 3 hours");
                }
            }
            int count = x.AddCourse(list);
            if(count == -999)
            {
                MessageBox.Show("Same Entry Exists in Database");
                return;
            }
            string s = string.Format("{0} rows inserted in Course", count);
            MessageBox.Show(s);
        }

        private void CourseAdding_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.upForm.Select();
        }

        private void CourseAdding_Load(object sender, EventArgs e)
        {
            Operations op = new Operations();

            List<Course> crs = op.ShowAllCourses();
            this.gridShowCourses.DataSource = crs ;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = this.gridShowCourses.SelectedRows;
            int count = this.gridShowCourses.Rows.GetRowCount(DataGridViewElementStates.Selected);

            for (int i = 0; i <count; i++)
            {
                string name = row[i].Cells[1].Value.ToString();
                string section = row[i].Cells[3].Value.ToString();

                new Operations().DeleteSections(name, section);
            }
            
        }
    }
}
