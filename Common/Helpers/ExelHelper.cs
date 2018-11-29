using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;

namespace Common.Helpers
{
    public class ExelHelper
    {

        public static System.Data.DataTable GetExcelData(string excelFilePath)
        {
            string oledbConnectionString;
            OleDbConnection objConn = null;
            try
            {
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;";
            }
            catch
            {
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;";
            }
            objConn = new OleDbConnection(oledbConnectionString);

            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }

            var objCmdSelect = new OleDbCommand("Select * from [Sheet1$]", objConn);
            var objAdapter = new OleDbDataAdapter {SelectCommand = objCmdSelect};
            var objDataset = new DataSet();
            objAdapter.Fill(objDataset, "ExcelDataTable");
            objConn.Close();
            return objDataset.Tables[0];
        }

        public static System.Data.DataTable GetExcelData(string excelFilePath, int sheet)
        {
            string oledbConnectionString;
            OleDbConnection objConn = null;
            try
            {
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;";
            }
            catch
            {
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;";
            }
            objConn = new OleDbConnection(oledbConnectionString);

            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }
            var objAdapter = new OleDbDataAdapter();
            try
            {
                var objCmdSelect = new OleDbCommand("Select * from [" + sheet.ToString() + "$]", objConn);
                objAdapter.SelectCommand = objCmdSelect;
            }
            catch
            {

            }

            var objDataset = new DataSet();
            objAdapter.Fill(objDataset, "ExcelDataTable");
            objConn.Close();
            return objDataset.Tables[0];
        }
        public static System.Data.DataTable GetExcelData(string excelFilePath, string sheet)
        {
            string oledbConnectionString;
            OleDbConnection objConn = null;
            try
            {
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;";
            }
            catch
            {
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;";
            }
            objConn = new OleDbConnection(oledbConnectionString);

            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }
            var objAdapter = new OleDbDataAdapter();
            try
            {
                var objCmdSelect = new OleDbCommand("Select * from [" + sheet + "$]", objConn);
                objAdapter.SelectCommand = objCmdSelect;
            }
            catch
            {

            }

            var objDataset = new DataSet();
            objAdapter.Fill(objDataset, "ExcelDataTable");
            objConn.Close();
            return objDataset.Tables[0];
        }

        //Row limits older excel verion per sheet, the row limit for excel 2003 is 65536
        const int RowLimit = 65000;

        private static string GetWorkbookTemplate()
        {
            var sb = new StringBuilder(818);
            sb.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
            sb.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
            sb.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            sb.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
            sb.AppendFormat(@" <Styles>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""Default"" ss:Name=""Normal"">{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Borders/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Interior/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <NumberFormat/>{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Protection/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""s62"">{0}", Environment.NewLine);
            sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""{0}", Environment.NewLine);
            sb.AppendFormat(@"    ss:Bold=""1""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@"  <Style ss:ID=""s63"">{0}", Environment.NewLine);
            sb.AppendFormat(@"   <NumberFormat ss:Format=""Short Date""/>{0}", Environment.NewLine);
            sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
            sb.AppendFormat(@" </Styles>{0}", Environment.NewLine);
            sb.Append(@"{0}\r\n</Workbook>");
            return sb.ToString();
        }

        private static string ReplaceXmlChar(string input)
        {
            input = input.Replace("&", "&amp");
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            input = input.Replace("\"", "&quot;");
            input = input.Replace("'", "&apos;");
            return input;
        }

        private static string GetCell(Type type, object cellData)
        {
            var data = (cellData is DBNull) ? "" : cellData;
            if (type.Name.Contains("Int") || type.Name.Contains("Double") || type.Name.Contains("Decimal")) return string.Format("<Cell><Data ss:Type=\"Number\">{0}</Data></Cell>", data);
            if (type.Name.Contains("Date") && data.ToString() != string.Empty)
            {
                return string.Format("<Cell ss:StyleID=\"s63\"><Data ss:Type=\"DateTime\">{0}</Data></Cell>", Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
            }
            return string.Format("<Cell><Data ss:Type=\"String\">{0}</Data></Cell>", ReplaceXmlChar(data.ToString()));
        }
        private static string GetWorksheets(DataSet source)
        {
            var sw = new StringWriter();
            if (source == null || source.Tables.Count == 0)
            {
                sw.Write("<Worksheet ss:Name=\"Sheet1\">\r\n<Table>\r\n<Row><Cell><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                return sw.ToString();
            }
            foreach (DataTable dt in source.Tables)
            {
                if (dt.Rows.Count == 0)
                    sw.Write("<Worksheet ss:Name=\"" + ReplaceXmlChar(dt.TableName) + "\">\r\n<Table>\r\n<Row><Cell  ss:StyleID=\"s62\"><Data ss:Type=\"String\"></Data></Cell></Row>\r\n</Table>\r\n</Worksheet>");
                else
                {
                    //write each row data                
                    var sheetCount = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((i % RowLimit) == 0)
                        {
                            //add close tags for previous sheet of the same data table
                            if ((i / RowLimit) > sheetCount)
                            {
                                sw.Write("\r\n</Table>\r\n</Worksheet>");
                                sheetCount = (i / RowLimit);
                            }
                            sw.Write("\r\n<Worksheet ss:Name=\"" + ReplaceXmlChar(dt.TableName) +
                                     (((i / RowLimit) == 0) ? "" : Convert.ToString(i / RowLimit)) + "\">\r\n<Table>");
                            //write column name row
                            sw.Write("\r\n<Row>");
                            foreach (DataColumn dc in dt.Columns)
                                sw.Write(string.Format("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>", ReplaceXmlChar(dc.ColumnName)));
                            sw.Write("</Row>");
                        }
                        sw.Write("\r\n<Row>");
                        foreach (DataColumn dc in dt.Columns)
                            sw.Write(GetCell(dc.DataType, dt.Rows[i][dc.ColumnName]));
                        sw.Write("</Row>");
                    }
                    sw.Write("\r\n</Table>\r\n</Worksheet>");
                }
            }

            return sw.ToString();
        }
        public static string GetExcelXml(DataTable dtInput, string filename)
        {
            var excelTemplate = GetWorkbookTemplate();
            var ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            var worksheets = GetWorksheets(ds);
            var excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }

        public static string GetExcelXml(DataSet dsInput, string filename)
        {
            var excelTemplate = GetWorkbookTemplate();
            var worksheets = GetWorksheets(dsInput);
            var excelXml = string.Format(excelTemplate, worksheets);
            return excelXml;
        }

        public static void ToExcel(DataSet dsInput, string filename, HttpResponseBase response)
        {
            var excelXml = GetExcelXml(dsInput, filename);
            response.Clear();
            response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            response.AppendHeader("Content-disposition", "attachment; filename=" + filename);
            response.Write(excelXml);
            response.Flush();
            response.End();
        }

        public static void ToExcel(DataTable dtInput, string filename, HttpResponseBase response)
        {
            var ds = new DataSet();
            ds.Tables.Add(dtInput.Copy());
            ToExcel(ds, filename, response);
        }
    }
}