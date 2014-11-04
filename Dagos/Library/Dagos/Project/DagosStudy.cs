using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos.Project
{
    public class DagosStudy
    {
        /*
         * Study stores all info about one CT series: images, lung, formations, comment
         */

        private DagosProject project;

        public string Name { get; set; }

        public DagosImage Image { get; set; }
        public DagosMask LungMask { get; set; }

        public List<DagosFormation> Formations { get { return Formations; } }

        public string Comment { get; set; }

        public DagosStudy(DagosProject project, string name)
        {
            this.project = project;
            this.Name = name;
        }

        public DagosStudy(DagosProject project)
        {
            this.project = project;
            this.Name = "Study_" + new Random().Next(100000, 1000000);
        }

        public void addFormation(DagosFormation formation)
        {
            Formations.Add(formation);
        }
    }
}
