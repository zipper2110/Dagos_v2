using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos.Project
{
    public class DagosImage
    {
        /*
         * This class should contain representation of CT images stack. Main goal is to store pixels data and provide access to it
         */

        private byte[,,] imageData;

        public byte[,,] ImageData { get { return imageData; } }

        public int Width { get { return imageData.GetLength(0); } }
        public int Height { get { return imageData.GetLength(1); } }
        public int SliceCount { get { return imageData.GetLength(2); } }

        public DagosImage(byte[,,] imageData)
        {
            this.imageData = imageData;
        }

        public DagosImage(FileInfo[] imageFiles)
        {
            // TODO: implement import image from files
        }

        public byte getPointValue(DagosPoint point)
        {
            return getPointValue(point.X, point.Y, point.Z);
        }

        public byte getPointValue(int x, int y, int z)
        {
            return imageData[x, y, z];
        }

        public bool hasPoint(int x, int y, int z)
        {
            return x > -1 && x < this.Width &&
                    y > -1 && y < this.Height &&
                    z > -1 && z < this.SliceCount;
        }
    }
}
