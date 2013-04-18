using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewSample04
{
    public abstract class ExplorerNode
    {
        public string Name { get; set; }
        public abstract int Size { get; }
    }

    public class DirectoryNode : ExplorerNode
    {
        public override int Size
        {
            get 
            {
                return this.ChildNodes.Sum(n => n.Size);
            }
        }
        public List<ExplorerNode> ChildNodes { get; set; }
    }

    public class FileNode : ExplorerNode
    {
        private int size;

        public FileNode(int size)
        {
            this.size = size;
        }

        public override int Size 
        {
            get { return this.size; }
        }
    }
}
