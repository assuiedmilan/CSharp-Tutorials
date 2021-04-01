using System;
namespace LeetCode.Algorithms
{
    public class ListNode {
         
        public readonly int Val;
        public readonly ListNode Next;

        public static ListNode Factory(int[] table, bool reverse=true)
        {
            ListNode previousNode = null;
            ListNode theNode = null;
            
            if (reverse) {
                Array.Reverse(table);
            }
            
            foreach (var i in table)
            {
                theNode = new ListNode(i, previousNode);
                previousNode = theNode;
            }

            return theNode;
        }
        
        private ListNode(int val=0, ListNode next=null) {
            Val = val;
            Next = next;
        }
         
    }

    public class AddTwoNumbers
    {

        public static ListNode AddFromListsOfDigits(ListNode l1, ListNode l2)
        {
            var sumAsChars = (ListNodeToInt(l1) + ListNodeToInt(l2)).ToString().ToCharArray();
            var sum = Array.ConvertAll(sumAsChars, c => (int) Char.GetNumericValue(c));
            
            return ListNode.Factory(sum, false);
        }

        public static long ListNodeToInt(ListNode l)
        {
            long coefficient = 1;
            var theList = l;
            long sum = 0;
            
            while (theList != null)
            {
                sum += theList.Val * coefficient;
                coefficient *= 10;
                theList = theList.Next;
            }

            return sum;
        }

    }
}