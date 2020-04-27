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


namespace QuickTrackWeb.Pages
{
    public class TrackProgressBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        protected string LoggedInUserName;

        [Inject]
        public IClassEntityDataService _classEntityDataService { get; set; }
        public ClassEntityDto PickedClassEntity { get; set; }

        public WeekWithoutProgressDto PickedWeek { get; set; } = WeekWithoutProgressDto.GetNullWeek();




        protected override async Task OnInitializedAsync()
        {

            LoggedInUserName = (await authenticationStateTask).User.Identity.Name;
            PickedClassEntity = (await _classEntityDataService.GetClassEntity(LoggedInUserName));
            StateHasChanged();

        }
        public async void Dialog_OnDialogClose()
        {
            //ORIGINAL CODE
            ////updates Employee List
            // Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            // StateHasChanged();


            PickedClassEntity = (await _classEntityDataService.GetClassEntity(LoggedInUserName));

            StateHasChanged();
        }

        public async void WeekChanged_Callback(WeekWithoutProgressDto pickedWeek)
        {
            PickedWeek = pickedWeek;
            StateHasChanged();
        }
        protected void AddClassEntity()
        {
           // AddClassEntityDialog.Show(LoggedInUserName);
        }
        protected void DeleteClassEntity()
        {
           // DeleteClassEntityDialog.Show(PickedClassEntity);
        }
    }
}