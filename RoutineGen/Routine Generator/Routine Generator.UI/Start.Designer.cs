namespace RoutineGenerator.UI
{
    partial class Start
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.tileGenerate = new MetroFramework.Controls.MetroTile();
            this.tileCourseAdd = new MetroFramework.Controls.MetroTile();
            this.tileRoom = new MetroFramework.Controls.MetroTile();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.DarkRed;
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.tileGenerate);
            this.metroPanel1.Controls.Add(this.tileCourseAdd);
            this.metroPanel1.Controls.Add(this.tileRoom);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1060, 570);
            this.metroPanel1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // tileGenerate
            // 
            this.tileGenerate.ActiveControl = null;
            this.tileGenerate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tileGenerate.Location = new System.Drawing.Point(59, 278);
            this.tileGenerate.Name = "tileGenerate";
            this.tileGenerate.Size = new System.Drawing.Size(942, 228);
            this.tileGenerate.Style = MetroFramework.MetroColorStyle.Yellow;
            this.tileGenerate.TabIndex = 2;
            this.tileGenerate.Text = "Generate Routine";
            this.tileGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tileGenerate.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tileGenerate.UseSelectable = true;
            this.tileGenerate.Click += new System.EventHandler(this.tileGenerate_Click);
            // 
            // tileCourseAdd
            // 
            this.tileCourseAdd.ActiveControl = null;
            this.tileCourseAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tileCourseAdd.Location = new System.Drawing.Point(619, 40);
            this.tileCourseAdd.Name = "tileCourseAdd";
            this.tileCourseAdd.Size = new System.Drawing.Size(382, 197);
            this.tileCourseAdd.Style = MetroFramework.MetroColorStyle.Lime;
            this.tileCourseAdd.TabIndex = 1;
            this.tileCourseAdd.Text = "Add/Delete Course list";
            this.tileCourseAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tileCourseAdd.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tileCourseAdd.UseSelectable = true;
            this.tileCourseAdd.Click += new System.EventHandler(this.tileCourseAdd_Click);
            // 
            // tileRoom
            // 
            this.tileRoom.ActiveControl = null;
            this.tileRoom.BackColor = System.Drawing.Color.White;
            this.tileRoom.ForeColor = System.Drawing.Color.White;
            this.tileRoom.Location = new System.Drawing.Point(59, 40);
            this.tileRoom.Name = "tileRoom";
            this.tileRoom.Size = new System.Drawing.Size(387, 197);
            this.tileRoom.Style = MetroFramework.MetroColorStyle.Brown;
            this.tileRoom.TabIndex = 0;
            this.tileRoom.Text = "Add/Delete Rooms";
            this.tileRoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tileRoom.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.tileRoom.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tileRoom.UseSelectable = true;
            this.tileRoom.Click += new System.EventHandler(this.tileRoom_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.metroPanel1);
            this.Name = "Start";
            this.Style = MetroFramework.MetroColorStyle.Black;
            this.Text = "Routine Generator";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroTile tileRoom;
        private MetroFramework.Controls.MetroTile tileCourseAdd;
        private MetroFramework.Controls.MetroTile tileGenerate;


    }
}

