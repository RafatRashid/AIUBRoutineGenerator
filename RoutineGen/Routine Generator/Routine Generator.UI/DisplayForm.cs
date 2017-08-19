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
    public partial class DisplayForm : MetroFramework.Forms.MetroForm
    {
        public DisplayForm()
        {
            InitializeComponent();
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            Operations o = new Operations();
            List<MapLab> labs = o.GetMapLabs();
            List<MapTheoryLab> tl = o.GetMapTheoryLabs();
            List<MapTheory> t = o.GetTheories();

            this.labs.DataSource = labs;
            this.theory2.DataSource = tl;
            this.theory1_1_5.DataSource = t;
        }
    }
}
