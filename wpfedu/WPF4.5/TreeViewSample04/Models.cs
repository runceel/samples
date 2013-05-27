using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewSample04
{
    public abstract class ExplorerNode
    {
        public ExplorerNode Parent { get; set; }
        public string Name { get; set; }
        public abstract long Size { get; }
        public string FullPath
        {
            get
            {
                if (this.Parent != null)
                {
                    return Path.Combine(this.Parent.FullPath, this.Name);
                }
                return this.Name;
            }
        }
    }

    public class DirectoryNode : ExplorerNode
    {
        public override long Size
        {
            get 
            {
                return this.ChildNodes.Sum(n => n.Size);
            }
        }
        public IList<ExplorerNode> ChildNodes
        {
            get
            {
                // TODO : 例外が出たら諦める？
                var fullPath = this.FullPath;
                return Directory.GetDirectories(fullPath)
                    .Where(p => File.GetAccessControl(p).)
                    .Select(p => new DirectoryNode { Name = p, Parent = this })
                    .Cast<ExplorerNode>()
                    .Concat(Directory.GetFiles(fullPath)
                        .Where(p => !new FileInfo(p).Attributes.HasFlag(FileAttributes.System))
                        .Select(p => new FileNode { Name = p, Parent = this }))
                    .ToList();
            }
        }
    }

    public class FileNode : ExplorerNode
    {
        public override long Size 
        {
            get { return new FileInfo(this.FullPath).Length; }
        }
    }
}
