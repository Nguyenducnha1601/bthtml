using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class BMIController : Controller{
        public IActionResult Index(){
            return View();
        }
        [HttpPost]
        public IActionResult Index(BMI bmi){
            double bmiResult = (bmi.Nang / (bmi.Cao * bmi.Cao));
            string strOutput = "Chi so BMI cua ban la: "+bmiResult;
            if (bmi.Cao <= 0 || bmi.Nang <= 0){
                ViewBag.BMIResult = "Vui long nhap lai";
            }else{
                if(bmiResult <18.5){
                    @ViewBag.BMIResult = strOutput +"Gầy";
                }
                else if(bmiResult <24.9){
                    @ViewBag.BMIResult = strOutput +"Bình Thường";
                }
                else if(bmiResult <29.9){
                    @ViewBag.BMIResult = strOutput +"Thừa Cân";
                }
                else{
                    @ViewBag.BMIResult = strOutput +"Béo phì";

                }
            }
            return View();
        }
    }
}