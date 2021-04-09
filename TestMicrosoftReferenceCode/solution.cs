using System;
using NUnit.Framework;

namespace TestMicrosoftReferenceCode
{
    class Solution {
        public int solution(int[] A)
        {
            Array.Sort(A);
            var firstPositiveIndex = Array.FindIndex(A, isPositive);

            if (firstPositiveIndex == -1)
            {
                return 1;
            }

            var previousValue = A[firstPositiveIndex];
            var currentValue = A[firstPositiveIndex+1];
            var index = firstPositiveIndex+2;

            while (currentValue - previousValue < 2 && index < A.Length)
            {
                previousValue = currentValue;
                currentValue = A[index];
                index++;
            }

            if (currentValue - previousValue < 2)
            {
                return currentValue + 1;
            }

            return previousValue + 1;
        }

        private static bool isPositive(int i)
        {
            return i >= 0;
        }
    }

    public class TestDataSets
    {
        [Test]
        public void TestDeckGeneration()
        {
            Solution s = new Solution();
            s.solution(new[] {1});
        }
    }
}