using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos.Project
{
    public class DagosProject
    {
        /*
         * Project should contain a set of studies that are performed in the project
         */

        public delegate void StudyChanged();
        public StudyChanged onStudyChanged;

        private List<DagosStudy> studies;
        public List<DagosStudy> Studies
        {
            get
            {
                return studies;
            }
        }

        public DagosProject()
        {
            this.studies = new List<DagosStudy>();
            this.studies.Add(new DagosStudy(this));

            if (onStudyChanged != null)
            {
                onStudyChanged();
            }
        }

        public DagosProject(List<DagosStudy> studies)
        {
            this.studies = studies;
        }
    }
}
