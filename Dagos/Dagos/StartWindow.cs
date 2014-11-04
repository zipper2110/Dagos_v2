using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dagos
{
    public partial class StartWindow : Form
    {
        public delegate void OpenProject(string ProjectPath);
        public event OpenProject onSelectProject;

        public delegate void NewProject();
        public event NewProject onNewProject;

        public StartWindow()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (onNewProject != null)
            {
                onNewProject();
            }
            this.Close();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openProjectDialog = new OpenFileDialog();
            openProjectDialog.Filter = "Dagos project file|*.dgs";
            if (openProjectDialog.ShowDialog() != DialogResult.OK) return;

            if (onSelectProject != null)
            {
                onSelectProject(openProjectDialog.FileName);
            }
            this.Close();
        }
    }
}
