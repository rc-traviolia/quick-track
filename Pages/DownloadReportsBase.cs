using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.AspNetCore.Components.Authorization;
using QuickTrackWeb.Services.ClassEntityDataService;
using QuickTrackWeb.Services.WeekDataService;
//using QuickTrackWeb.Services.ProgressRecordService;
using QuickTrackWeb.Components.TrackProgress;
using QuickTrackWeb.Shared.Models;
using QuickTrackWeb.Services.DownloadFile;
//using Microsoft.JSInterop;
using QuickTrackWeb.Services.ProgressRecordDataService;

namespace QuickTrackWeb.Pages
{
    public class DownloadReportsBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        protected string LoggedInUserName;

        [Inject]
        public IClassEntityDataService _classEntityDataService { get; set; }

        [Inject]
        public IProgressRecordDataService _progressRecordDataService { get; set; }

        [Inject]
        public IDownloadFileService _downloadFileService { get; set; }

        public ClassEntityDto PickedClassEntity { get; set; }
        public WeekWithoutProgressDto PickedWeek { get; set; } = WeekWithoutProgressDto.GetNullWeek();
        //public IEnumerable<ProgressRecordDto> WeekReportsForDownload { get; set; }
        //public IEnumerable<ProgressRecordDto> AllReportsForDownload { get; set; }




        protected override async Task OnInitializedAsync()
        {

            LoggedInUserName = (await authenticationStateTask).User.Identity.Name;
            PickedClassEntity = (await _classEntityDataService.GetClassEntity(LoggedInUserName));
            StateHasChanged();

        }
        public async void Dialog_OnDialogClose()
        {
            PickedClassEntity = (await _classEntityDataService.GetClassEntity(LoggedInUserName));
            StateHasChanged();
        }

        public async void WeekChanged_Callback(WeekWithoutProgressDto pickedWeek)
        {
            PickedWeek = pickedWeek;
            //WeekReportsForDownload = await _progressRecordDataService.GetProgressRecordsForClassEntityAndWeek(PickedClassEntity.OwnerIdentityName, pickedWeek.Id);
            StateHasChanged();
        }

        //MOVED to the file because I couldn't get the injection of the JSRuntime to work on the base class
        //public async void DownloadReports()
        //{
        //    var fileName = $"{PickedClassEntity.Name} Reports - Week {PickedWeek.Number}.zip";
        //    var byteContent = _downloadFileService.GetWeekOfReports(PickedClassEntity, PickedWeek, ReportsForDownload);
        //    await JSRuntime.InvokeAsync<object>(
        //        "Test",
        //        fileName,
        //        byteContent
        //    // noteContent
        //    );
        //}
    }
}