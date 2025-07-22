using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Dtos
{
    public  class MarkDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student mark record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the student ID.
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Gets or sets the grade or class of the student.
        /// </summary>
        public string GradeOrClass { get; set; }

        /// <summary>
        /// Gets or sets the section ID.
        /// </summary>
        public int Section { get; set; }

        /// <summary>
        /// Gets or sets the subject-wise marks and related data as a string.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the attendance status for the previous month.
        /// </summary>
        public string PreviousMonthAttendance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether attendance is required.
        /// </summary>
        public bool IsattendanceRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mark is ready to be sent via email.
        /// </summary>
        public bool ReadytosendEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parent has been intimated.
        /// </summary>
        public bool IsParentIntemated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the record was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the username of the person who created the record.
        /// </summary>
        public string createdby { get; set; }

        /// <summary>
        /// Gets or sets the type of test (e.g., Midterm, Final).
        /// </summary>
        public string TestType { get; set; }
    }

    public class MarkDetailsDto
    {
        /// <summary>
        /// Gets or sets the code of the subject.
        /// </summary>
        public string SubjectCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the marks obtained in the subject.
        /// </summary>
        public string Marks { get; set; }
    }
}
