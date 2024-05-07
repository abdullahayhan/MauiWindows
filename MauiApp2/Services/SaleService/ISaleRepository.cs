using MauiApp2.Data;

namespace MauiApp2.Services.SaleService;

public interface ISaleRepository
{
    Task<bool> AddUpdateSaleAsync(Sale sale);
    Task<bool> DeleteSaleAsync(string uuid);
    Task<bool> DeleteAllSaleAsync();
    Task<Sale> GetSaleAsync(string uuid);
    Task<IEnumerable<Sale>> GetAllSalesAsync(SaleViewModel filterModel);
    Task<bool> ImportSalesFromExcelAsync(Stream excelStream);
    Task<int> GetLastSiraNo();
}
