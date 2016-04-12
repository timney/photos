using System.Drawing;
using System.IO;
using ImageProcessor;
using ImageProcessor.Imaging;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Blob;
using MimeTypes;

namespace timney_photos_job
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ResizeImageForThumbnail(
            [BlobTrigger("original/{name}")] Stream input,
            string name,
            [Blob("thumbnail/{name}")] CloudBlockBlob output)
        {
            var size = new Size(250, 250);
            var resizeLayer = new ResizeLayer(size, ResizeMode.Crop, AnchorPosition.Center, true);

            using (var stream = output.OpenWrite())
            using (ImageFactory imageFactory = new ImageFactory())
            {
                var mime = MimeTypeMap.GetMimeType(Path.GetExtension(name));
                output.Properties.ContentType = mime;
                imageFactory.Load(input)
                    .Resize(resizeLayer)
                    .Quality(80)
                    .Save(stream);
            }
        }

        public static void ResizeImageForWeb([
            BlobTrigger("original/{name}")] Stream input,
            string name,
            [Blob("web/{name}")] CloudBlockBlob output)
        {
            var size = new Size(900, 900);
            var resizeLayer = new ResizeLayer(size, ResizeMode.Max, AnchorPosition.Center, true);

            using (var stream = output.OpenWrite())
            using (ImageFactory imageFactory = new ImageFactory())
            {
                var mime = MimeTypeMap.GetMimeType(Path.GetExtension(name));
                output.Properties.ContentType = mime;
                imageFactory.Load(input)
                    .Resize(resizeLayer)
                    .Quality(80)
                    .Save(stream);
            }
        }
    }
}
