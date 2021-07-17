using System.Net;

namespace TestNinja.Mocking
{
    public interface IWebDownloader
    {
        void DownloadFile(string url, string path);
    }
    public class WebDownloader : IWebDownloader
    {
        public void DownloadFile(string url, string path)
        {
            using (var client = new WebClient())
                client.DownloadFile(url, path);
        }
    }
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private readonly IWebDownloader _webDownloader;

        public InstallerHelper(string setupDestinationFile, IWebDownloader webDownloader)
        {
            _setupDestinationFile = setupDestinationFile;
            _webDownloader = webDownloader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            var url = $"http://example.com/{customerName}/{installerName}";

            try
            {
                _webDownloader.DownloadFile(url, _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}