using Dapper;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RestaurantManagement.Infrastructure.Constants;
using SonaNova.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace SonaNova.Infrastructure.Repositories
{/// <summary>
 /// Repository class for performing CRUD operations on bill.
 /// </summary>
    public class AttendanceRepository: IAttendanceRepository
    {

        
        
            private readonly IDataBaseConnection _db;
        private readonly string _connectionString;
        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingCompetitionRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public AttendanceRepository(IDataBaseConnection db, IConfiguration configuration)
        {
            _db = db;
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public async Task<IEnumerable<Attendance>> GetAttendanceDetails(DateTime? AttendanceDate, int sectionId, string Hoursday)
        {
            var spName = SPNames.SP_GETALLATTENDANCE;
            return await  Task.Factory.StartNew(() => _db.Connection.Query<Attendance>(spName, new
            {
                AttendanceDate = AttendanceDate,
                SectionId = sectionId,
                Hoursday = Hoursday

            }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async  Task<IEnumerable<Attendance>> GetAttendanceById(int? id)
        {
            var spName = SPNames.SP_GETALLATTENDANCEBYID;
            return  await Task.Factory.StartNew(() => _db.Connection.Query<Attendance>(spName, 
              new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }
        public async Task<IEnumerable<StudentAttendanceModel>> GetAttendanceByStudentId(int studentId, int month, int year)
        {
            var spName = SPNames.SP_GETALLATTENDANCEBYSTUDENTID;
            using SqlConnection sqlconnection = new SqlConnection(_connectionString);
            sqlconnection.Open();
            SqlCommand command = new SqlCommand(spName, sqlconnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@StudentId", studentId));
            command.Parameters.Add(new SqlParameter("@Month", month));
            command.Parameters.Add(new SqlParameter("@Year", year));

            //_db.Connection.Open();
            using (var result = command.ExecuteReader())
            {
                var entities = new List<StudentAttendanceModel>();
                while (result.Read())
                {
                    var attendanceRecord = new StudentAttendanceModel
                    {
                        StudentId = result.GetInt64(result.GetOrdinal("StudentId")),
                        StudentName = result.GetString(result.GetOrdinal("StudentName")),
                        AttendanceRecords = new Dictionary<string, string>()
                    };

                    // Handle dynamic columns
                    for (int i = 2; i < result.FieldCount; i++) // Start from 5 assuming the first 4 columns are fixed
                    {
                        attendanceRecord.AttendanceRecords.Add(result.GetName(i), result.IsDBNull(i) ? "N" : result.GetString(i));
                    }

                    entities.Add(attendanceRecord);
                }
                //  _db.Connection.Close();
                return entities;
            }
        }

        public async Task<string> InsertAttendanceDetails(List<Attendance> attendance)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;
            await using (XmlWriter writer = XmlWriter.Create("text.xml", settings))
            {
                writer.WriteStartElement("Paramters");
                if (attendance != null)
                {
                    for (int i = 0; i < attendance.Count; i++)
                    {
                        var hours = attendance[i].Hoursdays.Split(',');
                        for (int j = 0; j < hours.Length; j++)
                        {
                            writer.WriteStartElement("Param");
                            writer.WriteElementString("StudentId", attendance[i].StudentId.ToString());
                            writer.WriteElementString("SectionId", attendance[i].SectionId.ToString());
                            writer.WriteElementString("Date", attendance[i].Date.ToString("dd/MM/yyyy"));
                            writer.WriteElementString("IsPresent", attendance[i].IsPresent.ToString());
                            writer.WriteElementString("Hoursday", hours[j].ToString());
                            writer.WriteElementString("CreatedBy", attendance[i].CreatedBy.ToString());
                            writer.WriteElementString("CreatedDate", attendance[i].CreatedDate.ToString());
                            writer.WriteEndElement();
                        }

                    }

                    writer.WriteEndElement();
                    writer.Close();
                    writer.Flush();
                    writer.Dispose();
                }

            }

            XmlReader xmlReader = new XmlTextReader("text.xml");
            string xml = File.ReadAllText("text.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmlReader);
            xmlReader.Close();
            var spName = SPNames.SP_INSERTATTENDANCE;
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(xml);

            await using (SqlConnection sqlconnection =
                   new SqlConnection(_connectionString))
            {
                sqlconnection.Open();


                SqlCommand command = new SqlCommand(spName, sqlconnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@xml", SqlDbType.VarChar).Value = xml;
                command.CommandTimeout = 100000;
                command.ExecuteNonQuery();
                return "Success";
            }
        }

        public async Task<List<Attendance>> UpdateAttendanceDetails(Attendance attendance)
        {
            var spName = SPNames.SP_UPDATEATTENDANCE;
            return await Task.Factory.StartNew(() => _db.Connection.Query<Attendance>(spName, new
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                SectionId = attendance.SectionId,
                Date = attendance.Date,
                IsPresent = attendance.IsPresent,
                ModifiedBy = attendance.ModifiedBy,
                ModifiedDate = attendance.ModifiedDate

            }, commandType: CommandType.StoredProcedure).ToList());

        }



        public async Task<string> DeleteAttendanceDetails(List<Attendance> attendance)
        {
            var spName = SPNames.SP_DELETEATTENDANCE;
            var sendToDB = new ArrayList();
            try
            {
                foreach (var item in attendance)
                {
                    sendToDB.Add(
                        new
                        {
                            SectionId = item.SectionId,
                            StudentId = item.StudentId,
                            Hoursday = item.Hoursday,
                            Date = item.Date.ToString("yyyy-MM-dd")
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

    }

}
