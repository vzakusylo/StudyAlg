using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;
using System.Collections;


namespace template
{
    // https://leetcode.com/problems/merge-two-sorted-lists/
    [TestClass]
    public class alg021
    {
       
        [TestMethod]
        public void Main()
        {
            // list1 = [1,2,4], list2 = [1,3,4]
            ListNode list1 = new ListNode(1, new ListNode(2, new ListNode(4, null)));
            ListNode list2 = new ListNode(1, new ListNode(3, new ListNode(4, null)));

            // [1,1,2,3,4,4]
            var res = MergeTwoLists(list1, list2);
        }

        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            ListNode result = null;
            ListNode last = null;

            var list1Iterator = list1;
            var list2Iterator = list2;

            while (list1Iterator != null || list2Iterator != null)
            {
                if (list1Iterator != null && list2Iterator != null)
                {
                    if (list1Iterator.val >= list2Iterator.val)
                    {
                        if (result == null)
                        {
                            last = new ListNode(list1Iterator.val, null);
                            result = new ListNode(list2Iterator.val, last);
                            
                        }
                        else
                        {
                            last.next = new ListNode(list2Iterator.val, new ListNode(list1Iterator.val, null));
                            last = last.next.next;
                        }
                    }
                    else
                    {
                        if (result == null)
                        {
                            last = new ListNode(list2Iterator.val, null);
                            result = new ListNode(list1Iterator.val, last);
                        }
                        else
                        {
                            //result.next = list1Iterator;
                            //result.next.next = list2Iterator;
                            
                            last.next = new ListNode(list1Iterator.val, new ListNode(list2Iterator.val, null));
                            last = last.next.next;
                        }
                    }
                }

                if (list1Iterator != null && list2Iterator == null)
                {
                    if (result == null)
                    {
                        last = new ListNode(list1Iterator.val, null);
                        result = last;
                    }
                    else
                    {
                        last.next = new ListNode(list1Iterator.val, null);
                    }
                }

                if (list1Iterator == null && list2Iterator != null)
                {
                    if (result == null)
                    {
                        last = new ListNode(list2Iterator.val, null);
                        result = last;
                    }
                    else
                    {
                        last.next = new ListNode(list2Iterator.val, null);
                    }
                }

                list1Iterator = list1Iterator?.next ?? null;
                list2Iterator = list2Iterator?.next ?? null;
            }

            return result;
        }

    }
}
