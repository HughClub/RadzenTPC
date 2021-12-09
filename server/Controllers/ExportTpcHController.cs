using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadzenDb.Data;

namespace RadzenDb
{
    public partial class ExportTpcHController : ExportController
    {
        private readonly TpcHContext context;

        public ExportTpcHController(TpcHContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/TpcH/customers/csv")]
        [HttpGet("/export/TpcH/customers/csv(fileName='{fileName}')")]
        public FileStreamResult ExportCustomersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Customers, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/customers/excel")]
        [HttpGet("/export/TpcH/customers/excel(fileName='{fileName}')")]
        public FileStreamResult ExportCustomersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Customers, Request.Query), fileName);
        }
        [HttpGet("/export/TpcH/lineitems/csv")]
        [HttpGet("/export/TpcH/lineitems/csv(fileName='{fileName}')")]
        public FileStreamResult ExportLineitemsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Lineitems, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/lineitems/excel")]
        [HttpGet("/export/TpcH/lineitems/excel(fileName='{fileName}')")]
        public FileStreamResult ExportLineitemsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Lineitems, Request.Query), fileName);
        }
        [HttpGet("/export/TpcH/nations/csv")]
        [HttpGet("/export/TpcH/nations/csv(fileName='{fileName}')")]
        public FileStreamResult ExportNationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Nations, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/nations/excel")]
        [HttpGet("/export/TpcH/nations/excel(fileName='{fileName}')")]
        public FileStreamResult ExportNationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Nations, Request.Query), fileName);
        }
        [HttpGet("/export/TpcH/orders/csv")]
        [HttpGet("/export/TpcH/orders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Orders, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/orders/excel")]
        [HttpGet("/export/TpcH/orders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Orders, Request.Query), fileName);
        }
        [HttpGet("/export/TpcH/parts/csv")]
        [HttpGet("/export/TpcH/parts/csv(fileName='{fileName}')")]
        public FileStreamResult ExportPartsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Parts, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/parts/excel")]
        [HttpGet("/export/TpcH/parts/excel(fileName='{fileName}')")]
        public FileStreamResult ExportPartsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Parts, Request.Query), fileName);
        }
        [HttpGet("/export/TpcH/partsupps/csv")]
        [HttpGet("/export/TpcH/partsupps/csv(fileName='{fileName}')")]
        public FileStreamResult ExportPartsuppsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Partsupps, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/partsupps/excel")]
        [HttpGet("/export/TpcH/partsupps/excel(fileName='{fileName}')")]
        public FileStreamResult ExportPartsuppsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Partsupps, Request.Query), fileName);
        }
        [HttpGet("/export/TpcH/regions/csv")]
        [HttpGet("/export/TpcH/regions/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRegionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Regions, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/regions/excel")]
        [HttpGet("/export/TpcH/regions/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRegionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Regions, Request.Query), fileName);
        }
        [HttpGet("/export/TpcH/suppliers/csv")]
        [HttpGet("/export/TpcH/suppliers/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSuppliersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Suppliers, Request.Query), fileName);
        }

        [HttpGet("/export/TpcH/suppliers/excel")]
        [HttpGet("/export/TpcH/suppliers/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSuppliersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Suppliers, Request.Query), fileName);
        }
    }
}
