using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DnDHelper
{
    [Serializable]
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Block[,] BlockMap { get; set; }
        public List<Block> AllNamedBlocks
        {
            get
            {
                List<Block> blk = new List<Block>();
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        if (BlockMap[i, j] != null)
                        {
                            if (BlockMap[i, j].Name != null)
                            {
                                blk.Add(BlockMap[i, j]);
                            }
                        }
                    }
                }
                return blk;
            }
        }

        public Map()
        {
        }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            BlockMap = new Block[Width, Height];
        }
    }

    [Serializable]
    public class Block
    {
        public Color Color { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [XmlIgnore]
        public ImageBrush Image { get; set; }
        public string ImagePath { get; set; }
    }
}
