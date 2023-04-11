using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace binary_tree_right_side_view
{
    // https://leetcode.com/problems/binary-tree-right-side-view/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
          TreeNode root = new TreeNode{
            val = 1,
            left= new TreeNode{
                val = 2,
                right = new TreeNode{
                    val = 5
                }
            },
            right = new TreeNode{
                val = 3,
                right = new TreeNode{
                    val = 4
                }
            }
          };


          var res = RightSideView(root);

          Assert.AreEqual(1, res[0]);
          Assert.AreEqual(3, res[1]);
          Assert.AreEqual(4, res[2]);
        }

         public IList<int> RightSideView(TreeNode root) {
            List<int> resut = new List<int>();
            Queue<TreeNode> queue = new Queue<TreeNode>();

            if(root == null){
                return resut;
            }

            queue.Enqueue(root);

            while(queue.Count != 0){
                int size = queue.Count;
                for(int i = 0; i<size;i++){
                    TreeNode currentNode = queue.Dequeue();
                    if(i == 0){
                        resut.Add(currentNode.val);
                    }
                    if(currentNode.right != null){
                        queue.Enqueue(currentNode.right);
                    }
                    if(currentNode.left != null){
                        queue.Enqueue(currentNode.left);
                    }
                }
            }

            return resut;
         
        }
      
    }
}

/*

The method names Peek, Dequeue, Offer, and Enqueue are related to different queue operations and follow a convention used in some programming languages and libraries. Here's a brief explanation of each method:

Peek: This method allows you to look at the next item in the queue without removing it. It's called "Peek" because you can peek at the item at the front of the queue without actually dequeuing it.

Dequeue: This method removes and returns the item at the front of the queue. The name "Dequeue" is derived from the term "queue" and represents the action of taking an item out of the queue.

Offer: This method adds an item to the back of the queue. The name "Offer" is used in some programming languages, such as Java, as part of the java.util.concurrent.BlockingQueue interface. It implies that the method attempts to insert an item in the queue, and it might not be successful if the queue has limited capacity and is already full.

Enqueue: This method also adds an item to the back of the queue. The name "Enqueue" is derived from the term "queue" and represents the action of inserting an item into the queue. It's commonly used in C# with the Queue<T> class.

In summary, the method names Peek, Dequeue, Offer, and Enqueue are chosen to reflect the various operations performed on a queue data structure. The names are based on conventions used in different programming languages and libraries, and they help to describe the purpose of each method.

*/