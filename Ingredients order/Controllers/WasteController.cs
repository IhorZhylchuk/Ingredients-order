using BarcodeLib;
using Ingredients_order.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Ingredients_order.Controllers
{
    public class WasteController : Controller
    {
       public IActionResult Test()
        {
            return View();
        }
    }
}
