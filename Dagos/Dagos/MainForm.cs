using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Dagos.Project;
using Dagos.Import;

namespace Dagos
{
    public partial class MainForm : Form
    {
        private DagosProject currentProject;
        private DagosStudy currentStudy;

        public MainForm()
        {
            InitializeComponent();
        }

        private void reopenProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void label2_Click(object sender, System.EventArgs e)
        {

        }

        private void startProjectAnalysisToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void importImagesFolderToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ImportImagesFolder importImagesFolderForm = new ImportImagesFolder();
            importImagesFolderForm.onImportImage += (image) =>
            {
                if (image != null)
                {
                    currentStudy.Image = image;
                    MessageBox.Show("Images folder was imported successfully. Created image: " 
                        + image.Width + "x" + image.Height + "x" + image.SliceCount + ".");

                }
                else
                {
                    MessageBox.Show("Failed to load images folder. Nothing was imported.");
                }
            };
            importImagesFolderForm.ShowDialog();
        }

        private void Form1_Shown(object sender, System.EventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.onSelectProject += onProjectSelected;
            startWindow.onNewProject += onNewProject;
            startWindow.ShowDialog(this);
        }

        public void onProjectSelected(string projectPath)
        {
            MessageBox.Show(projectPath);
        }

        public void onNewProject()
        {
            currentProject = new DagosProject();
            updateStudies();
        }

        public void updateStudies()
        {
            comboBoxCurrentStudy.Items.Clear();

            foreach (DagosStudy study in currentProject.Studies)
            {
                comboBoxCurrentStudy.Items.Add(study.Name);
            }

            if(!comboBoxCurrentStudy.Size.IsEmpty) {
                comboBoxCurrentStudy.SelectedIndex = 0;
            }
        }

        private void comboBoxCurrentStudy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            currentStudy = currentProject.Studies.ElementAt(comboBoxCurrentStudy.SelectedIndex);
        }

        private void newProjectToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            onNewProject();
        }
    }
}
