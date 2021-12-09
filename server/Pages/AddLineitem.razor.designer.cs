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
    public partial class AddLineitemComponent : ComponentBase
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

        IEnumerable<RadzenDb.Models.TpcH.Order> _getOrdersForl_orderkeyResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Order> getOrdersForl_orderkeyResult
        {
            get
            {
                return _getOrdersForl_orderkeyResult;
            }
            set
            {
                if (!object.Equals(_getOrdersForl_orderkeyResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersForl_orderkeyResult", NewValue = value, OldValue = _getOrdersForl_orderkeyResult };
                    _getOrdersForl_orderkeyResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<RadzenDb.Models.TpcH.Partsupp> _getPartsuppsForl_partkeyResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Partsupp> getPartsuppsForl_partkeyResult
        {
            get
            {
                return _getPartsuppsForl_partkeyResult;
            }
            set
            {
                if (!object.Equals(_getPartsuppsForl_partkeyResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPartsuppsForl_partkeyResult", NewValue = value, OldValue = _getPartsuppsForl_partkeyResult };
                    _getPartsuppsForl_partkeyResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        RadzenDb.Models.TpcH.Lineitem _lineitem;
        protected RadzenDb.Models.TpcH.Lineitem lineitem
        {
            get
            {
                return _lineitem;
            }
            set
            {
                if (!object.Equals(_lineitem, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "lineitem", NewValue = value, OldValue = _lineitem };
                    _lineitem = value;
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
            var tpcHGetOrdersResult = await TpcH.GetOrders();
            getOrdersForl_orderkeyResult = tpcHGetOrdersResult;

            var tpcHGetPartsuppsResult = await TpcH.GetPartsupps();
            getPartsuppsForl_partkeyResult = tpcHGetPartsuppsResult;

            lineitem = new RadzenDb.Models.TpcH.Lineitem(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(RadzenDb.Models.TpcH.Lineitem args)
        {
            try
            {
                var tpcHCreateLineitemResult = await TpcH.CreateLineitem(lineitem);
                DialogService.Close(lineitem);
            }
            catch (System.Exception tpcHCreateLineitemException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Lineitem!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
