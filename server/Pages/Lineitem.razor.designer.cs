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
    public partial class LineitemComponent : ComponentBase
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
        protected RadzenDataGrid<RadzenDb.Models.TpcH.Lineitem> grid0;

        IEnumerable<RadzenDb.Models.TpcH.Lineitem> _getLineitemsResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Lineitem> getLineitemsResult
        {
            get
            {
                return _getLineitemsResult;
            }
            set
            {
                if (!object.Equals(_getLineitemsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLineitemsResult", NewValue = value, OldValue = _getLineitemsResult };
                    _getLineitemsResult = value;
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
            var tpcHGetLineitemsResult = await TpcH.GetLineitems(new Query() { Expand = "Order,Partsupp" });
            getLineitemsResult = tpcHGetLineitemsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddLineitem>("Add Lineitem", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(RadzenDb.Models.TpcH.Lineitem args)
        {
            var dialogResult = await DialogService.OpenAsync<EditLineitem>("Edit Lineitem", new Dictionary<string, object>() { {"l_orderkey", args.l_orderkey}, {"l_linenumber", args.l_linenumber} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var tpcHDeleteLineitemResult = await TpcH.DeleteLineitem(data.l_orderkey, data.l_linenumber);
                    if (tpcHDeleteLineitemResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception tpcHDeleteLineitemException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Lineitem" });
            }
        }
    }
}
