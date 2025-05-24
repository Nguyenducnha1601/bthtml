using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MvcMovie.Models;
namespace MvcMovie.Controllers;
    public class MoneyController: Controller{
        public IActionResult Index(){
            return View();
        }
        [HttpPost]
        public IActionResult Index (Money money){
            if(money.Dongia>0 || money.Soluong>0 ){
                double tong = money.Dongia*money.Soluong;
                @ViewBag.Tongtien = "Sản phẩm" +money.TenSP +"Tổng số tiền phải trả là:" + tong +"VND";
            }else{
                @ViewBag.Tongtien ="Vui lòng nhập lại! ";
            }
            return View();
        }
    }