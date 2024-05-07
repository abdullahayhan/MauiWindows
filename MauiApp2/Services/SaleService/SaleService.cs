using MauiApp2.Data;
using MudBlazor.Extensions;
using OfficeOpenXml;
using SQLite;
using System.Reflection;


namespace MauiApp2.Services.SaleService;

public class SaleService : ISaleRepository
{
    private SQLiteAsyncConnection _db;
    private readonly string _dbPath;

    public SaleService(string dbPath)
    {
        _dbPath = dbPath;
        InitAsync();
    }

    private async Task InitAsync()
    {
        if (_db is not null)
        {
            return;
        }

        _db = new SQLiteAsyncConnection(_dbPath);
        await _db.CreateTableAsync<Sale>();
    }

    public async Task<bool> AddUpdateSaleAsync(Sale sale)
    {
        if (!string.IsNullOrEmpty(sale.EFatura_UUID))
        {
            await _db.UpdateAsync(sale);
        }
        else
        {
            await _db.InsertAsync(sale);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteSaleAsync(string uuid)
    {
        await _db.DeleteAsync<Sale>(uuid);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAllSaleAsync()
    {
        await _db.DeleteAllAsync<Sale>();
        return await Task.FromResult(true);
    }

    public async Task<IEnumerable<Sale>> GetAllSalesAsync(SaleViewModel filterModel)
    {
        var query = _db.Table<Sale>();

        if (filterModel.ParaBirimi != "-") query = query.Where(s => s.ParaBirimi == filterModel.ParaBirimi);
        if (filterModel.SatisKanali != "-") query = query.Where(s => s.SatisKanali == filterModel.SatisKanali);
        if (filterModel.FaturaTipi != "-") query = query.Where(s => s.FaturaTipi == filterModel.FaturaTipi);

        return await Task.FromResult(await query.ToListAsync());
    }

    public async Task<Sale> GetSaleAsync(string uuid)
    {
        return await _db.Table<Sale>().Where(p => p.EFatura_UUID == uuid).FirstOrDefaultAsync();
    }

    public async Task<bool> ImportSalesFromExcelAsync(Stream excelStream)
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(excelStream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet != null)
            {
                int rowCount = worksheet.Dimension.Rows;
                List<Sale> importedSales = new List<Sale>();

                for (int row = 2; row <= rowCount; row++)
                {
                    PropertyInfo[] properties = typeof(Sale).GetProperties();
                    Sale sale = new Sale();
                    int columnIndex = 1;

                    foreach (PropertyInfo property in properties)
                    {
                        object value = worksheet.Cells[row, columnIndex].GetValue<string>();
                        property.SetValue(sale, value);
                        columnIndex++;
                    }

                    importedSales.Add(sale);
                }

                await _db.InsertAllAsync(importedSales);
                return await Task.FromResult(true);
            }
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
            //throw;
        }
        return await Task.FromResult(false);

    }

    public async Task<int> GetLastSiraNo()
    {
       int saleCount = _db.Table<Sale>()
            .ToListAsync()
            .Result
            .Count;

        return await Task.FromResult(saleCount);
    }
}
