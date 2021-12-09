using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenDb.Models.TpcH;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RadzenDb.Models;

namespace RadzenDb.Pages
{
    public partial class NationComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected TpcHService TpcH { get; set; }
        protected RadzenDataGrid<RadzenDb.Models.TpcH.Nation> grid0;

        IEnumerable<RadzenDb.Models.TpcH.Nation> _getNationsResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Nation> getNationsResult
        {
            get
            {
                return _getNationsResult;
            }
            set
            {
                if (!object.Equals(_getNationsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getNationsResult", NewValue = value, OldValue = _getNationsResult };
                    _getNationsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var tpcHGetNationsResult = await TpcH.GetNations(new Query() { Expand = "Region" });
            getNationsResult = tpcHGetNationsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddNation>("Add Nation", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(RadzenDb.Models.TpcH.Nation args)
        {
            var dialogResult = await DialogService.OpenAsync<EditNation>("Edit Nation", new Dictionary<string, object>() { {"n_nationkey", args.n_nationkey} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var tpcHDeleteNationResult = await TpcH.DeleteNation(data.n_nationkey);
                    if (tpcHDeleteNationResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception tpcHDeleteNationException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Nation" });
            }
        }
    }
}
