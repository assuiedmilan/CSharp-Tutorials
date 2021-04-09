using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Algorithms
{
    public class ListNode {

        public readonly int val;
        public readonly ListNode next;

        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }

    }

    public class AddTwoNumbersProblem
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var hasNext = true;
            var results = new List<int>();
            var carry = 0;

            while (hasNext)
            {
                var digit1 = l1?.val ?? 0;
                var digit2 = l2?.val ?? 0;

                var l1HasNext = l1?.next != null;
                var l2HasNext = l2?.next != null;

                var sum = digit1 + digit2 + carry;

                if (sum > 9)
                {
                    sum -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                results.Add(sum);

                hasNext = l1HasNext || l2HasNext || carry == 1;
                l1 = l1?.next;
                l2 = l2?.next;

            }

            return ListNodeFactory(results.ToArray());

        }

        public ListNode ListNodeFactory(int[] table)
        {
            ListNode previousNode = null;
            ListNode theNode = null;

            table = table.Reverse().ToArray();

            foreach (var i in table)
            {
                theNode = new ListNode(i, previousNode);
                previousNode = theNode;
            }

            return theNode;
        }

    }
}