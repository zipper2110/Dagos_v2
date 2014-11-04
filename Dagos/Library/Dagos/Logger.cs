using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos
{
    class Logger
    {
        private Logger _logger;
        public Logger Get
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new Logger();
                }
                return _logger;
            }
        }

        private Logger()
        {

        }
    }
}
