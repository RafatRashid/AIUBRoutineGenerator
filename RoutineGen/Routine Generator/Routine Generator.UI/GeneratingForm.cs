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
    public partial class GeneratingForm : MetroFramework.Forms.MetroForm
    {
        private Start upForm;
        public GeneratingForm(Start f)
        {
            this.upForm = f;
            InitializeComponent();
            this.txtRoom.Hide();
            this.txtCourse.Hide();
            this.metroLabel1.Hide();
            this.metroLabel2.Hide();
        }

        private void GeneratingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.upForm.Activate();
        }

        private void btnGenerate_Click(object sender, EventArgs e)                  //............//
        {
            Operations gen = new Operations();
            if (this.txtRoom.Enabled == true && this.txtCourse.Enabled == true)
            {
                string rnum = this.txtRoom.Text;
                if (rnum == "")
                {
                    MessageBox.Show("please enter valid room number");
                    return;
                }
                
                string cname = this.txtCourse.Text;
                if (cname == "")
                {
                    MessageBox.Show("please enter valid course name");
                    return;
                }
                gen.GetCourse(cname);

                gen.Decide(gen.GetCourse(cname), gen.GetRoom(rnum));
               

                return;
            }

            List<Room> roomList = new List<Room>();
            List<Course> allCourseList = new List<Course>();
            List<string> courseNames = new List<string>();
            
            var presentRoomTab = this.tabsRoomDepts.SelectedTab;
            var presentCourseTab = this.tabCourseDept.SelectedTab;
            
            if (presentRoomTab.Text == "EEE")
                roomList = gen.ShowEEERooms();
            else if(presentRoomTab.Text == "CSE")
                roomList = gen.ShowCSERooms();
            else if (presentRoomTab.Text == "BBA")
                roomList = gen.ShowBBARooms();
            else if (presentRoomTab.Text == "ARTS")
                roomList = gen.ShowARTRooms();

            if (presentCourseTab.Text == "EEE")
                allCourseList = gen.ShowEEECourses();
            else if (presentCourseTab.Text == "CSE")
                allCourseList = gen.ShowCSECourses();
            else if (presentCourseTab.Text == "BBA")
                allCourseList = gen.ShowBBACourses();
            else if (presentCourseTab.Text == "ARTS")
                allCourseList = gen.ShowARTCourses();

            int j = -1;
            for (int i = 0; i < allCourseList.Count; i++)
            {
                if (courseNames.Count == 0 || courseNames[j] != allCourseList[i].Name)
                {
                    courseNames.Add(allCourseList[i].Name);
                    j++;
                }
            }
           
            int courseCounter = 0, roomCounter = 0, roomFullCounter=0;
            bool cantMap = true;
            while (true)
            {
                if (gen.isRoomFull(roomList[roomCounter]))
                {
                    if (roomFullCounter == roomList.Count)
                    {
                        MessageBox.Show("Room full");
                        break;
                    }
                    else
                    {
                        roomFullCounter++;
                        roomCounter++;
                    }
                }
                
                if (gen.Decide(gen.GetCourse(courseNames[courseCounter]), roomList[roomCounter]) == 1)
                    roomCounter++;
                
                else if(gen.isAllMapped(courseNames[courseCounter]))
                {
                    courseCounter++;
                    cantMap = false;
                }

                if (roomCounter == roomList.Count)
                {
                    roomCounter = 0;
                    if(cantMap)
                    {
                        MessageBox.Show("no rooms available or room and course type mismatch");
                        break;
                    }
                }
                if (courseCounter == courseNames.Count)
                {
                    MessageBox.Show("course all mapped");
                    break;
                }
            }
        }
        
        private void GeneratingForm_Load(object sender, EventArgs e)
        {
            Operations op = new Operations();

            List<Course> crsEEE = op.ShowEEECourses();
            List<Course> crsCSE = op.ShowCSECourses();
            List<Course> crsBBA = op.ShowBBACourses();
            List<Course> crsART = op.ShowARTCourses();

            List<Room> rmEEE =  op.ShowEEERooms();
            List<Room> rmCSE = op.ShowCSERooms();
            List<Room> rmBBA = op.ShowBBARooms();
            List<Room> rmART = op.ShowARTRooms();

            this.gridCourseEEE.DataSource = crsEEE;
            this.gridRoomEEE.DataSource = rmEEE;

            this.gridCourseBBA.DataSource = crsBBA;
            this.gridRoomBBA.DataSource = rmBBA;

            this.gridCourseCSE.DataSource = crsCSE;
            this.gridRoomCSE.DataSource = rmCSE;

            this.gridCourseArts.DataSource = crsART;
            this.gridRoomArts.DataSource = rmART;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DisplayForm disp = new DisplayForm();
            disp.Show();
        }
        
        private void toggleSingle_CheckedChanged(object sender, EventArgs e)
        {
            if(this.toggleSingle.Checked)
            {
                this.txtCourse.Show();
                this.txtRoom.Show();
                this.txtCourse.Enabled = true;
                this.txtRoom.Enabled = true;
                this.metroLabel1.Show();
                this.metroLabel2.Show();
            }
            else
            {
                this.txtCourse.Hide();
                this.txtRoom.Hide();
                this.txtCourse.Enabled = false;
                this.txtRoom.Enabled = false;
                this.metroLabel1.Hide();
                this.metroLabel2.Hide();
            }
        }
    }
}
