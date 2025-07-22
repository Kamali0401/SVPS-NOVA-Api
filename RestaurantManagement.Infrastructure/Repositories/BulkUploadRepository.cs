using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Configuration;



using SonaNova.Infrastructure.Interfaces;
using ClosedXML.Excel;
namespace SonaNova.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class BulkUploadRepository : IBulkuploadRepository
    {
        private readonly IDataBaseConnection _db;
        private readonly string _connectionString;
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkUploadRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        /*public BulkUploadRepository(IDataBaseConnection db)
        {
            this._db = db;
        }
        private readonly string _connectionString;

        public BulkUploadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }*/
        public BulkUploadRepository(IDataBaseConnection db, IConfiguration configuration)
        {
            _db = db;
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        /// <inheritdoc/>
        public async Task<string> bulkuploadmark(string target, string section)
        {
            var testtype = "";
            //bool? IsAttandanceRequired=null;
            var AttandanceRequired = "";
            DateTime today = DateTime.Now;

            DataTable dt = new DataTable();
            DataColumn dtColumn;
            dtColumn = new DataColumn();

            dtColumn.DataType = typeof(Int32);
            dtColumn.ColumnName = "Id";
            dtColumn.Caption = "Id";
            dtColumn.AutoIncrement = true;
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "StudentId";
            dtColumn.Caption = "StudentId";
            dtColumn.Unique = true;
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "StudentName";
            dtColumn.Caption = "StudentName";
            dt.Columns.Add(dtColumn);



            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Int32);
            dtColumn.ColumnName = "Section";
            dtColumn.Caption = "Section";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "TestType";
            dtColumn.Caption = "TestType";
            dt.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "Data";
            dtColumn.Caption = "Data";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "PreviousMonthAttendance";
            dtColumn.Caption = "PreviousMonthAttendance";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "IsattendanceRequired";
            dtColumn.Caption = "IsattendanceRequired";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "ReadytosendEmail";
            dtColumn.Caption = "ReadytosendEmail";
            dt.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "IsParentIntemated";
            dtColumn.Caption = "IsParentIntemated";
            dt.Columns.Add(dtColumn);



            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "CreatedDate";
            dtColumn.Caption = "CreatedDate";
            dt.Columns.Add(dtColumn);



            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "createdby";
            dtColumn.Caption = "createdby";
            dt.Columns.Add(dtColumn);
            DataRow dtRow;
            List<string> subject = new List<string>();
            List<string> dateofexam = new List<string>();
            //string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", "C:\\Users\\HP-LD\\Downloads\\Excel Files\\Excel Files\\Student.xslx");
            using (XLWorkbook workBook = new XLWorkbook(target))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                var columncount = workBook.Worksheet(1).LastColumnUsed().ColumnNumber();

                // bool fouthRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    if (row.RangeAddress.FirstAddress.RowNumber == 2)
                    {
                        testtype = row.Cell(1).Value.ToString().Split('-').Last();
                    }

                    // IsAttandanceRequired = AttandanceRequired == "Yes" ? true : false;
                    if (row.RangeAddress.FirstAddress.RowNumber == 4)
                    {
                        for (int i = 4; i <= 11; i++)
                        {
                            if (row.Cell(i).Value.ToString() != "")
                            {
                                subject.Add(row.Cell(i).Value.ToString().Split('-').Last());
                            }
                        }
                    }
                    if (row.RangeAddress.FirstAddress.RowNumber == 5)
                    {
                        for (int i = 4; i <= 11; i++)
                        {
                            if (row.Cell(i).Value.ToString() != "")
                            {
                                dateofexam.Add(row.Cell(i).Value.ToString().Split(" ").First());
                            }
                        }
                    }
                    //Use the first row to add columns to DataTable.
                    if (row.RangeAddress.FirstAddress.RowNumber > 5)
                    {
                        string data = "";
                        for (int i = 0; i < subject.Count; i++)
                        {
                            data += subject[i] + "-" + row.Cell(i + 4).Value + " ||  Date : " + dateofexam[i] + ",";
                        }
                        //Add rows to DataTable.
                        dtRow = dt.NewRow();
                        dtRow["Id"] = 1;
                        dtRow["StudentId"] = row.Cell(2).Value.ToString();
                        dtRow["StudentName"] = row.Cell(3).Value.ToString();
                        dtRow["Section"] = Convert.ToInt32(section.Split('-')[2]);
                        dtRow["TestType"] = testtype.ToLower();
                        dtRow["Data"] = data;
                        dtRow["PreviousMonthAttendance"] = "-";
                        dtRow["IsattendanceRequired"] = false;
                        dtRow["ReadytosendEmail"] = 0;
                        dtRow["IsParentIntemated"] = 0;
                        dtRow["CreatedDate"] = today;
                        dtRow["createdby"] = "Admin";
                        dt.Rows.Add(dtRow);
                    }
                }
            }
            // DataTable Exceldt = dt.Tables[0]; //copy data set to datatable
            using (SqlBulkCopy bulkCopy =
                   new SqlBulkCopy(_connectionString))
            {
                var tblName = SPNames.TBL_STUDENTMARKS;
                bulkCopy.DestinationTableName = tblName;
                try
                {
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dt);
                    var sendToDB = new ArrayList();

                    return "Uploaded Successfully";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //_logger.LogError(ex.InnerException.ToString());
                    return ex.Message;
                }
            }
        }

        public async Task<string> bulkuploadstudent(DataTable target)
        {
            try
            {
                var spName = SPNames.SP_BULKSTUDENTUPLOAD;
                using SqlConnection sqlConnection = new(_db.Connection.ConnectionString);
                await sqlConnection.OpenAsync(); // Await this!

                using SqlCommand command = new(spName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@StudentTable", SqlDbType.Structured).Value = target;

                SqlParameter returnStatusParam = command.Parameters.Add("@UploadStatus", SqlDbType.NVarChar, 50);
                returnStatusParam.Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync(); // Await this!

                return returnStatusParam.Value?.ToString() ?? string.Empty;
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        public async Task<string> bulkuploadfaculty(DataTable target)
        {

            try
            {
                var spName = SPNames.SP_BULKFACULTYUPLOAD;
                using SqlConnection sqlConnection = new(_db.Connection.ConnectionString);
                await sqlConnection.OpenAsync(); // Await this!

                using SqlCommand command = new(spName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FacultyTable", SqlDbType.Structured).Value = target;

                SqlParameter returnStatusParam = command.Parameters.Add("@UploadStatus", SqlDbType.NVarChar, 50);
                returnStatusParam.Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync(); // Await this!

                return returnStatusParam.Value?.ToString() ?? string.Empty;
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }


        }

        public async Task<string> bulkuploadsubject(DataTable target)
        {
            try
            {
                var spName = SPNames.SP_BULKSUBJECTUPLOAD;
                using SqlConnection sqlConnection = new(_db.Connection.ConnectionString);
                await sqlConnection.OpenAsync();

                using SqlCommand command = new(spName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@SubjectTable", SqlDbType.Structured).Value = target;

                SqlParameter returnStatusParam = command.Parameters.Add("@UploadStatus", SqlDbType.NVarChar, 50);
                returnStatusParam.Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync(); // Await this!

                return returnStatusParam.Value?.ToString() ?? string.Empty;
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public async Task<string> bulkuploadtimetable(DataTable target)
        {
            try
            {
                var spName = SPNames.SP_BULKTIMETABLEUPLOAD;
                using SqlConnection sqlConnection = new(_db.Connection.ConnectionString);
                await sqlConnection.OpenAsync();

                using SqlCommand command = new(spName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TimetableTable", SqlDbType.Structured).Value = target;

                SqlParameter returnStatusParam = command.Parameters.Add("@UploadStatus", SqlDbType.NVarChar, 50);
                returnStatusParam.Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync(); // Await this!

                return returnStatusParam.Value?.ToString() ?? string.Empty;
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<string> bulkuploadholidaycalendar(DataTable target)
        {
            try
            {
                var spName = SPNames.SP_BULKHOLIDAYUPLOAD;
                using SqlConnection sqlConnection = new(_db.Connection.ConnectionString);
                await sqlConnection.OpenAsync();

                using SqlCommand command = new(spName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@HolidayTable", SqlDbType.Structured).Value = target;

                SqlParameter returnStatusParam = command.Parameters.Add("@UploadStatus", SqlDbType.NVarChar, 50);
                returnStatusParam.Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync(); // Await this!

                return returnStatusParam.Value?.ToString() ?? string.Empty;
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public async Task<string> bulkuploadacademiccalendar(DataTable target)
        {
            try
            {
                var spName = SPNames.SP_BULKACADEMICUPLOAD;
                using SqlConnection sqlConnection = new(_db.Connection.ConnectionString);
                await sqlConnection.OpenAsync();

                using SqlCommand command = new(spName, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@AcademicCalendarTable", SqlDbType.Structured).Value = target;

                SqlParameter returnStatusParam = command.Parameters.Add("@UploadStatus", SqlDbType.NVarChar, 50);
                returnStatusParam.Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync(); // Await this!

                return returnStatusParam.Value?.ToString() ?? string.Empty;
            }
            catch (SqlException ex)
            {
                return "SQL Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

    }
}
