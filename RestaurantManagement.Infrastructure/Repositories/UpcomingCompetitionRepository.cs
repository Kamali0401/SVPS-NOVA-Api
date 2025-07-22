using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System.Data;
using Dapper;
using SonaNova.Domain.Entities;
using System.Reflection;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ClosedXML.Excel;

namespace RestaurantManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class UpcomingCompetitionRepository : IUpcomingCompetitionRepository
    {
        private readonly IDataBaseConnection _db;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingCompetitionRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public UpcomingCompetitionRepository(IDataBaseConnection db, IConfiguration configuration)
        {
            _db = db;
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public async Task<IEnumerable<UpcomingCompetition>> GetUpcomingCompetition(string role, int? id)
        {
            var spName = SPNames.SP_GETALLUPCOMINGCOMPETITION; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<UpcomingCompetition>(spName,
                new { Role = role, Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<UpcomingCompetition>> GetUpcomingCompetitionbyId(int? id)
        {
            var spName = SPNames.SP_GETUPCOMINGCOMPETITIONBYID; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<UpcomingCompetition>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<UpcomingCompetition> InsertUpcomingCompetition(UpcomingCompetition model)
        {
            var spName = SPNames.SP_INSERTUPCOMINGCOMPETITION; // Name of your stored procedure
                                                                 // Define parameters for the stored procedure
            
            

            var parameters = new
            {

                //Id = model.Id,
                EventDate = model.EventDate,
                EventName = model.EventName,
                EventDay = model.EventDay,
                Grade = model.Grade,
                EventTiming = model.EventTiming,
                Eligibility = model.Eligibility,
                Guidelines = model.Guidelines,
                TimeLimit = model.TimeLimit,
                JudgingCriteria = model.JudgingCriteria,
                DressCode = model.DressCode,
                IsPollingRequired = model.IsPollingRequired,
                PollingEndDate = model.PollingEndDate,
                CreatedBy = model.CreatedBy
            };

            // Execute the stored procedure and retrieve the inserted data
            /*await _db.Connection.QuerySingleOrDefaultAsync<UpcomingCompetition>(
               spName,
               parameters,
               commandType: CommandType.StoredProcedure
           );*/
            int? newStudentId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
                  spName,
                  parameters,
                  commandType: CommandType.StoredProcedure
               );


            if (newStudentId.HasValue)
            {
                model.Id = newStudentId.Value;
            }



            return model;


        }
        /// <inheritdoc/>
        public async Task UpdateUpcomingCompetition(UpcomingCompetition model)
        {
            var spName = SPNames.SP_UPDATEUPCOMINGCOMPETITION; // Update the stored procedure name if necessary
            

            var parameters = new
            {
                Id = model.Id,
                EventDate = model.EventDate,
                EventName = model.EventName,
                EventDay = model.EventDay,
                Grade = model.Grade,
                EventTiming = model.EventTiming,
                Eligibility = model.Eligibility,
                Guidelines = model.Guidelines,
                TimeLimit = model.TimeLimit,
                JudgingCriteria = model.JudgingCriteria,
                DressCode = model.DressCode,
                IsPollingRequired = model.IsPollingRequired,
                PollingEndDate = model.PollingEndDate,
                ModifiedBy = model.ModifiedBy

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteUpcomingCompetitionDetails(int id)
        {
            var spName = SPNames.SP_DELETEUPCOMINGCOMPETITION; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }
        public async Task<IEnumerable<UpcomingCompetition>> UpdateInterestedCompetition(int studentId, int competitionId)
        {
            var spName = SPNames.SP_UPDATEINTERESTED;

            var result = await _db.Connection.QueryAsync<UpcomingCompetition>(
                spName,
                new
                {
                    StudentId = studentId,
                    CompetitionId = competitionId
                },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<string> GetInterestedStudentList(int competitionId)
        {
            try
            {
                string spName = SPNames.SP_INTERESTEDSTUDENTLIST;


                string connectionString = _connectionString;

                await using (var connection = new SqlConnection(connectionString))
                await using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@CompetitionId", SqlDbType.Int).Value = competitionId;
                    //command.CommandTimeout = 100000;

                    using (var adapter = new SqlDataAdapter(command))
                    using (var ds = new DataSet())
                    {
                        adapter.Fill(ds);
                        DataTable studentTable = ds.Tables[0];
                        return GenerateExcelusingDatatable(studentTable, "Competition");
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error generating mark report");
                return ex.Message;
            }
        }
        private string GenerateExcelusingDatatable(DataTable studentTable, string activityName)
        {
            try
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
                string filePath = Path.Combine(folderPath, "Report.xlsx");

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Competition");
                    int colCount = 1, rowCount = 1;

                    string eventValue = studentTable.Rows[0]["EventName"].ToString();
                    if (studentTable.Columns.Contains("EventName"))
                    {
                        studentTable.Columns.Remove("EventName");
                    }

                    int colMaxWidth = studentTable.Columns.Count;

                    // === Title Row: School Name ===
                    var titleRange = ws.Range(rowCount, colCount, rowCount, colMaxWidth);
                    titleRange.Merge();
                    titleRange.Value = "SONA VALLIAPPA PUBLIC SCHOOL";
                    titleRange.Style.Font.Bold = true;
                    titleRange.Style.Font.FontSize = 14;
                    titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    titleRange.Style.Border.TopBorder = XLBorderStyleValues.Thick;
                    titleRange.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    titleRange.Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                    titleRange.Style.Border.RightBorder = XLBorderStyleValues.Thick;
                    rowCount++;

                    // === Subtitle Row: Event Name - STUDENT LIST ===
                    var subtitleRange = ws.Range(rowCount, colCount, rowCount, colMaxWidth);
                    subtitleRange.Merge();
                    subtitleRange.Value = $"{eventValue} - STUDENT LIST".ToUpper();
                    subtitleRange.Style.Font.Bold = true;
                    subtitleRange.Style.Font.FontSize = 12;
                    subtitleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    subtitleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    subtitleRange.Style.Border.TopBorder = XLBorderStyleValues.Thick;
                    subtitleRange.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                    subtitleRange.Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                    subtitleRange.Style.Border.RightBorder = XLBorderStyleValues.Thick;
                    rowCount++;

                    // === Column Headers ===
                    var headers = new List<string> {
                "S.No", "Adminssion No", "Name of Student", "Grade", "Section", "Father Mobile No", "Polling Status"
            };

                    for (int i = 0; i < headers.Count; i++)
                    {
                        var cell = ws.Cell(rowCount, i + 1);
                        cell.Value = headers[i];
                        cell.Style.Font.Bold = true;
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        cell.Style.Border.TopBorder = XLBorderStyleValues.Thick;
                        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        cell.Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                        cell.Style.Border.RightBorder = XLBorderStyleValues.Thick;

                        ws.Column(i + 1).AdjustToContents();
                    }
                    rowCount++;

                    // === Insert Student Data ===
                    ws.Cell(rowCount, 1).InsertData(studentTable);

                    // === Apply Style to Data Rows ===
                    var dataStartRow = rowCount;
                    var totalRows = studentTable.Rows.Count;
                    var totalCols = colMaxWidth;

                    for (int i = 0; i < totalRows; i++)
                    {
                        for (int j = 0; j < totalCols; j++)
                        {
                            var cell = ws.Cell(dataStartRow + i, j + 1);
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        }
                    }

                    var fullRange = ws.Range(1, 1, rowCount + totalRows - 1, colMaxWidth);
                    fullRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                    // === Save File ===
                    if (File.Exists(filePath)) File.Delete(filePath);
                    wb.SaveAs(filePath);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
