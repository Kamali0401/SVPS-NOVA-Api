using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SonaNova.Application.Dtos
{
    public class LeaveDto
    {
        /// <summary>
        /// Unique identifier for the leave record.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// The ID of the student requesting leave.
        /// </summary>
        [JsonPropertyName("studentId")]
        public int StudentId { get; set; } = 0;

        /// <summary>
        /// Type of leave (e.g., Sick, Casual, Emergency).
        /// </summary>
        [JsonPropertyName("leaveType")]
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// Reason for the leave request.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Date of leave requested by the student.
        /// </summary>
        [JsonPropertyName("dateOfLeave")]
        public DateTime DateOfLeave { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Name of the attached leave document (if any).
        /// </summary>
        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        /// <summary>
        /// Path where the leave document is stored.
        /// </summary>
        [JsonPropertyName("filepath")]
        public string? Filepath { get; set; }

        /// <summary>
        /// User who created the leave request.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Date when the leave record was created.
        /// </summary>
        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// User who last modified the leave record.
        /// </summary>
        [JsonPropertyName("modifiedBy")]
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Date when the leave record was last modified.
        /// </summary>
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Section of the student.
        /// </summary>
        [JsonPropertyName("section")]
        public string Section { get; set; } = string.Empty;

        /// <summary>
        /// Grade or class of the student.
        /// </summary>
        [JsonPropertyName("gradeOrClass")]
        public string GradeOrClass { get; set; } = string.Empty;

        /// <summary>
        /// ID of the coordinator managing the leave request.
        /// </summary>
        [JsonPropertyName("coordinatorId")]
        public int CoordinatorId { get; set; } = 0;

        /// <summary>
        /// Section ID associated with the student.
        /// </summary>
        [JsonPropertyName("sectionId")]
        public int SectionId { get; set; } = 0;

        /// <summary>
        /// Roll number of the student.
        /// </summary>
        [JsonPropertyName("rollNo")]
        public string RollNo { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the student.
        /// </summary>
        [JsonPropertyName("studentName")]
        public string StudentName { get; set; } = string.Empty;
        //public string FileName { get; set; }
        //public string FilePath { get; set; }  // varchar(500)

        public List<string>? FileList { get; set; }
    }
}
