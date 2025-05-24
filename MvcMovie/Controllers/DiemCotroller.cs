using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class DiemCotroller : Controller{
        public IActionResult Index(){
            return View();
        }
        [HttpPost]
        public IActionResult Index(Diem diem){
            if(diem.DiemA <= 0 || diem.DiemB <=0 || diem.DiemC <=0){
                @ViewBag.DiemResult = "Bạn đã trượt môn này!";
            }else if(diem.DiemA >10 || diem.DiemB >10 || diem.DiemC >10){
                @ViewBag.DiemResult = "Số điểm vượt quá quy định, vui lòng nhập lại!";
            }else{
                double result = 0.1*diem.DiemC + diem.DiemB*0.3 + diem.DiemA*0.6;
                string strOutput = "Điểm môn học là: " +result;
                @ViewBag.DiemResult = strOutput;
            }
            return View();
        }
    }
}