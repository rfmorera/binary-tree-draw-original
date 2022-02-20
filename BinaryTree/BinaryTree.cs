/*
 * Developed by Mojtaba Hosseini, hmojtaba@live.com, www.gnegar.com
 * Feel free to change, edit, and reuse this application. :)
 */
using System;
using System.Drawing;

namespace BinaryTree
{
    public class BinaryTree
    {
        // the root node
        public Node RootNode { get; private set; }

        public BinaryTree()
        {
            RootNode = new Node(int.MinValue);
        }

        // count of nodes in the tree
        public int Count { get { return RootNode.Right.Count; } }

        // adds a node to the tree
        public bool Add(Node node)
        {
            return RootNode.Add(node);
        }
        
        // removes the node containing the inserted value.
        public bool Remove(int value)
        {
            bool isRootNode;
            var res = RootNode.Remove(value, out isRootNode);
            return !isRootNode && res;// return false if the inserted value is on rootNode, or the value does not exist on any of nodes
        }
        // draw
        public Image Draw()
        {
            GC.Collect();// collects the unreffered locations on the memory
            int temp;
            return RootNode.Right == null ? null : RootNode.Right.Draw(out temp);
        }

        // returns true if the current node exist in the tree
        public bool Exists(int item)
        {
            return RootNode.Exists(item);
        }
    }
}
