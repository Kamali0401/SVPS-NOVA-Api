using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Infrastructure.Constants;

namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class AttendanceReportRepository :IAttendanceReportRepository
    {

        


            private readonly IDataBaseConnection _db;
            private readonly string _connectionString;
            /// <summary>
            /// Initializes a new instance of the <see cref="UpcomingCompetitionRepository"/> class.
            /// </summary>
            /// <param name="_db">The database connection for accessing billing data.</param>
            public AttendanceReportRepository(IDataBaseConnection db, IConfiguration configuration)
            {
                _db = db;
                _connectionString = configuration.GetConnectionString("DbConnection");
            }

        public async Task<string> generateMonthlyAttendancereport(int startMonth, int startYear, int endMonth, int endYear, int sectionId, string grade, string section)
        {
            var spMonthwiseAtt = SPNames.SP_MonthwiseAttendance;
            string strfilepath = "";
            // DataTable dtCloned = new DataTable();

            try
            {

                var con = _connectionString;
                await using (SqlConnection myConnection = new SqlConnection(con))
                {
                    SqlCommand objCmd = new SqlCommand(spMonthwiseAtt, myConnection);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    using var da = new SqlDataAdapter(objCmd);
                    DataSet ds = new DataSet();
                    objCmd.Parameters.Add("@StartMonth", SqlDbType.Int).Value = startMonth;
                    objCmd.Parameters.Add("@StartYear", SqlDbType.Int).Value = startYear;
                    objCmd.Parameters.Add("@EndMonth", SqlDbType.Int).Value = endMonth;
                    objCmd.Parameters.Add("@EndYear", SqlDbType.Int).Value = endYear;
                    objCmd.Parameters.Add("@SectionId", SqlDbType.Int).Value = sectionId;

                    objCmd.CommandTimeout = 100000;
                    da.Fill(ds);
                    var dataTable = ds.Tables[0];
                    // Create a new DataTable for the transformed data
                    if (dataTable.Rows.Count == 0)
                        //return NotFound("No attendance records found.");
                        throw new Exception("No attendance records found.");
                    strfilepath = GenerateExcel(dataTable, "Cumulative Report- Grade:" + grade + "/ Section: " + section);

                }
                return strfilepath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_logger.LogError(ex.InnerException.ToString());

                return ex.Message;
            }
        }
        private string GenerateExcel(DataTable dataTable, string reportname)
        {
            // Define folder and file paths
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string filePath = Path.Combine(folderPath, "Report.xlsx");

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Delete the old file (if it exists)
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Attendance Report");

                // Add title row
                worksheet.Cell(1, 1).Value = "Sona Valliappa Public School";
                worksheet.Range(1, 1, 1, dataTable.Columns.Count).Merge();
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 14;
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Add report name row
                worksheet.Cell(2, 1).Value = reportname;
                worksheet.Range(2, 1, 2, dataTable.Columns.Count).Merge();
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Style.Font.FontSize = 14;
                worksheet.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Add header row
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cell(3, col + 1).Value = dataTable.Columns[col].ColumnName;
                    worksheet.Cell(3, col + 1).Style.Font.Bold = true;
                }

                // Add data rows
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cell(row + 4, col + 1).Value = dataTable.Rows[row][col].ToString();
                    }
                }

                // Auto adjust column width
                worksheet.Columns().AdjustToContents();

                // Save the file
                workbook.SaveAs(filePath);
            }

            return filePath; // Return the full file path
        }
        public async Task<string> generateExcelList(string role)
        {
            var spMonthwiseAtt = role == "Student" ? SPNames.SP_GETALLSTUDENTDETAILWITHSECTION : SPNames.SP_GETALLFACULTYLIST;
            string strfilepath = "";
            // DataTable dtCloned = new DataTable();

            try
            {

                var con = _connectionString;
                await using (SqlConnection myConnection = new SqlConnection(con))
                {
                    SqlCommand objCmd = new SqlCommand(spMonthwiseAtt, myConnection);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(objCmd))
                    {
                        DataSet ds = new DataSet();

                        objCmd.CommandTimeout = 100000;
                        da.Fill(ds);
                        var dataTable = ds.Tables[0];
                        // Create a new DataTable for the transformed data
                        if (dataTable.Rows.Count == 0)
                            // return NotFound("No attendance records found.");
                            throw new Exception("No attendance records found.");
                        strfilepath = GenerateExcel(dataTable, role.ToUpper() + " List");
                    }

                }
                return strfilepath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_logger.LogError(ex.InnerException.ToString());

                return ex.Message;
            }
        }
        public async Task<string> generateDailyAttendancereport(int month, int year, int sectionId, string grade, string section)
        {
            var spMonthwiseAtt = SPNames.SP_MonthwiseDynamicAttendance;
            string strfilepath = "";
            // DataTable dtCloned = new DataTable();

            try
            {

                var con = _connectionString;
                await using (SqlConnection myConnection = new SqlConnection(con))
                {
                    SqlCommand objCmd = new SqlCommand(spMonthwiseAtt, myConnection);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(objCmd))
                    {
                        DataSet ds = new DataSet();
                        objCmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                        objCmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                        objCmd.Parameters.Add("@SectionId", SqlDbType.Int).Value = sectionId;

                        objCmd.CommandTimeout = 100000;
                        //SqlParameter activeStudentCountParam = new SqlParameter("@ActiveStudentCount", SqlDbType.Int);
                        //activeStudentCountParam.Direction = ParameterDirection.Output;
                        //objCmd.Parameters.Add(activeStudentCountParam);

                        da.Fill(ds);
                        //int activeStudentCount = (int)activeStudentCountParam.Value;
                        //var dataTable = ds.Tables[0];
                        // Create a new DataTable for the transformed data
                        //if (dataTable.Rows.Count == 0)
                        if (ds.Tables.Count == 0)
                            //return NotFound("No attendance records found.");
                            throw new Exception("No attendance records found.");
                        strfilepath = GenerateExcelSheet(ds.Tables[0],
                        ds.Tables[1],
                        ds.Tables[2],
                        "Daily Attendance Report- Grade:" + grade + "/ Section: " + section, month);
                    }

                }
                return strfilepath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_logger.LogError(ex.InnerException.ToString());

                return ex.Message;
            }
        }
        public async Task<string> generateAttendanceCumulativereport(int startYear, int endyear, int sectionId)
        {
            var spMonthwiseAtt = SPNames.SP_CumulativeAttendance;
            string strfilepath = "";
            // DataTable dtCloned = new DataTable();

            try
            {

                var con = _connectionString;
                await using (SqlConnection myConnection = new SqlConnection(con))
                {
                    SqlCommand objCmd = new SqlCommand(spMonthwiseAtt, myConnection);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    using (var da = new SqlDataAdapter(objCmd))
                    {
                        DataSet ds = new DataSet();
                        objCmd.Parameters.Add("@StartYear", SqlDbType.Int).Value = startYear;
                        objCmd.Parameters.Add("@EndYear", SqlDbType.Int).Value = endyear;

                        objCmd.Parameters.Add("@SectionId", SqlDbType.Int).Value = sectionId;

                        objCmd.CommandTimeout = 100000;
                        da.Fill(ds);
                        var dataTable = ds.Tables[0];
                        // Create a new DataTable for the transformed data
                        if (dataTable.Rows.Count == 0)
                            // return NotFound("No attendance records found.");
                            throw new Exception("No attendance records found.");
                        strfilepath = GenerateExcelReport(dataTable, "Attendance Cumulative Report");
                    }

                }
                return strfilepath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_logger.LogError(ex.InnerException.ToString());

                return ex.Message;
            }
        }
        private string GenerateExcelReport(DataTable dataTable, string reportname)
        {
            // Define folder and file paths
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string filePath = Path.Combine(folderPath, "CUMReport.xlsx");

            // Ensure directory exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Cumulative Attendance Report");

                // Title Row
                worksheet.Cell(1, 1).Value = "Sona Valliappa Public School";
                worksheet.Range(1, 1, 1, dataTable.Columns.Count).Merge().Style
                    .Font.SetBold()
                    .Font.SetFontSize(14)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Border.OutsideBorder = XLBorderStyleValues.Thick;

                // Report Name Row
                worksheet.Cell(2, 1).Value = reportname;
                worksheet.Range(2, 1, 2, dataTable.Columns.Count).Merge().Style
                    .Font.SetBold()
                    .Font.SetFontSize(14)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Border.OutsideBorder = XLBorderStyleValues.Thick;

                int headerRow = 3;
                int dataStartRow = 4;

                // Write headers
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    var headerCell = worksheet.Cell(headerRow, col + 1);
                    headerCell.Value = dataTable.Columns[col].ColumnName;
                    headerCell.Style.Font.Bold = true;
                    headerCell.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                // Write data rows
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        var value = dataTable.Rows[row][col];
                        var cell = worksheet.Cell(row + dataStartRow, col + 1);

                        if (value == DBNull.Value)
                            cell.Value = ""; // or "N/A"
                        else
                            cell.Value = value?.ToString() ?? string.Empty;


                    }
                }

                int totalColumns = dataTable.Columns.Count;
                int dataEndRow = dataStartRow + dataTable.Rows.Count - 1;

                // Apply table border
                var fullRange = worksheet.Range(headerRow, 1, dataEndRow, totalColumns);
                fullRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                fullRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Apply percentage format
                string[] percentCols = { "% for L.T.", "% for S.T.", "Overall %" };
                foreach (string colName in percentCols)
                {
                    if (dataTable.Columns.Contains(colName))
                    {
                        int colIndex = dataTable.Columns.IndexOf(colName) + 1;
                        var percentRange = worksheet.Range(dataStartRow, colIndex, dataEndRow, colIndex);
                        percentRange.Style.NumberFormat.Format = "0.00";
                        // Optional: highlight low attendance
                        percentRange.AddConditionalFormat()
                            .WhenLessThan(75)
                            .Fill.SetBackgroundColor(XLColor.LightPink);
                    }
                }

                // Signature section
                int signatureRowStart = dataEndRow + 1;
                int signatureRowEnd = signatureRowStart + 2;
                var signatureRange = worksheet.Range(signatureRowStart, 1, signatureRowEnd, totalColumns);
                signatureRange.Merge();
                signatureRange.Value = "Signature";
                signatureRange.Style
                    .Font.SetBold()
                    .Font.SetFontSize(15)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Bottom)
                    .Border.OutsideBorder = XLBorderStyleValues.Thick;

                // Outer border
                var fullOutlineRange = worksheet.Range(1, 1, signatureRowEnd, totalColumns);
                fullOutlineRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                // Auto fit
                worksheet.Columns().AdjustToContents();

                // Save the file
                workbook.SaveAs(filePath);
            }

            return filePath;
        }



        private string GenerateExcelSheet(DataTable dataTable, DataTable table2, DataTable table3,  string reportname, int month)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string filePath = Path.Combine(folderPath, "Report.xlsx");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (File.Exists(filePath))
                File.Delete(filePath);
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Attendance Report");
                int totalColumns = dataTable.Columns.Count;
                int totalRows = dataTable.Rows.Count;

                // 1. School Title
                // 1. School Title with separate thick border
                worksheet.Cell(1, 1).Value = "Sona Valliappa Public School";
                var schoolTitleRange = worksheet.Range(1, 1, 1, totalColumns);
                schoolTitleRange.Merge();
                schoolTitleRange.Style.Font.SetBold()
                                  .Font.SetFontSize(14)
                                  .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                  .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                // Apply full thick border to school title
                schoolTitleRange.Style.Border.TopBorder = XLBorderStyleValues.Thick;
                schoolTitleRange.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                schoolTitleRange.Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                schoolTitleRange.Style.Border.RightBorder = XLBorderStyleValues.Thick;


                // 2. Report Title with separate thick border
                worksheet.Cell(2, 1).Value = reportname;
                var reportTitleRange = worksheet.Range(2, 1, 2, totalColumns);
                reportTitleRange.Merge();
                reportTitleRange.Style.Font.SetBold()
                                   .Font.SetFontSize(13)
                                   .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                   .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                // Apply full thick border to report title
                reportTitleRange.Style.Border.TopBorder = XLBorderStyleValues.Thick;
                reportTitleRange.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                reportTitleRange.Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                reportTitleRange.Style.Border.RightBorder = XLBorderStyleValues.Thick;
                // 3. Column Headers
                int headerRow = 3;
                for (int col = 0; col < totalColumns; col++)
                {
                    worksheet.Cell(headerRow, col + 1).Value = dataTable.Columns[col].ColumnName;
                    worksheet.Cell(headerRow, col + 1).Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                }

                // 4. Attendance Data Rows
                int dataStartRow = headerRow + 1;

                for (int row = 0; row < totalRows; row++)
                {
                    for (int col = 0; col < totalColumns; col++)
                    {
                        worksheet.Cell(dataStartRow + row, col + 1).Value = dataTable.Rows[row][col]?.ToString();
                        worksheet.Cell(dataStartRow + row, col + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    }
                }

                int dataEndRow = dataStartRow + totalRows - 1;


                // 5. Number Present Daily & Initials of Teacher (below data)
                int summaryRowStart = dataEndRow + 1;

                // "Number Present Daily" - Column B
                worksheet.Cell(summaryRowStart, 1).Value = "Number Present Daily";
                worksheet.Range(summaryRowStart, 1, summaryRowStart + 1, 2).Merge().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Cell(summaryRowStart, 3).Value = "M";
                worksheet.Cell(summaryRowStart + 1, 3).Value = "E";
                // Add days as headers (1 to 31) starting from column 4
                int totalDays = table2.Rows.Count;
                //int totalDaysColumn = table2.Column.Count;
                int dayColumnStart = 4;
               
                for (int i = 0; i < totalDays; i++)
                {
                    int morningPresent = Convert.ToInt32(table2.Rows[i]["StudentsPresentMorning"]);
                    int eveningPresent = Convert.ToInt32(table2.Rows[i]["StudentsPresentEvening"]);

                    worksheet.Cell(summaryRowStart, dayColumnStart + i).Value = morningPresent;        // Morning row
                    worksheet.Cell(summaryRowStart + 1, dayColumnStart + i).Value = eveningPresent;    // Evening row
                }

                // Apply borders and vertical alignment
                worksheet.Range(summaryRowStart, dayColumnStart, summaryRowStart + 1, dayColumnStart + totalDays - 1)
                         .Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(summaryRowStart, dayColumnStart, summaryRowStart + 1, dayColumnStart + totalDays - 1)
                         .Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(summaryRowStart, dayColumnStart, summaryRowStart + 1, dayColumnStart + totalDays - 1)
                         .Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                summaryRowStart = summaryRowStart + 2;
                worksheet.Cell(summaryRowStart, 1).Value = "Initials Of Teachers";
                worksheet.Range(summaryRowStart, 1, summaryRowStart + 1, 2).Merge().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Cell(summaryRowStart, 3).Value = "M";
                worksheet.Cell(summaryRowStart + 1, 3).Value = "E";


                // Calculate total students
                int totalStudents = 0;
                int newJoinedCount = 0;
                int LeftStudnetCount = 0;
                if (table3.Rows.Count > 0)
                {
                    var row = table3.Rows[0];
                    totalStudents = Convert.ToInt32(row["EarlyStudentCount"]);
                    newJoinedCount = Convert.ToInt32(row["NewlyJoinedStudentCount"]);
                    LeftStudnetCount = Convert.ToInt32(row["LeftStudentCount"]);
                }

               
                int totalEnrolled = totalStudents + newJoinedCount - LeftStudnetCount;
               
                int dayColumn = 0;
                int dayColumnEnd = 0;

                // Dynamically find the range of day columns (1-31)
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    if (int.TryParse(dataTable.Columns[col].ColumnName, out int day))
                    {
                        if (dayColumn == 0) dayColumn = col + 1; // Set start column
                        dayColumnEnd = col + 1; // Continuously update the end column
                    }
                }

                // Calculate total working days
                int totalWorkingDays = 0;

                // Loop through each day column to determine working days
                for (int colIndex = dayColumn; colIndex <= dayColumnEnd; colIndex++)
                {
                    bool isWorkingDay = false;

                    // Check if any row has either 'P' or 'A' for the current day
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string attendanceMark = row[colIndex - 1]?.ToString()?.Trim().ToUpper();

                        if (attendanceMark == "P" || attendanceMark == "A")
                        {
                            isWorkingDay = true;
                            break; // No need to check further rows for this day
                        }
                    }

                    if (isWorkingDay)
                    {
                        totalWorkingDays++;
                    }
                }

                // Calculate total present count
                int totalPresentCount = 0;
                for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
                {
                    for (int colIndex = dayColumn - 1; colIndex < dayColumnEnd; colIndex++)
                    {
                        string attendanceMark = dataTable.Rows[rowIndex][colIndex]?.ToString()?.Trim().ToUpper();
                        if (attendanceMark == "P")
                        {
                            totalPresentCount++;
                        }
                    }
                }
                double averageNoOnRoll = totalStudents > 0 ? (double)totalPresentCount / totalStudents : 0;
                // Calculate average attendance
                double averageAttendance = totalWorkingDays > 0 ? (double)totalPresentCount / totalWorkingDays : 0;

                // 6. Monthly Summary Rows
                int rollSummaryStart = summaryRowStart + 2;
                worksheet.Cell(rollSummaryStart, 1).Value =
                $"No. on Roll at the beginning of Month: {totalStudents}  No. of School Days: {totalWorkingDays}   Average No. on Roll: {averageNoOnRoll:F2}";
                worksheet.Range(rollSummaryStart, 1, rollSummaryStart, totalColumns - 8).Merge();

                // Calculate the number of students admitted during the month from table3

                // Add the calculated value for "Admitted during the month" to the summary
                worksheet.Cell(rollSummaryStart + 1, 1).Value =
                    $"Admitted during the month: {newJoinedCount}   Left: {LeftStudnetCount}   Average Attendance: {averageAttendance:F2}   During Month: {monthName}";
                worksheet.Range(rollSummaryStart + 1, 1, rollSummaryStart + 1, totalColumns - 8).Merge();

                worksheet.Cell(rollSummaryStart + 2, 1).Value = $"No. on Roll at the end of: {totalEnrolled}     During Month: {monthName} ";

                worksheet.Range(rollSummaryStart + 2, 1, rollSummaryStart + 2, totalColumns - 8).Merge();

                // 7. Certification Box - aligned right
                int certifyStartCol = totalColumns - 7;
                var certifyRange = worksheet.Range(rollSummaryStart, certifyStartCol, rollSummaryStart + 2, totalColumns);
                certifyRange.Merge();
                certifyRange.Value = "Certified attendance marked is correct\nH. M. / Teacher";
                certifyRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                certifyRange.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                certifyRange.Style.Alignment.SetWrapText(true);
                certifyRange.Style.Font.SetBold();
                certifyRange.Style.Font.SetFontSize(8);

                // 8. Adjustments
                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();

                worksheet.RangeUsed().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.RangeUsed().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                // 9. Apply thick outside border around the entire document content

                var usedRange = worksheet.RangeUsed();
                usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                usedRange.Style.Border.InsideBorderColor = XLColor.Black;
                usedRange.Style.Border.OutsideBorderColor = XLColor.Black;


                workbook.SaveAs(filePath);
            }

            return filePath;
        }


        public string generateAttendancedynamicreport(string Sem, string Year, int Department, string Section)
        {
            try
            {
                var spName = SPNames.SP_MonthwiseDynamicAttendance;
                //string strfilepath = _appSettings.Settings.DownloadPath.ToString() + "\\" + _appSettings.Settings.FileName.ToString();
                // Define the file name
                string fileName = "Report.xlsx";

                // Get the current directory and build full path
                string currentDirectory = Directory.GetCurrentDirectory();
                string strfilepath = Path.Combine(currentDirectory, "Downloads", fileName);

                // Ensure the directory exists
                string dirPath = Path.GetDirectoryName(strfilepath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                DataTable dtTable = new DataTable();
                var con = _connectionString;
                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    SqlCommand objCmd = new SqlCommand(spName, myConnection);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    //objCmd.Parameters.Add("@noofDays", SqlDbType.BigInt).Value = Noofdays;
                    using (var da = new SqlDataAdapter(objCmd))
                    {
                        objCmd.Parameters.Add("@Sem", SqlDbType.VarChar).Value = Sem;
                        objCmd.Parameters.Add("@year", SqlDbType.VarChar).Value = Year;
                        objCmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = Convert.ToString(Department);
                        objCmd.Parameters.Add("@Section", SqlDbType.VarChar).Value = Section;
                        da.Fill(dtTable);
                    }
                }


                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtTable, "Attendance_Dynamic_Report").Columns().AdjustToContents();

                    if (File.Exists(strfilepath))
                    {
                        File.Delete(strfilepath);
                    }
                    //lblerror.Text = "three";
                    wb.SaveAs(strfilepath); // (filepath, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }

                return strfilepath;
                //}
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
