using System.Linq;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace timney_photos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Random()
        {
            return View();
        }

        public ActionResult Album()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("web");

            var thumbnails = container.ListBlobs().Cast<CloudBlockBlob>().ToList();

            return View(thumbnails);
        }

        [Route("mila-4th-bday")]
        public ActionResult Mila()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("web");

            var thumbnails = container.ListBlobs("mila4", true).Cast<CloudBlockBlob>().ToList();

            return View("Mila-4th-bday", thumbnails);
        }

        public ActionResult PostAlbum(AlbumViewModel album)
        {
            return View();
        }
    }

    public class AlbumViewModel
    {
        public string AlbumName { get; set; }
        public string Pictures { get; set; }
    }
}