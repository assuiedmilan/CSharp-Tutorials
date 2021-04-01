using LeetCode.Algorithms;
using NUnit.Framework;

namespace TestLeetCode.TestAlgorithms
{
    public class TestAddTwoNumbers
    {
        [Test]
        [TestCase(new [] {2, 4, 3},new [] {5,6,4},new [] {7, 0, 8},807)]
        [TestCase(new [] {0},new [] {0},new [] {0},0)]
        [TestCase(new [] {9,9,9,9,9,9,9},new [] {9,9,9,9},new [] {8, 9, 9, 9, 0, 0, 0, 1},10009998)]
        [TestCase(new [] {9},new [] {1,9,9,9,9,9,9,9,9,9},new [] {0,0,0,0,0,0,0,0,0,0,1},10000000000)]
        public void Test1(int[] firstArray, int[] secondArray, int[] expectedListNodesValues, long expectedSum)
        {
            var first = ListNode.Factory(firstArray);
            var second = ListNode.Factory(secondArray);
            var actualListNode = AddTwoNumbers.AddFromListsOfDigits(first, second);
            var actualSum = AddTwoNumbers.ListNodeToInt(actualListNode);

            Assert.AreEqual(expectedSum, actualSum);

            foreach (int digit in expectedListNodesValues)
            {
                Assert.AreEqual(digit, actualListNode.Val);
                actualListNode = actualListNode.Next;
            }
        }
    }
}