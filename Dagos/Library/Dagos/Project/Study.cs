using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos.Project
{
    class Study
    {
        /*
         * Study stores all info about one CT series: images, lung, formations, comment
         */

        private Project project;

        public Image Image { get; set; }
        public Mask LungMask { get; set; }

        public List<Formation> Formations { get { return Formations; } }

        private string Comment { get; set; }

        public Study(Project project)
        {
            this.project = project;
        }

        public void addFormation(Formation formation)
        {
            Formations.Add(formation);
        }
    }
}
