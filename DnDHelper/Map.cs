using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Xml.Serialization;

namespace DnDHelper.WPF
{
    [Serializable]
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Block[,] BlockMap { get; set; }
        public List<StringBlock> TextBlocks { get; set; }
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
            TextBlocks = new List<StringBlock>();
        }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            BlockMap = new Block[Width, Height];
            TextBlocks = new List<StringBlock>();
        }

        public SerializableMap Serialize()
        {
            SerializableMap sMap = new SerializableMap();
            sMap.Width = Width;
            sMap.Height = Height;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (BlockMap[i, j] != null)
                    {
                        Block bl = BlockMap[i, j];
                        sMap.Blocks.Add(new SerializableBlock() { Color = string.Format("{0}.{1}.{2}.{3}", bl.Color.A, bl.Color.R, bl.Color.G, bl.Color.B), Description = bl.Description, Name = bl.Name, X = i, Y = j });
                    }
                }
            }
            sMap.TextBlocks = TextBlocks;
            return sMap;
        }

        public static Map Deserialize(SerializableMap sMap)
        {
            Map map = new Map(sMap.Width, sMap.Height);
            foreach (SerializableBlock sBlock in sMap.Blocks)
            {
                string [] colorArray = sBlock.Color.Split('.');
                map.BlockMap[sBlock.X, sBlock.Y] = new Block() { Color = Color.FromArgb(byte.Parse(colorArray[0]), byte.Parse(colorArray[1]), byte.Parse(colorArray[2]), byte.Parse(colorArray[3])), Name = sBlock.Name, Description = sBlock.Description };
            }
            map.TextBlocks = sMap.TextBlocks;
            return map;
        }
    }

    public class Block
    {
        public Color Color { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class StringBlock
    {
        public Point Position { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public Color Color { get; set; }
        public int Size { get; set; }
    }

    [Serializable]
    public class SerializableBlock
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    [Serializable]
    public class SerializableMap
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<SerializableBlock> Blocks { get; set; }
        public List<StringBlock> TextBlocks { get; set; }

        public SerializableMap()
        {
            Blocks = new List<SerializableBlock>();
            TextBlocks = new List<StringBlock>();
        }
    }

}
