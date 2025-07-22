using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Dtos
{
    public  class SectionSubjectMappingDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the section-subject mapping.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the section.
        /// </summary>
        public long SectionID { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string? SectionName { get; set; }

        /// <summary>
        /// Gets or sets the grade or class of the section.
        /// </summary>
        public string? GradeorClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the faculty assigned to the section and subject.
        /// </summary>
        public string? FacultyName { get; set; }

        /// <summary>
        /// Gets or sets the section code or identifier.
        /// </summary>
        public string? Section { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the subject.
        /// </summary>
        public long SubjectID { get; set; }

        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        public string? SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the faculty.
        /// </summary>
        public string? FacultyMobileNo_1 { get; set; }

        /// <summary>
        /// Gets or sets the code of the subject.
        /// </summary>
        public string? SubjectCode { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the faculty.
        /// </summary>
        public long FacultyID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mapping is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the username who created the record.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the record was created.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the username who last modified the record.
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the record was last modified.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}

