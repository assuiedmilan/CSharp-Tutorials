using LeetCode.Algorithms;
using NUnit.Framework;

namespace TestLeetCode.TestAlgorithms
{
    public class TestAddTwoNumbers
    {
        [Test]
        [TestCase(new [] {2, 4, 3},new [] {5,6,4},new [] {7, 0, 8})]
        [TestCase(new [] {2, 4, 3, 1},new [] {5,6,4},new [] {7, 0, 8, 1})]
        [TestCase(new [] {2, 4, 3},new [] {5,6,4, 1},new [] {7, 0, 8, 1})]
        [TestCase(new [] {0},new [] {0},new [] {0})]
        [TestCase(new [] {9,9,9,9,9,9,9},new [] {9,9,9,9},new [] {8, 9, 9, 9, 0, 0, 0, 1})]
        [TestCase(new [] {9},new [] {1,9,9,9,9,9,9,9,9,9},new [] {0,0,0,0,0,0,0,0,0,0, 1 })]
        [TestCase(new [] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},new [] {5,6,4},new [] {6,6,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1})]
        public void Test1(int[] firstArray, int[] secondArray, int[] expectedListNodesValues)
        {
            var instance = new AddTwoNumbersProblem();

            var first = instance.ListNodeFactory(firstArray);
            var second = instance.ListNodeFactory(secondArray);
            var actualListNode = instance.AddTwoNumbers(first, second);


            foreach (int digit in expectedListNodesValues)
            {
                Assert.AreEqual(digit, actualListNode.val);
                actualListNode = actualListNode.next;
            }

            Assert.That(actualListNode == null);
        }
    }
}