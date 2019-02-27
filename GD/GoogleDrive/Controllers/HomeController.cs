using GD.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GoogleDrive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult HomeScreen()
        {
            List<FolderDTO> list = GD.BAL.FolderBO.getAllFolders();
            return View(list);
        }

        public ActionResult OpenFolder(int id = 1)
        {
            Session["id"] = id;
            String name = GD.BAL.FolderBO.getFolderName(id);
            Session["name"] = name;
            List<MixDTO> list = new List<MixDTO>();
            MixDTO dto = new MixDTO();
            List<FolderDTO> folder = GD.BAL.FolderBO.getFolderById(id);
            dto.folder = folder;
            dto.folderSize = folder.Count();
            List<FileDTO> file = GD.BAL.FileBO.getFileById(id);
            dto.file = file;
            dto.fileSize = file.Count();
            dto.size = 0;
            List<FolderDTO> dto1 = GD.BAL.FolderBO.navFlow(id);
            MixDTO dto3 = new MixDTO();
            List<FileDTO> dto2 = new List<FileDTO>();
            dto3.folder = dto1;
            dto3.folderSize = dto1.Count();
            dto3.file = dto2;
            dto3.size = 100;
            list.Add(dto3);
            list.Add(dto);
            int count = list.Count();
            return View(list);
        }

        public void downloadMeta(int id)
        {    
            List<FolderDTO> folder = GD.BAL.FolderBO.getFolderMeta(id);
            List<FileDTO> file = GD.BAL.FileBO.getFileMeta(folder);
            String[] arr = GD.BAL.FolderBO.getFolderParentName(folder);
            String[] arr1 = GD.BAL.FileBO.getFileParentName(file);
            var appPhysicalPath = Server.MapPath("~/docs");
            var fileName = DateTime.Now.Ticks.ToString() + ".pdf";
            var filePath = appPhysicalPath + "\\" + fileName;
            var doc1 = new Document();
            var streamObj = new System.IO.FileStream(filePath, System.IO.FileMode.CreateNew);
            PdfWriter.GetInstance(doc1, streamObj);
            doc1.Open();

            doc1.Add(new Paragraph("Meta data of the folder"));
            Chunk lineBreak = new Chunk(new LineSeparator());
            doc1.Add(lineBreak);

            for (int i = 0; i < folder.Count(); i++)
            {
                PdfPTable table = new PdfPTable(2);
                Phrase ph = new Phrase("Meta Data", new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.BOLD, BaseColor.BLACK));

                PdfPCell cell = new PdfPCell(ph);
                cell.Colspan = 2;
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;

                table.AddCell(cell);

                table.AddCell(new PdfPCell(new Phrase("Name", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell(folder[i].Name);

                table.AddCell(new PdfPCell(new Phrase("Type", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell("Folder");

                table.AddCell(new PdfPCell(new Phrase("Size", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell("None");

                table.AddCell(new PdfPCell(new Phrase("Parent", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell(arr[i]);

                table.SpacingBefore = 10f;
                table.SpacingAfter = 12.5f;

                doc1.Add(table);
            }

            doc1.Add(new Paragraph("Meta data of the Files"));
            doc1.Add(lineBreak);

            for (int i = 0; i < file.Count(); i++)
            {
                PdfPTable table = new PdfPTable(2);
                Phrase ph = new Phrase("Meta Data", new Font(Font.FontFamily.TIMES_ROMAN, 14f, Font.BOLD, BaseColor.BLACK));

                PdfPCell cell = new PdfPCell(ph);
                cell.Colspan = 2;
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;

                table.AddCell(cell);
              
                table.AddCell(new PdfPCell(new Phrase("Name", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell(file[i].Name);

                table.AddCell(new PdfPCell(new Phrase("Type", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell("File");

                table.AddCell(new PdfPCell(new Phrase("Size", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell(file[i].FileSizeInKB.ToString());

                table.AddCell(new PdfPCell(new Phrase("Parent", new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.BOLD))));
                table.AddCell(arr1[i]);

                table.SpacingBefore = 10f;
                table.SpacingAfter = 12.5f;

                doc1.Add(table);
            }
            doc1.Close();

            string strURL = "~/docs" + "/" + fileName;
            WebClient req = new WebClient();
            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
            byte[] data = req.DownloadData(Server.MapPath(strURL));
            response.BinaryWrite(data);
            response.End();
        }

        public ActionResult ShowAll(String name)
        {
            List<FileDTO> list = new List<FileDTO>();
            list = GD.BAL.FileBO.serachAll(name);
            return View(list);
        }
    }
}
