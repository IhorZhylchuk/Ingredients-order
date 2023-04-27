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
        private readonly ApplicationDbContext dBContext;

        public WasteController(ApplicationDbContext context)
        {
            dBContext = context;

        }

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                MainBinAttachment binAttachment = new MainBinAttachment();
                ViewBag.Processes = new SelectList(binAttachment.Processes, "Id", "Name");
                return View();
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return View();
        }

        [Authorize]
        public IActionResult AttachDetach()
        {
            MainBinAttachment binAttachment = new MainBinAttachment();
            ViewBag.Processes = new SelectList(binAttachment.Processes, "Id", "Name");
            return View();
        }

        [Authorize]
        public JsonResult CheckAttaching(string process, string machine)
        {
            string bin = "";
            try
            {
                var machineName = dBContext.Machines.Where(m => m.ProcessModelId == Int32.Parse(process)).Where(m => m.Id == Int32.Parse(machine)).Select(m => m).FirstOrDefault();
                bin = dBContext.Bins.Where(p => p.ProcessId == Int32.Parse(process) && p.Machine == machineName).Select(b => b.BinNumber).FirstOrDefault();

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            if (bin == "")
            {
                return Json(null);
            }
            return Json(new { binNumber = bin });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AttachDetach(AttachedStringNamesModel model)
        {
            if (model != null) {
                BinAttachmentModel bin = new BinAttachmentModel();
                bin.Id = dBContext.Bins.Where(n => n.BinNumber == model.BinNumber).Select(i => i.Id).Single();
                bin.MachineName = dBContext.Machines.Where(i => i.Id == Int32.Parse(model.MachineName)).Select(n => n.Name).Single();
                bin.Machine = dBContext.Machines.Where(m => m.ProcessModelId == Int32.Parse(model.ProcessName)).Where(m => m.Id == Int32.Parse(model.MachineName)).Select(m => m).Single();
                bin.ProcessId = Int32.Parse(model.ProcessName);
                bin.ProcessName = dBContext.Processes.Where(i => i.Id == Int32.Parse(model.ProcessName)).Select(n => n.Name).Single();
                bin.BinNumber = model.BinNumber;
                bin.BinStatus = "Filling";

                dBContext.Bins.Update(bin);
                await dBContext.SaveChangesAsync();

                return RedirectToAction("Index", "Waste");
            }

            return RedirectToAction("Index", "Waste");
        }

        [Authorize]
        public IActionResult BinAttachment()
        {
            MainBinAttachment binAttachment = new MainBinAttachment();
            ViewBag.Processes = new SelectList(binAttachment.Processes, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BinAttachment(AttachedStringNamesModel model)
        {
            if(model != null)
            {
                try
                {
                    BinAttachmentModel bin = DefaultRecipies.Attach(dBContext, model);
                    dBContext.Bins.Update(bin);
                    await dBContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Waste");
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return RedirectToAction("Index", "Waste");
        }

         [Authorize]
        public JsonResult BinStatus(string binNumber)
        {
            if(binNumber != null)
            {
                try
                {
                    var bin = dBContext.Bins.Where(n => n.BinNumber == binNumber).Select(b => b);
                    var status = bin.Select(s => s.BinStatus).SingleOrDefault();
                    var process = bin.Select(p => p.ProcessName).SingleOrDefault();
                    var machine = bin.Select(m => m.MachineName).SingleOrDefault();

                    return Json(new { status, process, machine });
                }catch(Exception e)
                {
                    return Json(e.StackTrace);
                }
            }
            return Json(null);
        }

        [Authorize]
        public JsonResult BinStatusForDetaching(int id)
        {
            if(id != 0)
            {
                try
                {
                    var bin = dBContext.Bins.Where(n => n.Id == id).Select(b => b);
                    var status = bin.Select(s => s.BinStatus).SingleOrDefault();
                    var process = bin.Select(p => p.ProcessName).SingleOrDefault();
                    var machine = bin.Select(m => m.MachineName).SingleOrDefault();
                    var binNumber = bin.Select(b => b.BinNumber).SingleOrDefault();

                    return Json(new { status, process, machine, binNumber });
                }
                catch(Exception e)
                {
                    return Json("Some error occured");
                }
            }
            return Json(null);

        }

        [Authorize]
        public JsonResult Machines(int ProcessModelId)
        {
            if (ProcessModelId != 0)
            {
                try
                {
                    MainBinAttachment binAttachment = new MainBinAttachment();
                    var result = binAttachment.Machines.Where(p => p.ProcessModelId == ProcessModelId);
                    return Json(new SelectList(result, "Id", "Name"));
                }
                catch (Exception e)
                {
                    return Json("Some error occured");
                }
            }
            return Json(null);

        }


        public IActionResult AttachDetachMenu()
        {

            IEnumerable<MachineModel> machines = dBContext.Machines.Select(m => m).ToArray();
            IEnumerable<ProcessModel> processes = dBContext.Processes.Select(n => n).ToList();
            var bins = dBContext.Bins.Where(s => s.BinStatus == "Filling").Select(b => b).ToList();



            ViewBag.Processes = processes;
            ViewBag.Machines = machines;
            ViewBag.Bins = bins;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AttachDetachMenu(AttachedStringNamesModel model)
        {
            BinAttachmentModel bin = DefaultRecipies.Attach(dBContext, model);
            dBContext.Bins.Update(bin);
            await dBContext.SaveChangesAsync();
            return RedirectToAction("AttachDetachMenu", "Waste");
        }

        [Authorize]
        public JsonResult GetBin(int id)
        {
            if (id != 0)
            {
                try
                {
                    var binNumber = dBContext.Bins.Where(i => i.Machine.Id == id).Select(n => n.BinNumber).ToList();
                    return Json(binNumber);
                }
                catch (Exception e)
                {
                    return Json("Some error occured");
                }
            }
            return Json(null);

        }

        [Authorize]
        public JsonResult GetBins()
        {
            try
            {
                IEnumerable<BinAttachmentModel> list = dBContext.Bins.Select(b => b).ToList();
                return Json(list);
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return Json(null);
        }

        [Authorize]
        public JsonResult BinsChecking(string number)
        {
            if (number != null)
            {
                try
                {
                    var containing = dBContext.Bins.Select(n => n.BinNumber).Contains(number);
                    if (number.Length == 12 && containing == false)
                    {
                        return Json(false);
                    }

                    return Json(true);
                }
                catch (Exception e)
                {
                    return Json("Some error occured");
                }
            }
            return Json(null);

        }

        [Authorize]
        [HttpGet]
        public IActionResult BinAdding()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BinAdding(BinAttachmentModel bin)
        {
            if (bin != null)
            {
                try
                {
                    BinAttachmentModel binAdded = new BinAttachmentModel();
                    binAdded.Id = bin.Id;
                    binAdded.BinNumber = bin.BinNumber;
                    binAdded.BinStatus = "Free to use";
                    await dBContext.AddAsync(binAdded);
                    dBContext.SaveChanges();
                    return RedirectToAction("Index", "Waste");
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }

            }
            return View();

        }

        [Authorize]
        public async Task<IActionResult> Detach(string binNumber)
        {
            if(binNumber != null)
            {
                try
                {
                    BinAttachmentModel bin = DefaultRecipies.Detach(dBContext, binNumber);

                    dBContext.Bins.Update(bin);
                    await dBContext.SaveChangesAsync();
                    return Json(new { success = true, message = "Detached successfully" });
                }catch(Exception e)
                {
                    return Json(new { success = false, message = "Some error occured!" });
                }
            }
            return Json(new { success = false, message = "Some error occured!" });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BinDetaching(string binNumber)
        {
            if(binNumber != null)
            {
                try
                {
                    BinAttachmentModel bin = DefaultRecipies.Detach(dBContext, binNumber);
                    dBContext.Bins.Update(bin);
                    await dBContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Waste");
                }
                catch(Exception e)
                {
                    e.Message.ToString();
                }
            }
            return RedirectToAction("Index", "Waste");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id != null)
            {
                try
                {
                    var result = dBContext.Bins.Where(b => b.Id == id).FirstOrDefault();
                    dBContext.Bins.Remove(result);
                    await dBContext.SaveChangesAsync();
                    return Json(new { success = true, message = "Removed successfully" });
                }
                catch(Exception e)
                {
                    return Json(new { success = false, message = "Some error occured!" });
                }
            }
            return Json(new { success = false, message = "Some error occured!" });

        }

        [Authorize]
        public IActionResult GenerateBarCode()
        {
            return View();
        }

        [Authorize]
        public JsonResult GenerateCodeById(int id)
        {
            try
            {
                var bin = dBContext.Bins.Where(i => i.Id == id).Select(b => b.BinNumber);
                return Json(bin);
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return Json(null);
        }

        [Authorize]
        [HttpPost]
        public IActionResult GenerateBarCode(BinAttachmentModel bin)
        {
            if (bin != null)
            {
                return RedirectToAction("GenerateBarCodeMain", new { code = bin.BinNumber });
            }
            return View();
        }

        [Authorize]
        public IActionResult GenerateBarCodeMain(string code)
        {
            try
            {
                Barcode barcode = new Barcode();
                barcode.IncludeLabel = true;
                barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
                barcode.AlternateLabel = code;
                barcode.LabelFont = new Font(System.Drawing.FontFamily.GenericSansSerif, 18, FontStyle.Bold, GraphicsUnit.Pixel);
                Image image = barcode.Encode(BarcodeLib.TYPE.CODE39Extended, code, Color.Black, Color.White, 400, 200);

                var data = ConvertImageToBytes(image);

                return File(data, "image/jpeg");
            }
            catch(Exception e)
            {
                e.Message.ToString();
            }
            return NotFound();
        }

        private byte[] ConvertImageToBytes(Image img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
