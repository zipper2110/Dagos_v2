using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos.Project
{
    class Project
    {
        /*
         * Project should contain a set of studies that are performed in the project
         */

        private List<Study> studies;
        public List<Study> Studies
        {
            get
            {
                return studies;
            }
        }

        public Project()
        {

        }

        public Project(List<Study> studies)
        {
            this.studies = studies;
        }
    }
}
