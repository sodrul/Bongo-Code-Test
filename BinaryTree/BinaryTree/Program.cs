using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            BinaryTree tree = new BinaryTree(values);

            Console.Write("Tree Nodes are- 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 {0}", Environment.NewLine);
                
            int node1;
            
            Console.Write("Enter a node1- ");
            node1 = Convert.ToInt32(Console.ReadLine());

            int node2;
            Console.Write("Enter a node2- ");
            node2 = Convert.ToInt32(Console.ReadLine());

            tree.findLCA(node1, node2);
            
        }
    }    

    class BinaryTree
    {
        int data;
        BinaryTree left;
        BinaryTree right;
        public BinaryTree(int[] values) : this(values, 0) { }

        BinaryTree(int[] values, int index)
        {
            Load(this, values, index);            

        }

        void Load(BinaryTree tree, int[] values, int index)
        {
            this.data = values[index];
            if (index * 2 + 1 < values.Length)
            {
                this.left = new BinaryTree(values, index * 2 + 1);
            }
            if (index * 2 + 2 < values.Length)
            {
                this.right = new BinaryTree(values, index * 2 + 2);
            }
        }

        
        public void findLCA(int n1, int n2)
        {
            var lca=findLCA(this, n1, n2);
            Console.WriteLine("Least Common Ancestor of ({0}, {1}) = {2}", n1, n2, lca.data);
            Console.ReadKey();
        }
        BinaryTree findLCA(BinaryTree node, int n1, int n2)
        {
            if (node == null)
                return null;
            
            if (node.data == n1 || node.data == n2)
                return node;
            
            BinaryTree left_lca = findLCA(node.left, n1, n2);
            BinaryTree right_lca = findLCA(node.right, n1, n2);
            
            if (left_lca != null && right_lca != null)
                return node;
            
            return (left_lca != null) ? left_lca : right_lca;
        }
      
        
    }
}
