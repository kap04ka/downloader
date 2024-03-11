using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using Ionic.Zip;
using System.Collections.Generic;
using System.Linq;

namespace downloader
{
    public class Downloader
    {
        private List<ILink> downloadList = new List<ILink>();

        private string folder = Directory.GetCurrentDirectory();
        private int countLinks = 0;
        private string subPath = "downloads";
        private string fullPath;
        private int currentPriorityLinks = 0;

        string speed;

        public Downloader(ObservableCollection<ILink> linksForDownload)
        {
            downloadList = linksForDownload.OrderByDescending(l => l.priority).ToList(); // Отсортированный список по убыванию приоритета
            currentPriorityLinks = downloadList[0].priority;

            foreach (ILink links in downloadList)
            {
                links.progress = 0.0f;
            }

            if (!Directory.Exists($"{folder}\\{subPath}"))
            {
                Directory.CreateDirectory($"{folder}\\{subPath}");
            }

            fullPath = folder + $"\\{subPath}";

            DownloadByURI(currentPriorityLinks);
        }

        public void DownloadByURI(int currentPriority)
        {
            foreach (ILink links in downloadList)
            {
                if(links.priority == currentPriority) {

                    countLinks++;
                    string fileNameResult = fullPath + $"\\{Path.GetFileName(links.link)}";

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadProgressChanged += (s, e) => links.progress = e.ProgressPercentage;
                        client.DownloadProgressChanged += (s, e) => links.speedCalculate(e.BytesReceived);

                        client.DownloadFileCompleted += (s, e) =>
                        {
                            countLinks--;
                            ZipCheck(fileNameResult);
                            CheckEnd();
                        };

                        client.DownloadFileAsync(
                            new System.Uri(links.link),
                            fileNameResult
                        );
                    }
                }

            }
            CheckEnd();
        }

        private void ZipCheck(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (!Directory.Exists(Path.Combine(fullPath, Path.GetFileNameWithoutExtension(fileName))) && extension == ".zip")
            {
                using (ZipFile zip = ZipFile.Read(fileName))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path.Combine(fullPath, Path.GetFileNameWithoutExtension(fileName)));
                    zip.ExtractAll(di.FullName);
                }
                File.Delete(fileName);

            }
        }

        private void CheckEnd()
        {
            if (countLinks == 0) 
            {
                if (currentPriorityLinks != 0)
                    DownloadByURI(--currentPriorityLinks);
                else
                    MessageBox.Show("Загрузка завершена");
            }
        }
    }
}