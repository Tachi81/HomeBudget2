using HomeBudget2.Controllers;
using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace UnitTestsHomeBudget2
{
    [TestFixture]
    class BankAccountTests
    {
        private Mock<IBankAccountRepository> _mockBankAccountRepository;
        private Mock<IBankAccountLogic> _mockbankAccountLogic;

        private BankAccountsController _controller;
        private BankAccountViewModel _bankAccountVm;
        private BankAccount _bankAccount;

        private string _accountName;
        private int _initialBalance;
        private int _id;


        [SetUp]
        public void Init()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockbankAccountLogic = new Mock<IBankAccountLogic>();
            _accountName = "Tomek Account";
            _initialBalance = 1300;
            _id = 2;

            _controller = new BankAccountsController(_mockBankAccountRepository.Object, _mockbankAccountLogic.Object);

            _bankAccount = new BankAccount
            {
                Id = _id,
                Balance = _initialBalance,
                AccountName = _accountName
            };

            _bankAccountVm = new BankAccountViewModel();
            _bankAccountVm.BankAccount = _bankAccount;
        }

        [Test]
        public void Should_CreateAccount_WhenDataIsSpecified()
        {



            _mockBankAccountRepository.Object.Create(_bankAccount);

            var mockAccount = _mockBankAccountRepository.Object.GetWhere(ba => ba.Id == _bankAccount.Id).First();

            Assert.That(mockAccount.AccountName, Is.EqualTo(_accountName));
        }

    }
}
