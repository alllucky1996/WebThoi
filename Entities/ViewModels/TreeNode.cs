using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.ViewModels
{
    public class TreeNode
    {
        public string Id { get; set; } //Id of node
        public string Parent { get; set; } //maybe nullable
        public string Text { get; set; }  //text
        public bool Leaf { get; set; }
        public bool Expanded { get; set; }
        public List<TreeNode> Children { get; set; } // childrens if exists
        public string Url { get; set; } //maybe nullable
        public TreeNode()
        {
            Leaf = true;
            Children = new List<TreeNode>();
        }
    }

    public static class TreeNodeEx
    {
        public static TreeNode ToTree(this List<TreeNode> list)
        {
            if (list == null) throw new ArgumentNullException("list");
            var root = list.SingleOrDefault(x => x.Parent == null);
            if (root == null) throw new InvalidOperationException("root == null");

            PopulateChildren(root, list.Where(x => x.Parent != null).ToList());
            return root;
        }

        //recursive method
        private static void PopulateChildren(TreeNode node, ICollection<TreeNode> all)
        {
            var childs = all.Where(x => x.Parent.Equals(node.Id)).ToList();
            foreach (var item in childs)
            {
                node.Expanded = true;
                node.Leaf = false;
                node.Children.Add(item);
            }

            foreach (var item in childs)
                all.Remove(item);

            foreach (var item in childs)
                PopulateChildren(item, all);
        }
    }
}
