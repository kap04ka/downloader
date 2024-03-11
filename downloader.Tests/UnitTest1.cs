using System.Collections.ObjectModel;

namespace downloader.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void positive_test_empty_url()
        {
            Assert.Throws<UriFormatException>(() =>
            {
                ObservableCollection<ILink> links = new ObservableCollection<ILink>();
                links.Add(new LinkForTest("", 1));

                Downloader downloader = new Downloader(links);
            });
        }

        [Fact]
        public void positive_test_zero_objects()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                ObservableCollection<ILink> links = new ObservableCollection<ILink>();

                Downloader downloader = new Downloader(links);
            });
        }

        [Fact]
        public void positive_successful_download()
        {
            ObservableCollection<ILink> links = new ObservableCollection<ILink>();
            links.Add(new LinkForTest("https://upload.wikimedia.org/wikipedia/commons/a/aa/SmallFullColourGIF.gif", 3));

            Downloader downloader = new Downloader(links);

            bool fileExist = false;

            string folder = Directory.GetCurrentDirectory();
            string subPath = "downloads";

            string fullPath = folder + $"\\{subPath}" + $"\\SmallFullColourGIF.gif" ;

            if (File.Exists(fullPath)) fileExist = true;

            Assert.True(fileExist);

        }
    }
}