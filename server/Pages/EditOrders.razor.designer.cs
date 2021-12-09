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
    public partial class EditOrdersComponent : ComponentBase
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

        [Parameter]
        public dynamic o_orderkey { get; set; }

        RadzenDb.Models.TpcH.Order _order;
        protected RadzenDb.Models.TpcH.Order order
        {
            get
            {
                return _order;
            }
            set
            {
                if (!object.Equals(_order, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "order", NewValue = value, OldValue = _order };
                    _order = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }
        IEnumerable<RadzenDb.Models.TpcH.Customer> _getCustomersForo_custkeyResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Customer> getCustomersForo_custkeyResult
        {
            get
            {
                return _getCustomersForo_custkeyResult;
            }
            set
            {
                if (!object.Equals(_getCustomersForo_custkeyResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCustomersForo_custkeyResult", NewValue = value, OldValue = _getCustomersForo_custkeyResult };
                    _getCustomersForo_custkeyResult = value;
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
            var tpcHGetOrderByoOrderkeyResult = await TpcH.GetOrderByoOrderkey(o_orderkey);
            order = tpcHGetOrderByoOrderkeyResult;

            var tpcHGetCustomersResult = await TpcH.GetCustomers();
            getCustomersForo_custkeyResult = tpcHGetCustomersResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(RadzenDb.Models.TpcH.Order args)
        {
            try
            {
                var tpcHUpdateOrderResult = await TpcH.UpdateOrder(o_orderkey, order);
                DialogService.Close(order);
            }
            catch (System.Exception tpcHUpdateOrderException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Order" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
