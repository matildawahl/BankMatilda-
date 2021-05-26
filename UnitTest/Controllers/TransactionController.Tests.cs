using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;
using BankMatilda.Controllers;
using BankMatilda.Services;
using BankMatilda.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture;
using BankMatilda.Models;
using BankMatilda.Data;

namespace UnitTest.Controllers
{
    [TestClass]
    public class TransactionControllerTests: BaseTest
    {
        private TransactionsController sut;
        private Mock<IRepository> RepositoryMock;
        public TransactionControllerTests()
        {
            RepositoryMock = new Mock<IRepository>();
            sut = new TransactionsController(RepositoryMock.Object);

        }

        [TestMethod]
        public void Dont_Update_Account_If_SufficientBalance_False()
        {
            var viewModel = fixture.Create<TransactionWithdrawViewModel>();
            var account = fixture.Create<Account>();
            RepositoryMock.Setup(a => a.GetAccountById(viewModel.AccountId)).Returns(account);

            RepositoryMock.Setup(e => e.CheckIfSufficientBalance(viewModel.AmountToWithdraw, account.Balance))
                .Returns(false);
            RepositoryMock.Setup(e=>e.UpdateAccount(account)).Returns(account);
            
            var result = sut.Withdraw(viewModel);

            RepositoryMock.Verify(e => e.UpdateAccount(account), Times.Never);
        }

       
    }
}
