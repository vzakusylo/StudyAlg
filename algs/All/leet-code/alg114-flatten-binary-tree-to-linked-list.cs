using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace flatten_binary_tree_to_linked_list
{
    // https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
            TreeNode root = BuildDefaultTree();

            Flatten(root);

            Assert.AreEqual(1, root.val);
            Assert.AreEqual(2, root.right.val);
            Assert.AreEqual(3, root.right.right.val);
            Assert.AreEqual(4, root.right.right.right.val);

            root = BuildDefaultTree();

            flatten(root);

            Assert.AreEqual(1, root.val);
            Assert.AreEqual(2, root.right.val);
            Assert.AreEqual(3, root.right.right.val);
            Assert.AreEqual(4, root.right.right.right.val);
        }

        private static TreeNode BuildDefaultTree()
        {
            return new TreeNode
            {
                val = 1,
                left = new TreeNode
                {
                    val = 2,
                    left = new TreeNode
                    {
                        val = 3
                    },
                    right = new TreeNode
                    {
                        val = 4
                    }
                },
                right = new TreeNode
                {
                    val = 5,
                    right = new TreeNode
                    {
                        val = 6
                    }
                }
            };
        }

        public void Flatten(TreeNode root) 
        {
            if(root == null) return;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while(stack.Count != 0 ){
                TreeNode currentNode = stack.Pop();
                
                if(currentNode.right != null){
                    stack.Push(currentNode.right);
                }

                if(currentNode.left != null){
                    stack.Push(currentNode.left);
                }

                if(stack.Count != 0){
                    currentNode.right = stack.Peek();
                }

                currentNode.left = null;
            }
        
        }

        private TreeNode prev = null;

        public void flatten(TreeNode root) {
            if (root == null)
                return;
            flatten(root.right);
            flatten(root.left);
            root.right = prev;
            root.left = null;
            prev = root;
        }
      
    }
}
