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
    public partial class AddPartsuppComponent : ComponentBase
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

        IEnumerable<RadzenDb.Models.TpcH.Part> _getPartsForps_partkeyResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Part> getPartsForps_partkeyResult
        {
            get
            {
                return _getPartsForps_partkeyResult;
            }
            set
            {
                if (!object.Equals(_getPartsForps_partkeyResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPartsForps_partkeyResult", NewValue = value, OldValue = _getPartsForps_partkeyResult };
                    _getPartsForps_partkeyResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<RadzenDb.Models.TpcH.Supplier> _getSuppliersForps_suppkeyResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Supplier> getSuppliersForps_suppkeyResult
        {
            get
            {
                return _getSuppliersForps_suppkeyResult;
            }
            set
            {
                if (!object.Equals(_getSuppliersForps_suppkeyResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSuppliersForps_suppkeyResult", NewValue = value, OldValue = _getSuppliersForps_suppkeyResult };
                    _getSuppliersForps_suppkeyResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        RadzenDb.Models.TpcH.Partsupp _partsupp;
        protected RadzenDb.Models.TpcH.Partsupp partsupp
        {
            get
            {
                return _partsupp;
            }
            set
            {
                if (!object.Equals(_partsupp, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "partsupp", NewValue = value, OldValue = _partsupp };
                    _partsupp = value;
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
            var tpcHGetPartsResult = await TpcH.GetParts();
            getPartsForps_partkeyResult = tpcHGetPartsResult;

            var tpcHGetSuppliersResult = await TpcH.GetSuppliers();
            getSuppliersForps_suppkeyResult = tpcHGetSuppliersResult;

            partsupp = new RadzenDb.Models.TpcH.Partsupp(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(RadzenDb.Models.TpcH.Partsupp args)
        {
            try
            {
                var tpcHCreatePartsuppResult = await TpcH.CreatePartsupp(partsupp);
                DialogService.Close(partsupp);
            }
            catch (System.Exception tpcHCreatePartsuppException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Partsupp!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
