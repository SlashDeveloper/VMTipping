using System;
using System.IO;
using System.Runtime.InteropServices;
using VMTipping.Model;
using Excel = Microsoft.Office.Interop.Excel;

namespace VMTippingClient
{
    public class ExcelReader
    {
        public User GetResult(string file)
        {
            Excel.Application xlApp;
            User userPrediction;
            //Try to reuse an excel application object
            try
            {
                xlApp = Marshal.GetActiveObject("Excel.Application") as Excel.Application;
            }
            catch
            {
                // Create new application
                xlApp = new Excel.Application();
            }

            try
            {
           

                Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(file);
                xlApp.DisplayAlerts = false;

                userPrediction = new ExcelUserPredictionReader().GetUserPrectionFromWorkbook(xlWorkBook);

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                //xlApp.Visible = true;
                //xlApp.UserControl = true;

                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                return userPrediction;
            }
            catch (Exception ex)
            {
                //args.Exception = ex;
                throw new ApplicationException("Åpning av workbook feilet: " + ex.Message);

            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}