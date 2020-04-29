using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QuickTrackWeb.Shared.Models;

namespace QuickTrackWeb.Services.DownloadFile
{
    public class DownloadFileService :IDownloadFileService
    {
        public byte[] BuildZipFileBytes()
        {
            List<ZipItem> zipItems = new List<ZipItem>();

            //zipItems.Add(new ZipItem("one.pdf", BuildSingleStudentReport("one.pdf")));
            //zipItems.Add(new ZipItem("two.pdf", BuildSingleStudentReport("two.pdf")));
            //zipItems.Add(new ZipItem("three.pdf", BuildSingleStudentReport("three.pdf")));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Zipper.Zip(zipItems).CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();
                return bytes;
            }



        }
        public byte[] GetWeekOfReports(ClassEntityDto pickedClassEntity, WeekWithoutProgressDto pickedWeek, IEnumerable<ProgressRecordDto> reportsToSave)
        {
            List<ZipItem> zipItems = new List<ZipItem>();

            List<ProgressRecordDto> reportsToSaveWithNullProgress = AddNullProgress(pickedClassEntity, pickedWeek, reportsToSave);

            //organize the data we need to add the report items (see the .OrderBy .ThenBy (.ThenByDescending?)
            var joinedRecords =
               from progressRecord in reportsToSaveWithNullProgress
               join trackedItem in pickedClassEntity.TrackedItems on progressRecord.TrackedItemId equals trackedItem.Id
               select new { 
                   Progress = progressRecord.Progress,
                   StudentId = progressRecord.StudentId,
                   TrackedItemName = trackedItem.Name,
                   TrackedItemUnitOfMeasure = trackedItem.UnitOfMeasure
               };

            foreach (var student in pickedClassEntity.Students)
            {
                List<string> linesToWrite = new List<string>();
                var test = joinedRecords.Where(r => r.StudentId == student.Id).OrderBy(x => x.TrackedItemUnitOfMeasure).ThenBy(x => x.TrackedItemName);
 
                foreach (var item in test)
                {
                    linesToWrite.Add($"{item.TrackedItemName} ({item.TrackedItemUnitOfMeasure}): {item.Progress}");
                }

                //add report to the list of docs
                zipItems.Add(new ZipItem($"{student.Name} - Week {pickedWeek.Number}.pdf", BuildSingleStudentReport(pickedClassEntity, pickedWeek, student.Name, linesToWrite)));
            }

            zipItems.Add(new ZipItem($"CLASS_{pickedClassEntity.Name} - Week {pickedWeek.Number}.pdf", BuildClassReport(pickedClassEntity, pickedWeek, reportsToSaveWithNullProgress)));
            //zipItems.Add(new ZipItem("one.pdf", BuildSingleStudentReport("one.pdf")));
            //zipItems.Add(new ZipItem("two.pdf", BuildSingleStudentReport("two.pdf")));
            //zipItems.Add(new ZipItem("three.pdf", BuildSingleStudentReport("three.pdf")));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Zipper.Zip(zipItems).CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();
                return bytes;
            }



        }
        public byte[] GetClassSummary(ClassEntityDto pickedClassEntity, WeekWithoutProgressDto pickedWeek, IEnumerable<ProgressRecordDto> allProgress)
        {
            List<ProgressRecordDto> reportsToSaveWithNullProgress = AddNullProgress(pickedClassEntity, pickedWeek, allProgress);

            var headerFont = FontFactory.GetFont("Helvetica", 16);
            var largerFont = FontFactory.GetFont("Helvetica", 24);
            var reportFont = FontFactory.GetFont("Helvetica", 8); //this should be the default anyway

            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document document = new Document();
                document.AddAuthor($"{pickedClassEntity.OwnerIdentityName}");
                document.AddCreator("QuickTrack - By: Richard Traviolia");
                //document.AddKeywords("PDF tutorial education");
                document.AddSubject($"Weekly Report for {pickedClassEntity.Name}");

                var earliestDate = pickedClassEntity.Weeks.OrderBy(w => w.Number).First().MondayDate.ToString("MM/dd/yyyy");
                var latestDate = pickedClassEntity.Weeks.OrderByDescending(w => w.Number).First().MondayDate.AddDays(
                    pickedClassEntity.Weeks.OrderByDescending(w => w.Number).First().DayCount - 1)
                    .ToString("MM/dd/yyyy");
                document.AddTitle($"{pickedClassEntity.Name} - {earliestDate} to {latestDate}");


                PdfWriter myPDFWriter = PdfWriter.GetInstance(document, myMemoryStream);

                document.Open();

                //Add paragraph
                document.Add(new Paragraph($"{pickedClassEntity.Name} - {earliestDate} to {latestDate}", headerFont));
                document.Add(new Paragraph($"CLASS SUMMARY", largerFont));
                document.Add(new Paragraph(" ")); //spacer

                //Table
                PdfPTable table = new PdfPTable(pickedClassEntity.TrackedItems.Count() + 1);
                table.WidthPercentage = 100;
                table.HorizontalAlignment = 1;

                PdfPCell header = new PdfPCell(new Phrase("Student"));
                //header.Colspan = 2;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(header);

                foreach (var trackedItem in pickedClassEntity.TrackedItems)
                {
                    PdfPCell newHeader = new PdfPCell(new Phrase($"{trackedItem.Name}\n({trackedItem.UnitOfMeasure})"));
                    newHeader.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(newHeader);
                }

                foreach (var student in pickedClassEntity.Students)
                {
                    table.AddCell(student.Name);
                    foreach (var trackedItem in pickedClassEntity.TrackedItems)
                    {
                        var allRelevantProgress = reportsToSaveWithNullProgress.Where(r => r.StudentId == student.Id && r.TrackedItemId == trackedItem.Id);
                        var totalProgress = allRelevantProgress.Sum(p => p.Progress);
                        PdfPCell newCell = new PdfPCell(new Phrase($"{totalProgress}"));
                        newCell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table.AddCell(newCell);
                        //table.AddCell(progress.ToString());
                    }
                }


                document.Add(table);

                document.Close();

                byte[] content = myMemoryStream.ToArray();

                return content;
            }
        }

        private byte[] BuildSingleStudentReport(ClassEntityDto pickedClassEntity, WeekWithoutProgressDto pickedWeek, string studentName, List<string> reportRecords)
        {
            var headerFont = FontFactory.GetFont("Helvetica", 16);
            var studentFont = FontFactory.GetFont("Helvetica", 24);
            var reportFont = FontFactory.GetFont("Helvetica", 8); //this should be the default anyway

            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document document = new Document();
                document.AddAuthor($"{pickedClassEntity.OwnerIdentityName}");
                document.AddCreator("QuickTrack - By: Richard Traviolia");
                //document.AddKeywords("PDF tutorial education");
                document.AddSubject($"Weekly Report for {pickedClassEntity.Name}");
                document.AddTitle($"{studentName} - Week {pickedWeek.Number}");


                PdfWriter myPDFWriter = PdfWriter.GetInstance(document, myMemoryStream);

                document.Open();

                //Add paragraph
                document.Add(new Paragraph($"{pickedClassEntity.Name} - Week {pickedWeek.Number}: {pickedWeek.MondayDate.ToString("MM/dd/yyyy")}  -  {pickedWeek.MondayDate.AddDays(pickedWeek.DayCount - 1).ToString("MM/dd/yyyy")}", headerFont));
                document.Add(new Paragraph($"{studentName}", studentFont));
                document.Add(new Paragraph(" ")); //spacer

                Paragraph newParagraph = new Paragraph();

                bool firstLine = true;
                foreach(var line in reportRecords)
                {
                    if (firstLine)
                    {
                        newParagraph.Add($"{line}");
                        firstLine = false;
                    }
                    else
                    {
                        newParagraph.Add($"\n{line}");
                    }
                }
                document.Add(newParagraph);

                //document.Add(new Paragraph("Test1"));
                //document.Add(new Paragraph("Test2"));
                //document.Add(new Paragraph(""));
                //document.Add(new Paragraph("Test1"));
                //document.Add(new Paragraph("Test2"));
                //document.Add(new Paragraph(""));
                //document.Add(new Paragraph(""));
                //document.Add(new Paragraph("Test1"));
                //document.Add(new Paragraph("Test2"));

                // Add to content to your PDF here...
                //PdfPTable table = new PdfPTable(2);
                //PdfPCell header = new PdfPCell(new Phrase("THIS IS A CELL HEADER"));

                //header.Colspan = 2;
                //header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                //table.AddCell(header);
                //table.AddCell("ID: none");
                //document.Add(table);

                document.Close();

                byte[] content = myMemoryStream.ToArray();

                return content;

            }
        }

        private byte[] BuildClassReport(ClassEntityDto pickedClassEntity, WeekWithoutProgressDto pickedWeek, List<ProgressRecordDto> reportsToSaveWithNullProgress)
        {
            var headerFont = FontFactory.GetFont("Helvetica", 16);
            var largerFont = FontFactory.GetFont("Helvetica", 24);
            var reportFont = FontFactory.GetFont("Helvetica", 8); //this should be the default anyway

            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document document = new Document();
                document.AddAuthor($"{pickedClassEntity.OwnerIdentityName}");
                document.AddCreator("QuickTrack - By: Richard Traviolia");
                //document.AddKeywords("PDF tutorial education");
                document.AddSubject($"Weekly Report for {pickedClassEntity.Name}");
                document.AddTitle($"{pickedClassEntity.Name} - Week {pickedWeek.Number}");


                PdfWriter myPDFWriter = PdfWriter.GetInstance(document, myMemoryStream);

                document.Open();

                //Add paragraph
                document.Add(new Paragraph($"{pickedClassEntity.Name} - Week {pickedWeek.Number}: {pickedWeek.MondayDate.ToString("MM/dd/yyyy")}  -  {pickedWeek.MondayDate.AddDays(pickedWeek.DayCount - 1).ToString("MM/dd/yyyy")}", headerFont));
                document.Add(new Paragraph($"CLASS REPORT", largerFont));
                document.Add(new Paragraph(" ")); //spacer

                //Table
                PdfPTable table = new PdfPTable(pickedClassEntity.TrackedItems.Count() + 1);
                table.WidthPercentage = 100;
                table.HorizontalAlignment = 1;

                PdfPCell header = new PdfPCell(new Phrase("Student"));
                //header.Colspan = 2;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(header);

                foreach (var trackedItem in pickedClassEntity.TrackedItems)
                {
                    PdfPCell newHeader = new PdfPCell(new Phrase($"{trackedItem.Name}\n({trackedItem.UnitOfMeasure})"));
                    newHeader.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(newHeader);
                }
                
                foreach(var student in pickedClassEntity.Students)
                {
                    table.AddCell(student.Name);
                    foreach(var trackedItem in pickedClassEntity.TrackedItems)
                    {
                        var progress = reportsToSaveWithNullProgress.Where(r => r.StudentId == student.Id && r.TrackedItemId == trackedItem.Id).First().Progress;
                        PdfPCell newCell = new PdfPCell(new Phrase($"{progress}"));
                        newCell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        table.AddCell(newCell);
                        //table.AddCell(progress.ToString());
                    }
                }
                
                
                document.Add(table);

                document.Close();

                byte[] content = myMemoryStream.ToArray();

                return content;
            }
        }
      
        private List<ProgressRecordDto> AddNullProgress(ClassEntityDto pickedClassEntity, WeekWithoutProgressDto pickedWeek, IEnumerable<ProgressRecordDto> reportsWithoutNullProgress)
        {
            List<ProgressRecordDto> reportsWithNullProgress = reportsWithoutNullProgress.ToList();
            foreach (var student in pickedClassEntity.Students)
            {
                foreach (var trackedItem in pickedClassEntity.TrackedItems)
                {
                    if (!reportsWithNullProgress.Any(r => r.StudentId == student.Id && r.TrackedItemId == trackedItem.Id))
                    {
                        reportsWithNullProgress.Add(ProgressRecordDto.GetNullProgressRecord(pickedClassEntity.Id, student.Id, trackedItem.Id, pickedWeek.Id));
                    }
                }
            }
            return reportsWithNullProgress;
        }

    }
}
