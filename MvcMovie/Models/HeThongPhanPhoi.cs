using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models;

[Table("DsHeThongPhanPhoi")]
public class HeThongPhanPhoi
{
    [Key]
    public string? MaHTPP { get; set; }
    public string? TenHTPP {get; set; }
}