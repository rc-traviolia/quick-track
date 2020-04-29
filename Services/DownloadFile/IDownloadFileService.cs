using QuickTrackWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services.DownloadFile
{
    public interface IDownloadFileService
    {
        byte[] GetWeekOfReports(ClassEntityDto pickedClassEntity, WeekWithoutProgressDto pickedWeek, IEnumerable<ProgressRecordDto> reportsToSave);
        byte[] GetClassSummary(ClassEntityDto pickedClassEntity, WeekWithoutProgressDto pickedWeek, IEnumerable<ProgressRecordDto> allProgress);
    }
}
