using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using RadzenDb.Data;

namespace RadzenDb
{
    public partial class TpcHService
    {
        TpcHContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly TpcHContext context;
        private readonly NavigationManager navigationManager;

        public TpcHService(TpcHContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportCustomersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCustomersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCustomersRead(ref IQueryable<Models.TpcH.Customer> items);

        public async Task<IQueryable<Models.TpcH.Customer>> GetCustomers(Query query = null)
        {
            var items = Context.Customers.AsQueryable();

            items = items.Include(i => i.Nation);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCustomersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCustomerCreated(Models.TpcH.Customer item);
        partial void OnAfterCustomerCreated(Models.TpcH.Customer item);

        public async Task<Models.TpcH.Customer> CreateCustomer(Models.TpcH.Customer customer)
        {
            OnCustomerCreated(customer);

            var existingItem = Context.Customers
                              .Where(i => i.c_custkey == customer.c_custkey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Customers.Add(customer);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(customer).State = EntityState.Detached;
                customer.Nation = null;
                throw;
            }

            OnAfterCustomerCreated(customer);

            return customer;
        }
        public async Task ExportLineitemsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/lineitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/lineitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLineitemsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/lineitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/lineitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLineitemsRead(ref IQueryable<Models.TpcH.Lineitem> items);

        public async Task<IQueryable<Models.TpcH.Lineitem>> GetLineitems(Query query = null)
        {
            var items = Context.Lineitems.AsQueryable();

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Partsupp);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLineitemsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLineitemCreated(Models.TpcH.Lineitem item);
        partial void OnAfterLineitemCreated(Models.TpcH.Lineitem item);

        public async Task<Models.TpcH.Lineitem> CreateLineitem(Models.TpcH.Lineitem lineitem)
        {
            OnLineitemCreated(lineitem);

            var existingItem = Context.Lineitems
                              .Where(i => i.l_orderkey == lineitem.l_orderkey && i.l_linenumber == lineitem.l_linenumber)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Lineitems.Add(lineitem);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(lineitem).State = EntityState.Detached;
                lineitem.Order = null;
                lineitem.Partsupp = null;
                throw;
            }

            OnAfterLineitemCreated(lineitem);

            return lineitem;
        }
        public async Task ExportNationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/nations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/nations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportNationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/nations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/nations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnNationsRead(ref IQueryable<Models.TpcH.Nation> items);

        public async Task<IQueryable<Models.TpcH.Nation>> GetNations(Query query = null)
        {
            var items = Context.Nations.AsQueryable();

            items = items.Include(i => i.Region);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnNationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnNationCreated(Models.TpcH.Nation item);
        partial void OnAfterNationCreated(Models.TpcH.Nation item);

        public async Task<Models.TpcH.Nation> CreateNation(Models.TpcH.Nation nation)
        {
            OnNationCreated(nation);

            var existingItem = Context.Nations
                              .Where(i => i.n_nationkey == nation.n_nationkey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Nations.Add(nation);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(nation).State = EntityState.Detached;
                nation.Region = null;
                throw;
            }

            OnAfterNationCreated(nation);

            return nation;
        }
        public async Task ExportOrdersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrdersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrdersRead(ref IQueryable<Models.TpcH.Order> items);

        public async Task<IQueryable<Models.TpcH.Order>> GetOrders(Query query = null)
        {
            var items = Context.Orders.AsQueryable();

            items = items.Include(i => i.Customer);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOrdersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderCreated(Models.TpcH.Order item);
        partial void OnAfterOrderCreated(Models.TpcH.Order item);

        public async Task<Models.TpcH.Order> CreateOrder(Models.TpcH.Order order)
        {
            OnOrderCreated(order);

            var existingItem = Context.Orders
                              .Where(i => i.o_orderkey == order.o_orderkey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(order).State = EntityState.Detached;
                order.Customer = null;
                throw;
            }

            OnAfterOrderCreated(order);

            return order;
        }
        public async Task ExportPartsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/parts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/parts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPartsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/parts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/parts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPartsRead(ref IQueryable<Models.TpcH.Part> items);

        public async Task<IQueryable<Models.TpcH.Part>> GetParts(Query query = null)
        {
            var items = Context.Parts.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPartsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPartCreated(Models.TpcH.Part item);
        partial void OnAfterPartCreated(Models.TpcH.Part item);

        public async Task<Models.TpcH.Part> CreatePart(Models.TpcH.Part part)
        {
            OnPartCreated(part);

            var existingItem = Context.Parts
                              .Where(i => i.p_partkey == part.p_partkey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Parts.Add(part);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(part).State = EntityState.Detached;
                throw;
            }

            OnAfterPartCreated(part);

            return part;
        }
        public async Task ExportPartsuppsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/partsupps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/partsupps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPartsuppsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/partsupps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/partsupps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPartsuppsRead(ref IQueryable<Models.TpcH.Partsupp> items);

        public async Task<IQueryable<Models.TpcH.Partsupp>> GetPartsupps(Query query = null)
        {
            var items = Context.Partsupps.AsQueryable();

            items = items.Include(i => i.Part);

            items = items.Include(i => i.Supplier);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnPartsuppsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPartsuppCreated(Models.TpcH.Partsupp item);
        partial void OnAfterPartsuppCreated(Models.TpcH.Partsupp item);

        public async Task<Models.TpcH.Partsupp> CreatePartsupp(Models.TpcH.Partsupp partsupp)
        {
            OnPartsuppCreated(partsupp);

            var existingItem = Context.Partsupps
                              .Where(i => i.ps_partkey == partsupp.ps_partkey && i.ps_suppkey == partsupp.ps_suppkey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Partsupps.Add(partsupp);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(partsupp).State = EntityState.Detached;
                partsupp.Part = null;
                partsupp.Supplier = null;
                throw;
            }

            OnAfterPartsuppCreated(partsupp);

            return partsupp;
        }
        public async Task ExportRegionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegionsRead(ref IQueryable<Models.TpcH.Region> items);

        public async Task<IQueryable<Models.TpcH.Region>> GetRegions(Query query = null)
        {
            var items = Context.Regions.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnRegionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegionCreated(Models.TpcH.Region item);
        partial void OnAfterRegionCreated(Models.TpcH.Region item);

        public async Task<Models.TpcH.Region> CreateRegion(Models.TpcH.Region region)
        {
            OnRegionCreated(region);

            var existingItem = Context.Regions
                              .Where(i => i.r_regionkey == region.r_regionkey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Regions.Add(region);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(region).State = EntityState.Detached;
                throw;
            }

            OnAfterRegionCreated(region);

            return region;
        }
        public async Task ExportSuppliersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/suppliers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/suppliers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSuppliersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/tpch/suppliers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/tpch/suppliers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSuppliersRead(ref IQueryable<Models.TpcH.Supplier> items);

        public async Task<IQueryable<Models.TpcH.Supplier>> GetSuppliers(Query query = null)
        {
            var items = Context.Suppliers.AsQueryable();

            items = items.Include(i => i.Nation);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSuppliersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSupplierCreated(Models.TpcH.Supplier item);
        partial void OnAfterSupplierCreated(Models.TpcH.Supplier item);

        public async Task<Models.TpcH.Supplier> CreateSupplier(Models.TpcH.Supplier supplier)
        {
            OnSupplierCreated(supplier);

            var existingItem = Context.Suppliers
                              .Where(i => i.s_suppkey == supplier.s_suppkey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Suppliers.Add(supplier);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(supplier).State = EntityState.Detached;
                supplier.Nation = null;
                throw;
            }

            OnAfterSupplierCreated(supplier);

            return supplier;
        }

        partial void OnCustomerDeleted(Models.TpcH.Customer item);
        partial void OnAfterCustomerDeleted(Models.TpcH.Customer item);

        public async Task<Models.TpcH.Customer> DeleteCustomer(int? cCustkey)
        {
            var itemToDelete = Context.Customers
                              .Where(i => i.c_custkey == cCustkey)
                              .Include(i => i.Orders)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCustomerDeleted(itemToDelete);

            Context.Customers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCustomerDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCustomerGet(Models.TpcH.Customer item);

        public async Task<Models.TpcH.Customer> GetCustomerBycCustkey(int? cCustkey)
        {
            var items = Context.Customers
                              .AsNoTracking()
                              .Where(i => i.c_custkey == cCustkey);

            items = items.Include(i => i.Nation);

            var itemToReturn = items.FirstOrDefault();

            OnCustomerGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Customer> CancelCustomerChanges(Models.TpcH.Customer item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCustomerUpdated(Models.TpcH.Customer item);
        partial void OnAfterCustomerUpdated(Models.TpcH.Customer item);

        public async Task<Models.TpcH.Customer> UpdateCustomer(int? cCustkey, Models.TpcH.Customer customer)
        {
            OnCustomerUpdated(customer);

            var itemToUpdate = Context.Customers
                              .Where(i => i.c_custkey == cCustkey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(customer);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCustomerUpdated(customer);

            return customer;
        }

        partial void OnLineitemDeleted(Models.TpcH.Lineitem item);
        partial void OnAfterLineitemDeleted(Models.TpcH.Lineitem item);

        public async Task<Models.TpcH.Lineitem> DeleteLineitem(int? lOrderkey, int? lLinenumber)
        {
            var itemToDelete = Context.Lineitems
                              .Where(i => i.l_orderkey == lOrderkey && i.l_linenumber == lLinenumber)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLineitemDeleted(itemToDelete);

            Context.Lineitems.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLineitemDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnLineitemGet(Models.TpcH.Lineitem item);

        public async Task<Models.TpcH.Lineitem> GetLineitemByLOrderkeyAndLLinenumber(int? lOrderkey, int? lLinenumber)
        {
            var items = Context.Lineitems
                              .AsNoTracking()
                              .Where(i => i.l_orderkey == lOrderkey && i.l_linenumber == lLinenumber);

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Partsupp);

            var itemToReturn = items.FirstOrDefault();

            OnLineitemGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Lineitem> CancelLineitemChanges(Models.TpcH.Lineitem item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLineitemUpdated(Models.TpcH.Lineitem item);
        partial void OnAfterLineitemUpdated(Models.TpcH.Lineitem item);

        public async Task<Models.TpcH.Lineitem> UpdateLineitem(int? lOrderkey, int? lLinenumber, Models.TpcH.Lineitem lineitem)
        {
            OnLineitemUpdated(lineitem);

            var itemToUpdate = Context.Lineitems
                              .Where(i => i.l_orderkey == lOrderkey && i.l_linenumber == lLinenumber)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(lineitem);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterLineitemUpdated(lineitem);

            return lineitem;
        }

        partial void OnNationDeleted(Models.TpcH.Nation item);
        partial void OnAfterNationDeleted(Models.TpcH.Nation item);

        public async Task<Models.TpcH.Nation> DeleteNation(int? nNationkey)
        {
            var itemToDelete = Context.Nations
                              .Where(i => i.n_nationkey == nNationkey)
                              .Include(i => i.Customers)
                              .Include(i => i.Suppliers)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnNationDeleted(itemToDelete);

            Context.Nations.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterNationDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnNationGet(Models.TpcH.Nation item);

        public async Task<Models.TpcH.Nation> GetNationBynNationkey(int? nNationkey)
        {
            var items = Context.Nations
                              .AsNoTracking()
                              .Where(i => i.n_nationkey == nNationkey);

            items = items.Include(i => i.Region);

            var itemToReturn = items.FirstOrDefault();

            OnNationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Nation> CancelNationChanges(Models.TpcH.Nation item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnNationUpdated(Models.TpcH.Nation item);
        partial void OnAfterNationUpdated(Models.TpcH.Nation item);

        public async Task<Models.TpcH.Nation> UpdateNation(int? nNationkey, Models.TpcH.Nation nation)
        {
            OnNationUpdated(nation);

            var itemToUpdate = Context.Nations
                              .Where(i => i.n_nationkey == nNationkey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(nation);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterNationUpdated(nation);

            return nation;
        }

        partial void OnOrderDeleted(Models.TpcH.Order item);
        partial void OnAfterOrderDeleted(Models.TpcH.Order item);

        public async Task<Models.TpcH.Order> DeleteOrder(int? oOrderkey)
        {
            var itemToDelete = Context.Orders
                              .Where(i => i.o_orderkey == oOrderkey)
                              .Include(i => i.Lineitems)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDeleted(itemToDelete);

            Context.Orders.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOrderDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOrderGet(Models.TpcH.Order item);

        public async Task<Models.TpcH.Order> GetOrderByoOrderkey(int? oOrderkey)
        {
            var items = Context.Orders
                              .AsNoTracking()
                              .Where(i => i.o_orderkey == oOrderkey);

            items = items.Include(i => i.Customer);

            var itemToReturn = items.FirstOrDefault();

            OnOrderGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Order> CancelOrderChanges(Models.TpcH.Order item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnOrderUpdated(Models.TpcH.Order item);
        partial void OnAfterOrderUpdated(Models.TpcH.Order item);

        public async Task<Models.TpcH.Order> UpdateOrder(int? oOrderkey, Models.TpcH.Order order)
        {
            OnOrderUpdated(order);

            var itemToUpdate = Context.Orders
                              .Where(i => i.o_orderkey == oOrderkey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(order);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterOrderUpdated(order);

            return order;
        }

        partial void OnPartDeleted(Models.TpcH.Part item);
        partial void OnAfterPartDeleted(Models.TpcH.Part item);

        public async Task<Models.TpcH.Part> DeletePart(int? pPartkey)
        {
            var itemToDelete = Context.Parts
                              .Where(i => i.p_partkey == pPartkey)
                              .Include(i => i.Partsupps)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPartDeleted(itemToDelete);

            Context.Parts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPartDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnPartGet(Models.TpcH.Part item);

        public async Task<Models.TpcH.Part> GetPartBypPartkey(int? pPartkey)
        {
            var items = Context.Parts
                              .AsNoTracking()
                              .Where(i => i.p_partkey == pPartkey);

            var itemToReturn = items.FirstOrDefault();

            OnPartGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Part> CancelPartChanges(Models.TpcH.Part item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPartUpdated(Models.TpcH.Part item);
        partial void OnAfterPartUpdated(Models.TpcH.Part item);

        public async Task<Models.TpcH.Part> UpdatePart(int? pPartkey, Models.TpcH.Part part)
        {
            OnPartUpdated(part);

            var itemToUpdate = Context.Parts
                              .Where(i => i.p_partkey == pPartkey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(part);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterPartUpdated(part);

            return part;
        }

        partial void OnPartsuppDeleted(Models.TpcH.Partsupp item);
        partial void OnAfterPartsuppDeleted(Models.TpcH.Partsupp item);

        public async Task<Models.TpcH.Partsupp> DeletePartsupp(int? psPartkey, int? psSuppkey)
        {
            var itemToDelete = Context.Partsupps
                              .Where(i => i.ps_partkey == psPartkey && i.ps_suppkey == psSuppkey)
                              .Include(i => i.Lineitems)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPartsuppDeleted(itemToDelete);

            Context.Partsupps.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPartsuppDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnPartsuppGet(Models.TpcH.Partsupp item);

        public async Task<Models.TpcH.Partsupp> GetPartsuppByPsPartkeyAndPsSuppkey(int? psPartkey, int? psSuppkey)
        {
            var items = Context.Partsupps
                              .AsNoTracking()
                              .Where(i => i.ps_partkey == psPartkey && i.ps_suppkey == psSuppkey);

            items = items.Include(i => i.Part);

            items = items.Include(i => i.Supplier);

            var itemToReturn = items.FirstOrDefault();

            OnPartsuppGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Partsupp> CancelPartsuppChanges(Models.TpcH.Partsupp item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPartsuppUpdated(Models.TpcH.Partsupp item);
        partial void OnAfterPartsuppUpdated(Models.TpcH.Partsupp item);

        public async Task<Models.TpcH.Partsupp> UpdatePartsupp(int? psPartkey, int? psSuppkey, Models.TpcH.Partsupp partsupp)
        {
            OnPartsuppUpdated(partsupp);

            var itemToUpdate = Context.Partsupps
                              .Where(i => i.ps_partkey == psPartkey && i.ps_suppkey == psSuppkey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(partsupp);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterPartsuppUpdated(partsupp);

            return partsupp;
        }

        partial void OnRegionDeleted(Models.TpcH.Region item);
        partial void OnAfterRegionDeleted(Models.TpcH.Region item);

        public async Task<Models.TpcH.Region> DeleteRegion(int? rRegionkey)
        {
            var itemToDelete = Context.Regions
                              .Where(i => i.r_regionkey == rRegionkey)
                              .Include(i => i.Nations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegionDeleted(itemToDelete);

            Context.Regions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegionDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnRegionGet(Models.TpcH.Region item);

        public async Task<Models.TpcH.Region> GetRegionByrRegionkey(int? rRegionkey)
        {
            var items = Context.Regions
                              .AsNoTracking()
                              .Where(i => i.r_regionkey == rRegionkey);

            var itemToReturn = items.FirstOrDefault();

            OnRegionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Region> CancelRegionChanges(Models.TpcH.Region item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegionUpdated(Models.TpcH.Region item);
        partial void OnAfterRegionUpdated(Models.TpcH.Region item);

        public async Task<Models.TpcH.Region> UpdateRegion(int? rRegionkey, Models.TpcH.Region region)
        {
            OnRegionUpdated(region);

            var itemToUpdate = Context.Regions
                              .Where(i => i.r_regionkey == rRegionkey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(region);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterRegionUpdated(region);

            return region;
        }

        partial void OnSupplierDeleted(Models.TpcH.Supplier item);
        partial void OnAfterSupplierDeleted(Models.TpcH.Supplier item);

        public async Task<Models.TpcH.Supplier> DeleteSupplier(int? sSuppkey)
        {
            var itemToDelete = Context.Suppliers
                              .Where(i => i.s_suppkey == sSuppkey)
                              .Include(i => i.Partsupps)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSupplierDeleted(itemToDelete);

            Context.Suppliers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSupplierDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSupplierGet(Models.TpcH.Supplier item);

        public async Task<Models.TpcH.Supplier> GetSupplierBysSuppkey(int? sSuppkey)
        {
            var items = Context.Suppliers
                              .AsNoTracking()
                              .Where(i => i.s_suppkey == sSuppkey);

            items = items.Include(i => i.Nation);

            var itemToReturn = items.FirstOrDefault();

            OnSupplierGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.TpcH.Supplier> CancelSupplierChanges(Models.TpcH.Supplier item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSupplierUpdated(Models.TpcH.Supplier item);
        partial void OnAfterSupplierUpdated(Models.TpcH.Supplier item);

        public async Task<Models.TpcH.Supplier> UpdateSupplier(int? sSuppkey, Models.TpcH.Supplier supplier)
        {
            OnSupplierUpdated(supplier);

            var itemToUpdate = Context.Suppliers
                              .Where(i => i.s_suppkey == sSuppkey)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(supplier);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterSupplierUpdated(supplier);

            return supplier;
        }
    }
}
