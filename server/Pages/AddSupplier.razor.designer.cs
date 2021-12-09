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
    public partial class AddSupplierComponent : ComponentBase
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

        IEnumerable<RadzenDb.Models.TpcH.Nation> _getNationsFors_nationkeyResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Nation> getNationsFors_nationkeyResult
        {
            get
            {
                return _getNationsFors_nationkeyResult;
            }
            set
            {
                if (!object.Equals(_getNationsFors_nationkeyResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getNationsFors_nationkeyResult", NewValue = value, OldValue = _getNationsFors_nationkeyResult };
                    _getNationsFors_nationkeyResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        RadzenDb.Models.TpcH.Supplier _supplier;
        protected RadzenDb.Models.TpcH.Supplier supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                if (!object.Equals(_supplier, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "supplier", NewValue = value, OldValue = _supplier };
                    _supplier = value;
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
            var tpcHGetNationsResult = await TpcH.GetNations();
            getNationsFors_nationkeyResult = tpcHGetNationsResult;

            supplier = new RadzenDb.Models.TpcH.Supplier(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(RadzenDb.Models.TpcH.Supplier args)
        {
            try
            {
                var tpcHCreateSupplierResult = await TpcH.CreateSupplier(supplier);
                DialogService.Close(supplier);
            }
            catch (System.Exception tpcHCreateSupplierException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Supplier!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
