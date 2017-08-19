using MetroFramework;
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
    public partial class Start : MetroFramework.Forms.MetroForm
    {   
        private RoomAdding form1;
        private CourseAdding c;
        private GeneratingForm gen;

        public Start()
        {
            InitializeComponent();
        }

        private void tileRoom_Click(object sender, EventArgs e)
        {
            this.form1 = new RoomAdding(this);
            this.form1.Show();
            
        }

        private void tileCourseAdd_Click(object sender, EventArgs e)
        {
            this.c = new CourseAdding(this);
            this.c.Show();
        }

        private void tileGenerate_Click(object sender, EventArgs e)
        {
            this.gen = new GeneratingForm(this);
            this.gen.Show();
        }

    }
}
