using Microsoft.AspNetCore.Components;
using QuickTrackWeb.Services.ClassEntityDataService;
using QuickTrackWeb.Shared.Models;
using System.Threading.Tasks;

namespace QuickTrackWeb.Components.ClassMaintenance
{
    public class AddClassEntityDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IClassEntityDataService _classEntityDataService { get; set; }

        public ClassEntityForCreationDto ClassEntity { get; set; }


        public void Show(string loggedInUserName)
        {
            ResetDialog(loggedInUserName);
            ShowDialog = true;
            StateHasChanged();
        }

        private void ResetDialog(string loggedInUserName)
        {
            ClassEntity = new ClassEntityForCreationDto { OwnerIdentityName = loggedInUserName };// _sessionDataService.LoggedInUserName };
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            await _classEntityDataService.AddClassEntity(ClassEntity);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}

