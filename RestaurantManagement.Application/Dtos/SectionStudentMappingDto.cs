using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Dtos
{
    public class SectionStudentMappingDto
    {
        /// <summary>
        /// Unique identifier for the student-section mapping record.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Name of the section the student belongs to.
        /// </summary>
        public string? SectionName { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the section.
        /// </summary>
        public int SectionId { get; set; } = 0;

        /// <summary>
        /// Grade or class to which the student belongs.
        /// </summary>
        public string? GradeOrClass { get; set; } = string.Empty;

        /// <summary>
        /// Unique identifier for the student.
        /// </summary>
        public int StudentId { get; set; } = 0;

        /// <summary>
        /// Full name of the student.
        /// </summary>
        public string? StudentName { get; set; } = string.Empty;

        /// <summary>
        /// Admission number assigned to the student.
        /// </summary>
        public string AdmissionNumber { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the record is active.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Name of the user who created the record.
        /// </summary>
        public string? CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Date and time when the record was created.
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Name of the user who last modified the record.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Date and time when the record was last modified.
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
