using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcMovie.Models;

[Table("DaiLys")]
public class DaiLy:HeThongPhanPhoi
{
    public string? MaDaiLy { get; set; }
    public string? TenDaiLy  {get; set; }
    public string? DiaChi { get; set; }
    public string? NguoiDaiDien { get; set; }
    public string? DienThoai { get; set; }
    [Key]
    public string? MaHTPP { get; set; }
    
}