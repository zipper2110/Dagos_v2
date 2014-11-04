using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dagos.Project
{
    public class DagosMask
    {
        /*
         * Binary mask contains info of what pixels of images are in this mask. Used for remembering sectors of image, such as lung or nodules
         */

        private bool[,,] maskData;

        public bool[,,] MaskData { get { return maskData; } }
        
        public int Width { get { return maskData.GetLength(0); } }
        public int Height { get { return maskData.GetLength(1); } }
        public int SliceCount { get { return maskData.GetLength(2); } }

        public int PointsCount
        {
            get
            {
                int counter = 0;
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        for (int z = 0; z < SliceCount; z++)
                        {
                            if (maskData[x, y, z])
                            {
                                counter++;
                            }
                        }
                    }
                }

                return counter;
            }
        }

        public DagosMask(bool[,,] maskData)
        {
            this.maskData = maskData;
        }

        public bool getPointValue(DagosPoint point)
        {
            return getPointValue(point.X, point.Y, point.Z);
        }

        public bool getPointValue(int x, int y, int z) 
        {
            return maskData[x, y, z];
        }

        public void setPointValue(DagosPoint point, bool value)
        {
            setPointValue(point.X, point.Y, point.Z, value);
        }

        public void setPointValue(int x, int y, int z, bool value)
        {
            maskData[x, y, z] = value;
        }

        public bool hasPoint(int x, int y, int z)
        {
            return x > -1 && x < this.Width &&
                    y > -1 && y < this.Height &&
                    z > -1 && z < this.SliceCount;
        }

        public DagosMask invert()
        {
            bool[,,] invertedMaskData = new bool[Width, Height, SliceCount];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int z = 0; z < SliceCount; z++)
                    {
                        invertedMaskData[x, y, z] = !getPointValue(x, y, z);
                    }
                }
            }

            return new DagosMask(invertedMaskData);
        }
    }
}
