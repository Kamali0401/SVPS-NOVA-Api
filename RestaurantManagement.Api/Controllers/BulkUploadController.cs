using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using SonaNova.Application.Common;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;

namespace SonaNova.Api.Controllers
{/// <summary>
 /// Controller for handling CRUD operations on AssignmentDto.
 /// </summary>
    [Route("api/bulkupload")]
    [ApiController]
    [Authorize]
    public class BulkUploadController : SonaNovaControllerBase

    {
        
        private readonly IBulkUploadService _BulkUploadService;
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkUploadController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="BulkUploadService">The BulkUploadDto service instance used for CRUD operations on BulkUploadDto.</param>
        public BulkUploadController(ILogger<BulkUploadController> logger, IBulkUploadService BulkUploadService) : base(logger)
        {
            _BulkUploadService = BulkUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> bulkupload([FromForm] ExcelUploadDto fileUploadModel)
        {
            var result = "";
            if (fileUploadModel.FormFiles != null)
            {

                (string filePath, string fileName) = await FileOperations.SaveFileWithTimeStamp(fileUploadModel);

                var XlPath = fileUploadModel.FormFiles.FileName;
                if (XlPath == "Student.csv")
                {
                    var uploadedFileData = DataTableConverter.ConvertCsvToDataTable(filePath);
                    result = await _BulkUploadService.bulkuploadstudent(uploadedFileData);

                }
                else if (XlPath == "Faculty.csv")
                {
                    var uploadedFileData = DataTableConverter.ConvertCsvToDataTable(filePath);
                    result = await _BulkUploadService.bulkuploadfaculty(uploadedFileData);
                }

                else if (XlPath == "Subject.csv")
                {
                    var uploadedFileData = DataTableConverter.ConvertCsvToDataTable(filePath);
                    result = await _BulkUploadService.bulkuploadsubject(uploadedFileData);
                }
                else if (XlPath == "Mark.xlsx")
                {
                    result = await _BulkUploadService.bulkuploadmark(filePath, fileUploadModel.Section);
                }
                else if (XlPath == "HolidayCalendar.csv")
                {
                    var uploadedFileData = DataTableConverter.ConvertCsvToDataTable(filePath);
                    result = await _BulkUploadService.bulkuploadholidaycalendar(uploadedFileData);
                }
                else if (XlPath == "AcademicCalendar.csv")
                {
                    var uploadedFileData = DataTableConverter.ConvertCsvToDataTable(filePath);
                    result = await _BulkUploadService.bulkuploadacademiccalendar(uploadedFileData);
                }

                else if (XlPath == "Timetable.csv")
                {
                    var uploadedFileData = DataTableConverter.ConvertCsvToDataTable(filePath);
                    result = await _BulkUploadService.bulkuploadtimetable(uploadedFileData);
                }
            }
            return Ok(result);
        }
    }
}
