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

        public byte[] TestMethod()
        {
        List<ZipItem> zipItems = new List<ZipItem>();

        zipItems.Add(new ZipItem("one.pdf", GetPdfMemoryStream("one.pdf")));
        zipItems.Add(new ZipItem("two.pdf", GetPdfMemoryStream("two.pdf")));
        zipItems.Add(new ZipItem("three.pdf", GetPdfMemoryStream("three.pdf")));

        using (MemoryStream memoryStream = new MemoryStream())
        {
            Zipper.Zip(zipItems).CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();
            return bytes;
        }



    }
        public byte[] GetPdfMemoryStream(string userName)
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
        public byte[] WritePdfToDisk(string userName)
        {
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            //Document document = new Document();
            document.AddAuthor($"{userName}");
            document.AddCreator("QuickTrack - By: Richard Traviolia");
            document.AddKeywords("PDF tutorial education");
            document.AddSubject("Document subject - Describing the steps creating a PDF document");
            document.AddTitle("The document title - PDF creation using iTextSharp");

            byte[] pdfBytes;
            using (var mem = new MemoryStream())
            {
                using (PdfWriter wri = PdfWriter.GetInstance(document, mem))
                {
                    document.Open();//Open Document to write
                    Paragraph paragraph = new Paragraph("This is my first line using Paragraph.");
                    Phrase phrase = new Phrase("This is my second line using Pharse.");
                    Chunk chunk = new Chunk(" This is my third line using Chunk.");

                    document.Add(paragraph);

                    document.Add(phrase);

                    document.Add(chunk);
                }
                pdfBytes = mem.ToArray();
            }

            return pdfBytes;

        }
    }
}
