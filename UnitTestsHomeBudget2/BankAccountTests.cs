using HomeBudget2.Controllers;
using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestsHomeBudget2
{

    [TestFixture]
    class BankAccountTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
       
        private BankAccountsController _controller;
        private BankAccountViewModel _bankAccountVm;
        private BankAccount _bankAccount;

        private string _accountName;
        private string _userId;
        private int _initialBalance;
        private int _id;


        [SetUp]
        public void Init()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
           
           
            _accountName = "Tomek Account";
            _initialBalance = 1300;
            _id = 2;
            _userId = "123.456lk";

            _controller = new BankAccountsController(_mockUnitOfWork.Object);

            _bankAccount = new BankAccount
            {
                Id = _id,
                Balance = _initialBalance,
                AccountName = _accountName,
                UserId = _userId
            };

            _bankAccountVm = new BankAccountViewModel();
            _bankAccountVm.BankAccount = _bankAccount;
        }

        [Test]
        public void Should_CreateAccount_WhenDataIsSpecified()
        {
            _mockUnitOfWork.Object.BankAccountRepo.Create(_bankAccount);

            var mockAccount = _mockUnitOfWork.Object.BankAccountRepo.GetWhere(ba => ba.Id == _bankAccount.Id).First();

            Assert.That(mockAccount.AccountName, Is.EqualTo(_accountName));
            Assert.That(mockAccount.Balance, Is.EqualTo(_initialBalance));
        }

        public static IUnitOfWork MockUnitOfWork(List<BankAccount> bankAccounts)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

           // todo: fix it --> mockUnitOfWork.Setup(x => x.BankAccountRepo.Students()).Returns(bankAccounts.AsQueryable());

            return mockUnitOfWork.Object;
        }

    }
}
