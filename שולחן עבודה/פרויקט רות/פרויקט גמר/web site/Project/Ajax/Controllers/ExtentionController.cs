using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ajax.Controllers
{
    public class ExtentionController : Controller
    {
        //
        // GET: /Extention/
        [HttpGet]
        public ActionResult Download()
        {
            return View();
        }
        public ActionResult Downloud()
        {
            return View();
        }


        //[HttpGet]
        //[HttpPost]
        public FileResult DownloadFiles()
        {
            //Define file Type
            string fileType = "application/octet-stream";

            //Define Output Memory Stream
            var outputStream = new MemoryStream();
            //  System.IO.Compression.ZipFile d;

            //Create object of ZipFile library
            using (ZipFile zipFile = new ZipFile())
            {
                //Add Root Directory Name "Files" or Any string name
                zipFile.AddDirectoryByName("Files");

                //Get all filepath from folder
                String[] files = Directory.GetFiles(Server.MapPath("/onTime"));
                foreach (string file in files)
                {
                    string filePath = file;

                    //Adding files from filepath into Zip
                    zipFile.AddFile(filePath, "OnTime");
                }

                Response.ClearContent();
                Response.ClearHeaders();

                //Set zip file name
                Response.AppendHeader("content-disposition", "attachment; filename=OnTime.zip");

                //Save the zip content in output stream
                zipFile.Save(outputStream);
            }

            //Set the cursor to start position
            outputStream.Position = 0;

            //Dispance the stream
            return new FileStreamResult(outputStream, fileType);
        }
    }
}
