using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;
using TestNinjaCore.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperHelperTests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _mailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _housekeeper;

        [SetUp]
        public void Setup()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            unitOfWork.Setup(u => u.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _mailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object,
                                              _statementGenerator.Object,
                                              _mailSender.Object,
                                              _xtraMessageBox.Object);

        }
        [Test]
        public void SendStatementEmails_WhenCalled_ShouldGenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));

        }
    }
}
