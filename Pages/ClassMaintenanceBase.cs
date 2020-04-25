using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.AspNetCore.Components.Authorization;
using QuickTrackWeb.Services.ClassEntityDataService;
using QuickTrackWeb.Components.ClassMaintenance;
using QuickTrackWeb.Shared.Models;


namespace QuickTrackWeb.Pages
{
    public class ClassMaintenanceBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        protected string LoggedInUserName;

        [Inject] 
        public IClassEntityDataService _classEntityDataService {get; set;}

        public IEnumerable<ClassEntityWithoutChildrenDto> ClassEntities { get; set; } //= new List<ClassEntityWithoutChildrenDto>();
        public ClassEntityWithoutChildrenDto PickedClassEntity { get; set; } //= new ClassEntityDto { Id = -1, Name = "No Class Found", OwnerIdentityName = "unreal@test.com" };

        protected AddClassEntityDialog AddClassEntityDialog { get; set; }
        protected DeleteClassEntityDialog DeleteClassEntityDialog { get; set; }
        //protected StudentListDisplay StudentListDisplay { get; set; }


        protected override async Task OnInitializedAsync()
        {
            LoggedInUserName = (await authenticationStateTask).User.Identity.Name;
            PickedClassEntity = (await _classEntityDataService.GetClassEntityWithoutChildren(LoggedInUserName));
            
            if(PickedClassEntity == null)
            {
                ClassEntities = new List<ClassEntityWithoutChildrenDto>();
            }

            ClassEntities = (await _classEntityDataService.GetAllClassEntities()).ToList();

        }
        public async void Dialog_OnDialogClose()
        {
            //ORIGINAL CODE
            ////updates Employee List
            // Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            // StateHasChanged();


            PickedClassEntity = (await _classEntityDataService.GetClassEntityWithoutChildren(LoggedInUserName));

            StateHasChanged();
        }

        protected void AddClassEntity()
        {
            AddClassEntityDialog.Show(LoggedInUserName);
        }
        protected void DeleteClassEntity()
        {
            DeleteClassEntityDialog.Show(PickedClassEntity);
        }
    }
}
