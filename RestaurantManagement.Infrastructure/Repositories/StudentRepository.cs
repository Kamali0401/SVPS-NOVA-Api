using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System.Data;
using Dapper;
using System.Reflection;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text.Json;
using SonaNova.Domain.Entities;
using DocumentFormat.OpenXml.Wordprocessing;

namespace RestaurantManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on bill.
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public StudentRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Students>> GetStudentDetails(int? id)
        {
            var spName = SPNames.SP_GETSTUDENT; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Students>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }


        public async Task<Students> InsertStudent(Students studentDetails)
        {
            var spNameInsertOrderDetails = SPNames.SP_INSERTSTUDENT; // Name of your stored procedure
                                                                         // Define parameters for the stored procedure
          //  var spNameInsertOrderItems = SPNames.SP_INSERTORDERDETAIL;
           // OrderDetail insertedData = new OrderDetail();
          //  string orderItemsJson = JsonSerializer.Serialize(orderDetails.ItemDetails);
         //   var sendToDB = new ArrayList();
         
                var parameters = new
                {
                    AdmissionNumber = studentDetails.AdmissionNumber,
                    Student_FirstName = studentDetails.Student_FirstName,
                    Student_MiddleName = studentDetails.Student_MiddleName,
                    Student_LastName = studentDetails.Student_LastName,
                    AllergicTo = studentDetails.AllergicTo,
                    HouseId = studentDetails.HouseId,
                    BloodGroup = studentDetails.BloodGroup,
                    Gender = studentDetails.Gender,
                    Dob = studentDetails.Dob,
                    DOJ = studentDetails.DOJ,
                    DOL = studentDetails.DOL,
                    CommunicationAddress = studentDetails.CommunicationAddress,
                    PermanentAddress = studentDetails.PermanentAddress,
                    Student_AadhaarNumber = studentDetails.Student_AadhaarNumber,
                    ParentEmailId = studentDetails.ParentEmailId,
                    Photo = studentDetails.Photo,
                    FatherName = studentDetails.FatherName,
                    Father_MobileNumber = studentDetails.Father_MobileNumber,
                    Father_Photo = studentDetails.Father_Photo,
                    MotherName = studentDetails.MotherName,
                    Mother_MobileNumber = studentDetails.Mother_MobileNumber,
                    Mother_Photo = studentDetails.Mother_Photo,
                    Gardian1Name = studentDetails.Gardian1Name,
                    Gardian1MobileNumber = studentDetails.Gardian1MobileNumber,
                    Gardian1Photo = studentDetails.Gardian1Photo,
                    Gardian2Name = studentDetails.Gardian2Name,
                    Gardian2MobileNumber = studentDetails.Gardian2MobileNumber,
                    Gardian2Photo = studentDetails.Gardian2Photo,

                    Gardian3Name = studentDetails.Gardian3Name,
                    Gardian3MobileNumber = studentDetails.Gardian3MobileNumber,
                    Gardian3Photo = studentDetails.Gardian3Photo,

                    Gardian4Name = studentDetails.Gardian4Name,
                    Gardian4MobileNumber = studentDetails.Gardian4MobileNumber,
                    Gardian4Photo = studentDetails.Gardian4Photo,


                    CreatedBy = studentDetails.CreatedBy,
                };

            int? newStudentId = await _db.Connection.QuerySingleOrDefaultAsync<int?>(
                   spNameInsertOrderDetails,
                   parameters,
                   commandType: CommandType.StoredProcedure
                );
           

            if (newStudentId.HasValue)
            {
                studentDetails.Id = newStudentId.Value;
            }


            //orderDetails.OrderId = insertedData??0;



            //foreach (var item in orderDetails.ItemDetails)
            //{
            //    sendToDB.Add(
            //        new
            //        {
            //            OrderId = orderDetails?.OrderId,
            //            ItemId = item.ItemId,
            //            Qty = item.Qty,
            //            Price = item.Price,
            //            IsSave = item.IsSave,
            //            IsSavePrint=item.IsSavePrint,
            //            IsSaveEBill=item.IsSaveEBill,
            //            IsHold=item.IsHold,
            //            IsKOT=item.IsKOT,
            //            IsKOTPrint=item.IsKOTPrint,
            //            IsFoodReceived=item.IsFoodReceived,
            //            CreatedBy = item.CreatedBy,

            //        });

            //}
            // await Task.Factory.StartNew(() =>
            //    _db.Connection.Execute(spNameInsertOrderItems, sendToDB.ToArray(), commandType: CommandType.StoredProcedure));

            return studentDetails;


        }
        /// <inheritdoc/>
        public async Task UpdateStudent(Students studentDetails)
        {
            var spName = SPNames.SP_UPDATESTUDENT; // Update the stored procedure name if necessary
            

            var parameters = new
            {
                Id = studentDetails.Id,
                AdmissionNumber = studentDetails.AdmissionNumber,
                Student_FirstName = studentDetails.Student_FirstName,
                Student_MiddleName = studentDetails.Student_MiddleName,
                Student_LastName = studentDetails.Student_LastName,


                AllergicTo = studentDetails.AllergicTo,
                HouseId = studentDetails.HouseId,
                BloodGroup = studentDetails.BloodGroup,
                Gender = studentDetails.Gender,
                Dob = studentDetails.Dob,
                DOJ = studentDetails.DOJ,
                DOL = studentDetails.DOL,
                CommunicationAddress = studentDetails.CommunicationAddress,
                PermanentAddress = studentDetails.PermanentAddress,
                Student_AadhaarNumber = studentDetails.Student_AadhaarNumber,
                ParentEmailId = studentDetails.ParentEmailId,
                Photo = studentDetails.Photo,
                FatherName = studentDetails.FatherName,
                Father_MobileNumber = studentDetails.Father_MobileNumber,
                Father_Photo = studentDetails.Father_Photo,
                motherName = studentDetails.MotherName,
                Mother_MobileNumber = studentDetails.Mother_MobileNumber,
                Mother_Photo = studentDetails.Mother_Photo,
                Gardian1Name = studentDetails.Gardian1Name,
                Gardian1MobileNumber = studentDetails.Gardian1MobileNumber,
                Gardian1Photo = studentDetails.Gardian1Photo,
                Gardian2Name = studentDetails.Gardian2Name,
                Gardian2MobileNumber = studentDetails.Gardian2MobileNumber,
                Gardian2Photo = studentDetails.Gardian2Photo,
                Gardian3Name = studentDetails.Gardian3Name,
                Gardian3MobileNumber = studentDetails.Gardian3MobileNumber,
                Gardian3Photo = studentDetails.Gardian3Photo,

                Gardian4Name = studentDetails.Gardian4Name,
                Gardian4MobileNumber = studentDetails.Gardian4MobileNumber,
                Gardian4Photo = studentDetails.Gardian4Photo,
                ModifiedBy = studentDetails.ModifiedBy,

            };


            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var spName = SPNames.SP_DELETESTUDENT; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }


        public async Task<IEnumerable<StudentDropdown>> GetStudentByName(string studentname)
        {
            var spName = SPNames.SP_GETSTUDENTBYNAME;
            var result = await Task.Factory.StartNew(() =>
                _db.Connection.Query<StudentDropdown>(spName,
                    new { StudentName = studentname }, commandType: CommandType.StoredProcedure).ToList());

            return result;
            

        }

        public async Task<List<StudentSemDateModel>> GetAllStudentConfiguration(int? id)
        {
            var spName = SPNames.SP_GETSTDCONGIG;
            var result = await Task.Factory.StartNew(() =>
                _db.Connection.Query<StudentSemDateModel>(spName,
                    
                     commandType: CommandType.StoredProcedure).ToList());

            return result;
        }

        public async Task updateStudentSemDateDetails(StudentSemDateModel studentSemDate)
        {
            var spNameInsertOrderDetails = SPNames.SP_UPDATESTUDENTSEMDATE; // Name of your stored procedure
                                                                     // Define parameters for the stored procedure
                                                                     //  var spNameInsertOrderItems = SPNames.SP_INSERTORDERDETAIL;
                                                                     // OrderDetail insertedData = new OrderDetail();
                                                                     //  string orderItemsJson = JsonSerializer.Serialize(orderDetails.ItemDetails);
                                                                     //   var sendToDB = new ArrayList();

            var parameters = new
            {
                Sem = studentSemDate.Sem,
                FirstYearStartDate = studentSemDate.FirstYearStartDate,
                FirstYearEndDate = studentSemDate.FirstYearEndDate,
                SecondYearStartDate = studentSemDate.SecondYearStartDate,
                SecondYearEndDate = studentSemDate.SecondYearEndDate,
                ThirdYearStartDate = studentSemDate.ThirdYearStartDate,
                ThirdYearEndDate = studentSemDate.ThirdYearEndDate,
                FeedbackStartDate = studentSemDate.FeedbackStartDate,
                FeedbackEndDate = studentSemDate.FeedbackEndDate,
                ModifiedBy = studentSemDate.ModifiedBy,
                ModifiedDate = studentSemDate.ModifiedDate
            };
            await Task.Factory.StartNew(() =>
               _db.Connection.Execute(spNameInsertOrderDetails, parameters, commandType: CommandType.StoredProcedure));





            //  return studentSemDate;


        }
    }
}
