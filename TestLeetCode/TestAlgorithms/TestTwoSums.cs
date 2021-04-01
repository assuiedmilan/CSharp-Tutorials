using LeetCode.Algorithms;
using NUnit.Framework;
namespace TestLeetCode.TestAlgorithms
{
    public class TestTwoSums
    {
        [Test]
        [TestCase(new [] {2,7,11,15}, 9, new [] {0, 1})]
        [TestCase(new [] {3,3}, 6, new [] {0,1})]
        [TestCase(new [] {3,2,4}, 6, new [] {1,2})]
        [TestCase(new [] {3,2,4}, 27, null)]
        public void Test1(int[] testTable, int testTarget, int[] expectedResult)
        {
            Assert.AreEqual(expectedResult, TwoSum.ComputeValuesToAdd(testTable, testTarget) );
        }
    }

}