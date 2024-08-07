using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
namespace PLAN_MODULE
{
    public static class excel
    {

        // Export DataTable into an excel file with field names in the header line
        // - Save excel file without ever making it visible if filepath is given
        // - Don't save excel file, just make it visible if no filepath is given
        public static void ExportToExcel(this System.Data.DataTable tbl, string nameFile, string billNumber = "", int sumRe = 0, int sumReal = 0, string stock = "", string note = "")
        {
            try
            {
                string excelFilePath = @"D:\" + nameFile + ".xlsx";
                DateTime time = DateTime.Now;
                string day = time.Day.ToString();
                string month = time.Month.ToString();
                string year = time.Year.ToString();
                Application xlApp = new Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                    return;
                }
                xlApp.Visible = false;

                object misValue = System.Reflection.Missing.Value;
                //Excel.Workbook
                Workbook wb = xlApp.Workbooks.Add(misValue);

                Worksheet ws = (Worksheet)wb.Worksheets[1];

                //IWorkbook wb = spreadsheetControl1.Document;
                //Worksheet ws = wb.Worksheets[0];

                if (ws == null)
                {
                    MessageBox.Show("Không thể tạo được WorkSheet");
                    return;
                }
                int row = 1;
                string fontName = "Times New Roman";
                int fontSizeTieuDe = 18;
                int fontSizeTenTruong = 14;
                int fontSizeNoiDung = 12;

                Range row1 = ws.get_Range("E1", "G1");
                row1.Merge();
                row1.Font.Size = fontSizeNoiDung;
                row1.Font.Name = fontName;
                row1.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row1.Value2 = "Số :" + billNumber;

                //Tiêu đề
                Range row2 = ws.get_Range("A3", "G3");
                row2.Merge();
                row2.Font.Size = fontSizeTieuDe;
                row2.Font.Name = fontName;
                row2.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row2.Value2 = "PHIẾU XUẤT KHO";
                row2.Font.Bold = true;

                Range row3 = ws.get_Range("A4", "G4");
                row3.Merge();
                row3.Font.Size = fontSizeNoiDung;
                row3.Font.Name = fontName;
                row3.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row3.Value2 = "Ngày " + day + " Tháng " + month + " Năm " + year;

                Range row4 = ws.get_Range("A6", "C6");
                row4.Merge();
                row4.Font.Size = fontSizeNoiDung;
                row4.Font.Name = fontName;
                row4.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row4.Value2 = "Họ và tên người nhận :                Bộ Phận :           Phương tiện :";

                Range row5 = ws.get_Range("A8", "B8");
                row5.Merge();
                row5.Font.Size = fontSizeNoiDung;
                row5.Font.Name = fontName;
                row5.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row5.Value2 = "Lý do xuất kho : " + note;

                Range row7 = ws.get_Range("A10", "G10");
                row7.Merge();
                row7.Font.Size = fontSizeNoiDung;
                row7.Font.Name = fontName;
                row7.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row7.Value2 = "Xuất tại kho :               Địa điểm :              ";

                //Tạo Ô Số Thứ Tự (STT)
                Range row23_STT = ws.get_Range("A12", "A13");//Cột A dòng 2 và dòng 3
                row23_STT.Merge();
                row23_STT.Font.Size = fontSizeTenTruong;
                row23_STT.Font.Name = fontName;
                row23_STT.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row23_STT.Value2 = "STT";

                //Tạo  Mã Sản phẩm :
                Range row_MaSP = ws.get_Range("B12", "B13");
                row_MaSP.Merge();
                row_MaSP.Font.Size = fontSizeTenTruong;
                row_MaSP.Font.Name = fontName;
                row_MaSP.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_MaSP.Value2 = "Tên hàng (Mã hàng)";
                row_MaSP.ColumnWidth = 50;

                Range row_Unit = ws.get_Range("C12", "C13");
                row_Unit.Merge();
                row_Unit.Font.Size = fontSizeTenTruong;
                row_Unit.Font.Name = fontName;
                row_Unit.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Unit.Value2 = "ĐVT";
                row_Unit.ColumnWidth = 20;

                Range row_Work = ws.get_Range("D12", "D13");
                row_Work.Merge();
                row_Work.Font.Size = fontSizeTenTruong;
                row_Work.Font.Name = fontName;
                row_Work.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Work.Value2 = "Tên Work";
                row_Work.ColumnWidth = 20;

                Range row_SoLuong = ws.get_Range("E12", "F12");
                row_SoLuong.Merge();
                row_SoLuong.Font.Size = fontSizeTenTruong;
                row_SoLuong.Font.Name = fontName;
                row_SoLuong.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_SoLuong.Value2 = "Số Lượng";

                Range row_Request = ws.get_Range("E13", "E13");//Ô D3
                row_Request.Font.Size = fontSizeTenTruong;
                row_Request.Font.Name = fontName;
                row_Request.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Request.Value2 = "Yêu Cầu";
                row_Request.ColumnWidth = 20;

                Range row_Real = ws.get_Range("F13", "F13");//Ô E3
                row_Real.Font.Size = fontSizeTenTruong;
                row_Real.Font.Name = fontName;
                row_Real.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Real.Value2 = "Thực xuất";
                row_Real.ColumnWidth = 20;

                Range row_Note = ws.get_Range("G12", "G13");
                row_Note.Merge();
                row_Note.Font.Size = fontSizeTenTruong;
                row_Note.Font.Name = fontName;
                row_Note.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Note.Value2 = "Ghi chú";
                row_Note.ColumnWidth = 40;
                //data
                row = 13;
                int stt = 0;
                foreach (DataRow item in tbl.Rows)
                {
                    stt++;
                    row++;
                    dynamic[] arr = { stt, item["MFG_PART"], item["UNIT"], item["WORK"], item["REQUESTS"], item["EXPORTS_REAL"], item["NOTE"] };
                    Range rowData = ws.get_Range("A" + row, "G" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                    rowData.RowHeight = 40;
                }

                row++;
                Range row_Sum = ws.get_Range("B" + row, "D" + row);
                row_Sum.Merge();
                row_Sum.Font.Size = fontSizeTenTruong;
                row_Sum.Font.Name = fontName;
                row_Sum.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Sum.Value2 = "Tổng";
                row_Sum.RowHeight = 50;

                Range row_SumRe = ws.get_Range("E" + row, "E" + row);
                row_SumRe.Merge();
                row_SumRe.Font.Size = fontSizeTenTruong;
                row_SumRe.Font.Name = fontName;
                row_SumRe.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_SumRe.Value2 = sumRe;
                row_SumRe.RowHeight = 50;

                Range row_SumReal = ws.get_Range("F" + row, "F" + row);
                row_SumReal.Merge();
                row_SumReal.Font.Size = fontSizeTenTruong;
                row_SumReal.Font.Name = fontName;
                row_SumReal.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_SumReal.Value2 = sumReal;
                row_SumReal.RowHeight = 50;

                BorderAround(ws.get_Range("A12", "G" + row));
                row = row + 5;
                Range row_Manager = ws.get_Range("B" + row, "B" + row);
                row_Manager.Merge();
                row_Manager.Font.Size = fontSizeTenTruong;
                row_Manager.Font.Name = fontName;
                row_Manager.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Manager.Value2 = "Phụ trách duyệt";
                //row_Manager.RowHeight = 10;



                Range row_Acc = ws.get_Range("C" + row, "C" + row);
                row_Acc.Merge();
                row_Acc.Font.Size = fontSizeTenTruong;
                row_Acc.Font.Name = fontName;
                row_Acc.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Acc.Value2 = "Thủ Kho";

                Range row_Sec = ws.get_Range("D" + row, "D" + row);
                row_Sec.Merge();
                row_Sec.Font.Size = fontSizeTenTruong;
                row_Sec.Font.Name = fontName;
                row_Sec.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Sec.Value2 = "Bảo Vệ";

                Range row_TK = ws.get_Range("F" + row, "F" + row);
                row_TK.Merge();
                row_TK.Font.Size = fontSizeTenTruong;
                row_TK.Font.Name = fontName;
                row_TK.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_TK.Value2 = "Kế Toán";

                Range row_Rec = ws.get_Range("G" + row, "G" + row);
                row_Rec.Merge();
                row_Rec.Font.Size = fontSizeTenTruong;
                row_Rec.Font.Name = fontName;
                row_Rec.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Rec.Value2 = "Người Nhận";

                row = row + 3;
                Range row_Stock = ws.get_Range("C" + row, "C" + row);
                row_Stock.Merge();
                row_Stock.Font.Size = fontSizeTenTruong;
                row_Stock.Font.Name = fontName;
                row_Stock.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                row_Stock.Value2 = stock;
                // check file path

                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        wb.SaveAs(excelFilePath);
                        wb.Close(true, misValue, misValue);
                        xlApp.Quit();
                        MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    xlApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }

        private static void BorderAround(Range range)
        {
            throw new NotImplementedException();
        }

        public static void ExportToExcel(this System.Data.DataTable tbl, string excelFilePath = null)
        {
            try
            {
                if (tbl == null || tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (var i = 0; i < tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = tbl.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = tbl.Rows[i][j];
                    }
                }
                // check file path
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        workSheet.SaveAs(excelFilePath);
                        excelApp.Quit();
                        MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
        public static void ToCSV(this DataTable dtDataTable, string partFile)
        {
            StreamWriter sw = new StreamWriter($@"{partFile}.csv", false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static void ExToCSV(this DataTable dt)
        {

        }
       
    }

}
