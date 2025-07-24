using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Domain.Entities
{
    public  class Attendance
    { /// <summary>
      /// Gets or sets the unique identifier for the attendance record.
      /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the section ID to which the student belongs.
        /// </summary>
        public int SectionId { get; set; } = 0;

        /// <summary>
        /// Gets or sets the full name of the student.
        /// </summary>
        public string? StudentName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the student's admission number.
        /// </summary>
        public string? AdmissionNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the student’s unique identifier.
        /// </summary>
        public long StudentId { get; set; } = 0;

        /// <summary>
        /// Gets or sets the date of the attendance entry.
        /// </summary>
        public DateTime Date { get; set; }= DateTime.Now;

        /// <summary>
        /// Gets or sets a value indicating whether the student was present.
        /// </summary>
        public bool IsPresent { get; set; }= false;

        /// <summary>
        /// Gets or sets the number of hours the student attended.
        /// </summary>
        public int Hoursday { get; set; } = 0;

        /// <summary>
        /// Gets or sets the hours attended as a formatted string (e.g., "2.5 Hours").
        /// </summary>
        public string? Hoursdays { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username of the person who created the record.
        /// </summary>
        public string? CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the record was created.
        /// </summary>
        public DateTime? CreatedDate { get; set; }=DateTime.Now;

        /// <summary>
        /// Gets or sets the username of the person who last modified the record.
        /// </summary>
        public string? ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the record was last modified.
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }

    public class StudentAttendanceModel
    {
        public Int64 StudentId { get; set; }
        public string StudentName { get; set; }

        public Dictionary<string, string> AttendanceRecords { get; set; } = new Dictionary<string, string>();

    }
}
