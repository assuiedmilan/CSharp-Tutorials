using System;
using MicrosoftReference.IntroductionToClasses;
using NUnit.Framework;

namespace TestMicrosoftReferenceCode.TestIntroductionToClasses
{
    public class TestTransaction
    {
        private readonly DateTime _currentDate = DateTime.Now;
                
        [Test]
        [TestCase(10245.5, "This is a positive transaction")]
        [TestCase(-10245.5, "This is a negative transaction")]
        [TestCase(10245, "This is an integer transaction")]
        [TestCase(0, "This is a non transaction")]
        public void TestConstruction(decimal amount, string note)
        {
            var transaction = new Transaction(amount, _currentDate, note);
            
            Assert.AreEqual(transaction.Amount, amount);
            Assert.AreEqual(transaction.Date, _currentDate);
            Assert.AreEqual(transaction.Notes, note);
        }
    }
}