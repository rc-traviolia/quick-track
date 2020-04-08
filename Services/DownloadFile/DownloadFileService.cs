using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace QuickTrackWeb.Services.DownloadFile
{
    public class DownloadFileService
    {

        public byte[] BuildZipFileBytes()
        {
            List<ZipItem> zipItems = new List<ZipItem>();

            zipItems.Add(new ZipItem("one.pdf", BuildSingleStudentReport("one.pdf")));
            zipItems.Add(new ZipItem("two.pdf", BuildSingleStudentReport("two.pdf")));
            zipItems.Add(new ZipItem("three.pdf", BuildSingleStudentReport("three.pdf")));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Zipper.Zip(zipItems).CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();
                return bytes;
            }



        }
        public byte[] BuildSingleStudentReport(string userName)
        {


            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document document = new Document();
                document.AddAuthor($"{userName}");
                document.AddCreator("QuickTrack - By: Richard Traviolia");
                document.AddKeywords("PDF tutorial education");
                document.AddSubject("Document subject - Describing the steps creating a PDF document");
                document.AddTitle("The document title - PDF creation using iTextSharp");


                PdfWriter myPDFWriter = PdfWriter.GetInstance(document, myMemoryStream);

                document.Open();

                document.Add(new Paragraph("Test1"));
                document.Add(new Paragraph("Test2"));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("Test1"));
                document.Add(new Paragraph("Test2"));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph("Test1"));
                document.Add(new Paragraph("Test2"));

                // Add to content to your PDF here...
                PdfPTable table = new PdfPTable(2);
                PdfPCell header = new PdfPCell(new Phrase("THIS IS A CELL HEADER"));

                header.Colspan = 2;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(header);
                table.AddCell("ID: none");
                document.Add(table);
                document.Close();

                byte[] content = myMemoryStream.ToArray();

                return content;

            }
        }
       
    }
}
