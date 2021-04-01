using System;
using MicrosoftReference.IntroductionToClasses;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestMicrosoftReferenceCode.TestIntroductionToClasses
{
    [TestFixture]
    public class TestBankAccount
    {
        public static IEnumerable<TestCaseData> SingleAccountOwner
        {
            get
            {
                yield return new ("John Doe", 2000m);
            }
        }
        
        public static IEnumerable<(string, decimal)[]> SeveralAccountOwners
        {
            get
            {
                yield return new[] { ("John Doe", 2000.0m), ("Jane Doe", 4000.0m) };
            }
        }
        
        public static IEnumerable<TestCaseData> AccountTransactions
        {
            get
            {
                yield return new ("John Doe", 2000m, new List<decimal>(){400m, 324.54m});
            }
        }
        
        [Test]
        [TestCaseSource(nameof(SingleAccountOwner))]
        public void TestConstruction(string name, decimal initialBalance)
        {
            var account = new BankAccount(name, initialBalance);
            
            Assert.AreEqual(name,account.Owner);
            Assert.AreEqual(initialBalance,account.Balance);
        }
        
        [Test]
        [TestCaseSource(nameof(SeveralAccountOwners))]
        public void TestAccountNumber((string, decimal) firstAccountData, (string, decimal) secondAccountData)
        {
            var firstAccount = new BankAccount(firstAccountData.Item1, firstAccountData.Item2);
            var secondAccount = new BankAccount(firstAccountData.Item1, firstAccountData.Item2);
            
            Assert.AreEqual(1,Convert.ToDecimal(secondAccount.Number) - Convert.ToDecimal(firstAccount.Number));
        }
        
        [Test]
        [TestCaseSource(nameof(AccountTransactions))]
        public void TestMakeDeposit(string name, decimal initialBalance, List<decimal> transactions)
        {
            var account = new BankAccount(name, initialBalance);
            var expectedBalance = transactions.Sum(item => item) + initialBalance;
                
            foreach (var transaction in transactions)
            {
                account.MakeDeposit(transaction, DateTime.Now, "deposit");
            }
            
            Assert.AreEqual(expectedBalance,account.Balance);
        }
        
        [Test]
        [TestCaseSource(nameof(AccountTransactions))]
        public void TestMakeWithdrawal(string name, decimal initialBalance, List<decimal> transactions)
        {
            var account = new BankAccount(name, initialBalance);
            var expectedBalance = transactions.Sum(item => -1*item) + initialBalance;
                
            foreach (var transaction in transactions)
            {
                account.MakeWithdrawal(transaction, DateTime.Now, "deposit");
            }
            
            Assert.AreEqual(expectedBalance,account.Balance);
        }
        
        [Test]
        [TestCaseSource(nameof(SingleAccountOwner))]
        public void TestDepositsCannotBeNegative(string name, decimal initialBalance)
        {
            var account = new BankAccount(name, initialBalance);

            Assert.That(() => account.MakeDeposit(-3000m, DateTime.Now, "invalid deposit"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCaseSource(nameof(SingleAccountOwner))]
        public void TestWithdrawalsCannotBeNegative(string name, decimal initialBalance)
        {
            var account = new BankAccount(name, initialBalance);

            Assert.That(() => account.MakeWithdrawal(-3000m, DateTime.Now, "invalid withdrawal"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        
        [Test]
        [TestCaseSource(nameof(SingleAccountOwner))]
        public void TestWithdrawalsCannotCauseDeficit(string name, decimal initialBalance)
        {
            var account = new BankAccount(name, initialBalance);

            Assert.That(() => account.MakeWithdrawal(6000m, DateTime.Now, "withdrawal causes deficit"), Throws.TypeOf<InvalidOperationException>());
        }
    }
}