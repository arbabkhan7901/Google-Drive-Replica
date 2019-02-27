using GD.Entities;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GoogleDrive.Controllers
{
    public class FileController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public int saveFile(int id)
        {
            int count = -1;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    foreach (var fileName in HttpContext.Current.Request.Files.AllKeys)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                        if (file != null)
                        {
                            FileDTO dto = new FileDTO();
                            dto.FileExt = Path.GetExtension(file.FileName);
                            dto.Name = file.FileName;
                            dto.UploadedOn = DateTime.Now;
                            dto.isActive = true;
                            dto.FileSizeInKB = file.ContentLength/1024;
                            dto.ParentFolderId = id;
                            dto.uniqueName = Guid.NewGuid().ToString();
                            dto.contentType = file.ContentType;
                            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                            var fileSavePath = System.IO.Path.Combine(rootPath, dto.uniqueName + dto.FileExt);
                            file.SaveAs(fileSavePath);
                            count = GD.BAL.FileBO.saveFile(dto);

                            var thumbPath = HttpContext.Current.Server.MapPath("~/Thumbnails");
                            ShellFile shellFile = ShellFile.FromFilePath(fileSavePath);
                            Bitmap thumb = shellFile.Thumbnail.ExtraLargeBitmap;
                            var thumbSavePath = System.IO.Path.Combine(thumbPath, dto.uniqueName + ".jpg");
                            thumb.Save(thumbSavePath);                   
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return count;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]

        public Object downloadFile(String uniqueName)
        {
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            var fileDTO = GD.BAL.FileBO.getFileByName(uniqueName);
                if (fileDTO != null)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileFullPath = System.IO.Path.Combine(rootPath, fileDTO.uniqueName + fileDTO.FileExt);

                    byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

                    response.Content = new ByteArrayContent(file);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(fileDTO.contentType);
                    response.Content.Headers.ContentDisposition.FileName = fileDTO.Name;
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }
            
        }

        [HttpGet]
        public int deleteFile(int id)
        {
            int count = GD.BAL.FileBO.deleteFile(id);
            return count;
        }

        [HttpGet]
        public List<FileDTO> searchAll(String name)
        {
            List<FileDTO> list = new List<FileDTO>();
            list = GD.BAL.FileBO.serachAll(name);
            return list;
        }
    }
}