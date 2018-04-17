using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using Moq;
using NUnit.Framework;

namespace UnitTestsHomeBudget2
{
    [TestFixture]
    class HomeBudget2BankAccountTests
    {
        private string _accountName;
        private Mock<IBankAccountRepository> _mockBankAccountRepository;
        private int _initialBalance;


        [SetUp]
        public void Init()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            
            _accountName = "Tomek Account";
            _initialBalance = 1300;

            BankAccount bankAccount = new BankAccount()
            {
                Balance = _initialBalance,
                InitialBalance = _initialBalance,
                AccountName = _accountName
            };

        }

        [Test]
        public void Should_CreateCompany_WhenNameIsSpecified()
        {
            Assert.That(_accountName, Is.EqualTo("Tomek Account"));

        }

    }
}
