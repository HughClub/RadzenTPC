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
    public partial class AddNationComponent : ComponentBase
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

        IEnumerable<RadzenDb.Models.TpcH.Region> _getRegionsForn_regionkeyResult;
        protected IEnumerable<RadzenDb.Models.TpcH.Region> getRegionsForn_regionkeyResult
        {
            get
            {
                return _getRegionsForn_regionkeyResult;
            }
            set
            {
                if (!object.Equals(_getRegionsForn_regionkeyResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getRegionsForn_regionkeyResult", NewValue = value, OldValue = _getRegionsForn_regionkeyResult };
                    _getRegionsForn_regionkeyResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        RadzenDb.Models.TpcH.Nation _nation;
        protected RadzenDb.Models.TpcH.Nation nation
        {
            get
            {
                return _nation;
            }
            set
            {
                if (!object.Equals(_nation, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "nation", NewValue = value, OldValue = _nation };
                    _nation = value;
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
            var tpcHGetRegionsResult = await TpcH.GetRegions();
            getRegionsForn_regionkeyResult = tpcHGetRegionsResult;

            nation = new RadzenDb.Models.TpcH.Nation(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(RadzenDb.Models.TpcH.Nation args)
        {
            try
            {
                var tpcHCreateNationResult = await TpcH.CreateNation(nation);
                DialogService.Close(nation);
            }
            catch (System.Exception tpcHCreateNationException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Nation!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
