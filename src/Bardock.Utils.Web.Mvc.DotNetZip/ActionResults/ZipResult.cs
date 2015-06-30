using System.IO;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.DotNetZip.ActionResults
{
    public class ZipResult : ActionResult
    {
        public string FileName { get; set; }

        public Entry[] Entries { get; set; }

        public ZipResult(string fileName, params Entry[] entries)
        {
            this.Entries = entries;
            this.FileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/zip";
            response.AppendHeader("content-disposition", "attachment; filename=" + FileName);

            using (var zipFile = new Ionic.Zip.ZipFile())
            {
                foreach (var e in this.Entries)
                {
                    zipFile.AddEntry(e.FileName, e.BytesStream);
                }
                zipFile.Save(response.OutputStream);
            }
        }

        public class Entry
        {
            public string FileName { get; set; }

            public Stream BytesStream { get; set; }

            public Entry(string fileName, Stream bytesStream)
            {
                this.FileName = fileName;
                this.BytesStream = bytesStream;
            }

            public Entry(string fileName, byte[] bytes)
                : this(fileName, new MemoryStream(bytes))
            { }
        }
    }
}