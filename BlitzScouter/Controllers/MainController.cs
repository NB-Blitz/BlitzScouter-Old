using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BlitzScouter.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scout(Models.DataModel data)
        {
            return View(data);
        }
        
        [HttpPost]
        public IActionResult Data(Models.DataModel model)
        {
            uploadData(model); // TODO: Multi Threading
            return View();
        }

        // Redirect to Index When Manually Connecting to Scout or Data
        public IActionResult Scout() { return RedirectToAction("Index"); }
        public IActionResult Data() { return RedirectToAction("Index"); }

        // Data Management Methods
        private bool uploadData(Models.DataModel model)
        {
            string path = "./data.xlsx";
            FileInfo info = new FileInfo(path);
            
            using (ExcelPackage ep = new ExcelPackage(info))
            {
                ExcelWorksheet sheet;
                if (ep.Workbook.Worksheets.ToArray().Length > 0)
                {
                    sheet = ep.Workbook.Worksheets["Raw Data"];
                }
                else
                {
                    sheet = ep.Workbook.Worksheets.Add("Raw Data");
                    string[] title = model.condenseTitles();
                    for (int i = 0; i < title.Length; i++)
                        sheet.Cells[1, i + 1].Value = title[i];
                }
                //ep.SaveAs(info);

                //sheet.InsertRow(sheet.Dimension.End.Row, 1);
                sheet.Cells[sheet.Dimension.End.Row + 1, 1].Value = "0";
                string[] data = model.condenseData();
                for (int i = 0; i < data.Length; i++)
                   sheet.Cells[sheet.Dimension.End.Row, i + 1].Value = data[i];

                ep.SaveAs(info);
            }
            return System.IO.File.Exists(path);
        }
    }
}