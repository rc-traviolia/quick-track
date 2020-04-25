using Microsoft.AspNetCore.Components;
using QuickTrackWeb.Services;
using QuickTrackWeb.Services.ClassEntityDataService;
using QuickTrackWeb.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace QuickTrackWeb.Components.ClassMaintenance
{
    public class DeleteClassEntityDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IClassEntityDataService _classEntityDataService { get; set; }

        public ClassEntityWithoutChildrenDto ClassEntity { get; set; }

        protected StringMatcher StringMatcher { get; set; }


        public void Show(ClassEntityWithoutChildrenDto classEntityToDelete)
        {
            StringMatcher = new StringMatcher(classEntityToDelete.Name);
            ResetDialog(classEntityToDelete);
            ShowDialog = true;
            StateHasChanged();
        }

        private void ResetDialog(ClassEntityWithoutChildrenDto classEntityToDelete)
        {
            ClassEntity = classEntityToDelete;// _sessionDataService.LoggedInUserName };
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            await _classEntityDataService.DeleteClassEntity(ClassEntity.OwnerIdentityName);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }


    }


}

