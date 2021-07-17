using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private InstallerHelper installer;
        private Mock<IWebDownloader> downloaderMock;
        [SetUp]
        public void SetUp()
        {
            downloaderMock = new Mock<IWebDownloader>();
            installer = new InstallerHelper("destination", downloaderMock.Object);
        }
        [Test]
        public void DownloadInstaller_ReturnTrue()
        {
            var result = installer.DownloadInstaller(It.IsAny<string>(), It.IsAny<string>());

            Assert.That(result, Is.True);
        }
        [Test]
        public void DownloadInstaller_ReturnFalse()
        {
            downloaderMock.Setup(s => s.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            var result = installer.DownloadInstaller(It.IsAny<string>(), It.IsAny<string>());

            Assert.That(result, Is.False);
        }
    }
}
