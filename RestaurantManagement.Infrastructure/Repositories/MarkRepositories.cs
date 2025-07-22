using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SonaNova.Domain.Entities;
using System.Collections;
using SonaNova.Infrastructure.Interfaces;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ClosedXML.Excel;

namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class MarkRepository : IMarkRepository
    {
        private readonly IDataBaseConnection _db;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public MarkRepository(IDataBaseConnection db, IConfiguration configuration)
        {
            _db = db;
            _connectionString = configuration.GetConnectionString("DbConnection");
        }
        public async Task<IEnumerable<Mark>> GetStudentMark()
        {
            var spName = SPNames.SP_GETSTUDENTMARKS;
            var studentMarks = new List<Mark>();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                studentMarks.Add(new Mark
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    StudentId = reader["StudentId"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    Section = Convert.ToInt32(reader["Section"]),
                    Data = reader["Data"].ToString(),
                    IsattendanceRequired = Convert.ToBoolean(reader["IsattendanceRequired"]),
                    ReadytosendEmail = Convert.ToBoolean(reader["ReadytosendEmail"])
                });
            }

            return studentMarks; // ✅ return type matches Task<IEnumerable<Mark>>
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Mark>> GetStudentMarkById(int? id)
        {
            var spName = SPNames.SP_GETSTUDENTMARKBYID; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Mark>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        public async Task<IEnumerable<Mark>> GetStudentMarkByStudentId(int studentId)
        {
            var spName = SPNames.SP_GETSTUDENTMARKSBYSTUDENT; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Mark>(spName,
                new { StudentId = studentId, }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Mark> InsertMarkDetails(Mark studentmark)
        {
            var spName = SPNames.SP_INSERTSTUDENTMARKS; // Name of your stored procedure
                                                // Define parameters for the stored procedure
            var parameters = new
            {
                StudentId = studentmark.StudentId,
                StudentName = studentmark.StudentName,
                Section = studentmark.Section,
                Data = studentmark.Data,
                IsattendanceRequired = studentmark.IsattendanceRequired,
                ReadytosendEmail = studentmark.ReadytosendEmail,
                createdby = studentmark.createdby,

            };

            // Execute the stored procedure and retrieve the inserted data
            await _db.Connection.QuerySingleOrDefaultAsync<Mark>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );

            return studentmark;


        }
        /// <inheritdoc/>
      /*  public async Task UpdateMarkDetails(Mark Mark)
        {
            var spName = SPNames.SP_UPDATEMark; // Update the stored procedure name if necessary


            var parameters = new
            {
                Id = Mark.Id,
                Name = Mark.Name,
                Is_Active = Mark.Is_Active,
                ModifiedBy = Mark.ModifiedBy,

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }*/

        
        public async Task<string> DeleteMarkDetails(List<Mark> mark)
        {
            var spName = SPNames.SP_DELETEMARK;
            var sendToDB = new ArrayList();
            try
            {
                foreach (var item in mark)
                {
                    sendToDB.Add(
                        new
                        {
                            Id = item.Id
                        });
                }
                var delte = await _db.Connection.ExecuteAsync(spName, sendToDB.ToArray(),
                    commandType: CommandType.StoredProcedure);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<Mark>> UpdateReadytosendEmail(bool ReadytosendEmail)
        {
            var spName = SPNames.SP_UPDATEEMAIL; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Mark>(spName,
                new { ReadytosendEmail = ReadytosendEmail }, commandType: CommandType.StoredProcedure).ToList());
        }


        public async Task<string> GetAllMarkReport(string section, string subjects, string test)
        {
            try
            {
                string spName = SPNames.SP_MARKTEMPLATE;
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
                string filePath = Path.Combine(folderPath, "Report.xlsx");
                string[] subjectArr = subjects.Split(',');
                string[] secArr = section.Split('-');
                string connectionString = _connectionString;

                await using (var connection = new SqlConnection(connectionString))
                await using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@SectionId", SqlDbType.Int).Value = Convert.ToInt64(secArr[2]);
                    command.CommandTimeout = 100000;

                    using (var adapter = new SqlDataAdapter(command))
                    using (var ds = new DataSet())
                    {
                        adapter.Fill(ds);
                        DataTable studentTable = ds.Tables[0];
                        return GenerateExcelReport(studentTable, secArr, subjectArr, test, filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error generating mark report");
                return ex.Message;
            }
        }
        private string GenerateExcelReport(DataTable studentTable, string[] secArr, string[] subjectArr, string test, string filePath)
        {
            try
            {
                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Mark Template");
                    int colCount = 1, rowCount = 1;
                    int colMaxWidth = subjectArr.Length + 3;
                    int colPart = colMaxWidth / 2;

                    // Headers
                    ws.Cell(rowCount++, colCount).Value = "SONA VALLIAPPA PUBLIC SCHOOL";
                    ws.Range(rowCount - 1, colCount, rowCount - 1, colMaxWidth).Merge().AddToNamed("Titles");

                    ws.Cell(rowCount++, colCount).Value = $"STUDENT MARK REPORT-{test.ToUpper()}";
                    ws.Range(rowCount - 1, colCount, rowCount - 1, colMaxWidth).Merge().AddToNamed("Titles");

                    ws.Cell(rowCount, colCount).Value = "GRADE: " + secArr[0].ToUpper();
                    ws.Range(rowCount, colCount, rowCount, colPart).Merge().AddToNamed("Titles");

                    ws.Cell(rowCount++, colPart + 1).Value = "SECTION: " + secArr[1].ToUpper();
                    ws.Range(rowCount - 1, colPart + 1, rowCount - 1, colMaxWidth).Merge().AddToNamed("Titles");

                    // Column headers
                    var headers = new List<string> { "SNo", "Reg.No", "Name of Student" };
                    headers.AddRange(subjectArr);
                    for (int i = 0; i < headers.Count; i++)
                    {
                        ws.Cell(rowCount, i + 1).Value = headers[i];
                        ws.Column(i + 1).AdjustToContents().AddToNamed("Titles");
                    }
                    rowCount++;

                    ws.Cell(rowCount++, 1).Value = "Date of Examination";
                    ws.Range(rowCount - 1, 1, rowCount - 1, 3).Merge().AddToNamed("Titles");

                    // Insert Data
                    ws.Cell(rowCount, 1).InsertData(studentTable).AddToNamed("Titles");

                    ApplyStyles(wb, ws, studentTable.Rows.Count, colMaxWidth);

                    if (File.Exists(filePath)) File.Delete(filePath);
                    wb.SaveAs(filePath);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error generating Excel report");
                return ex.Message;
            }
        }
        private void ApplyStyles(XLWorkbook wb, IXLWorksheet ws, int studentCount, int colMaxWidth)
        {
            var style = wb.Style;
            style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            style.Border.InsideBorder = XLBorderStyleValues.Thin;
            style.Font.FontSize = 10;

            ws.Range(ws.Cell(1, 1), ws.Cell(studentCount + 1, colMaxWidth)).Style = style;
        }
       
    }

   }
