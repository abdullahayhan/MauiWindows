using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MauiApp2.Data;

public class Sale
{
    [Required]
    public string SiraNo { get; set; }
    [Required]
    public string FaturaTarihi { get; set; }
    [Required]
    public string FaturaNo { get; set; }
    [Required]
    public string AdiSoyadi { get; set; }
    [Required]
    public string VergiNo { get; set; }
    [Required]
    public string Matrah { get; set; }
    [Required]
    public string KDV20_Matrah { get; set; }
    [Required]
    public string KDV20 { get; set; }
    [Required]
    public string KDV18_Matrah { get; set; }
    [Required]
    public string KDV18 { get; set; }
    [Required]
    public string KDV10_Matrah { get; set; }
    [Required]
    public string KDV10 { get; set; }
    [Required]
    public string KDV8_Matrah { get; set; }
    [Required]
    public string KDV8 { get; set; }
    [Required]
    public string KDV1_Matrah { get; set; }
    [Required]
    public string KDV1 { get; set; }
    [Required]
    public string KDV0_Matrah { get; set; }
    [Required]
    public string GenelIskonto { get; set; }
    [Required]
    public string GenelToplam { get; set; }
    [Required]
    public string ParaBirimi { get; set; }
    [Required]
    public string DovizKuru { get; set; }
    [Required]
    public string KDVliKargoTutari { get; set; }
    [Required]
    public string SatisKanali { get; set; }
    [Required]
    public string FaturaTipi { get; set; }
    [Required]
    public string FaturaDurumu { get; set; }
    [Required]
    public string MagazaAdi { get; set; }
    [Required]
    public string OdemeYontemi { get; set; }
    [PrimaryKey]
    [Required]
    public string EFatura_UUID { get; set; }
    [Required]
    public string SiparisNo { get; set; }
    public string HesaplananTevkifat { get; set; }
}

