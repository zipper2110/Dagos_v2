using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos.Project
{
    class Image
    {
        /*
         * This class should contain representation of CT images stack. Main goal is to store pixels data and provide access to it
         */

        private byte[][][] imageData;

        public byte[][][] ImageData { get { return imageData; } }

        public int Width { get { return imageData.Length; } }
        public int Height { get { return imageData[0].Length; } }
        public int SliceCount { get { return imageData[0][0].Length; } }

        public Image(byte[][][] imageData)
        {
            this.imageData = imageData;
        }

        public Image(FileInfo[] imageFiles)
        {
            // TODO: implement import image from files
        }

        public byte getPointValue(Point point)
        {
            return imageData[point.X][point.Y][point.Z];
        }

        public byte getPointValue(int x, int y, int z)
        {
            return imageData[x][y][z];
        }

        public bool hasPoint(int x, int y, int z)
        {
            return x > -1 && x < this.Width &&
                    y > -1 && y < this.Height &&
                    z > -1 && z < this.SliceCount;
        }
    }
}
