using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Dtos
{
    public class TimeTableDto
    {
        /// <summary>
        /// Unique identifier for the timetable entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Day of the week for which the timetable is applicable (e.g., Monday, Tuesday).
        /// </summary>
        public string Day { get; set; } = string.Empty;

        /// <summary>
        /// Identifier for the section (can be alphanumeric).
        /// </summary>
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        /// Hall number or location where the classes are conducted.
        /// </summary>
        public string HallNo { get; set; } = string.Empty;

        /// <summary>
        /// Grade or class associated with the timetable (e.g., Grade I, Class 10).
        /// </summary>
        public string GradeOrClass { get; set; } = string.Empty;

        /// <summary>
        /// Section name (e.g., A, B, Red).
        /// </summary>
        public string Section { get; set; } = string.Empty;

        /// <summary>
        /// The date from which this timetable entry becomes effective.
        /// </summary>
        public DateTime WithEffectFrom { get; set; }

        /// <summary>
        /// Scheduled activity or subject for hour 1.
        /// </summary>
        public string Hour1 { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled activity or subject for hour 2.
        /// </summary>
        public string Hour2 { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled activity or subject for hour 3.
        /// </summary>
        public string Hour3 { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled activity or subject for hour 4.
        /// </summary>
        public string Hour4 { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled activity or subject for hour 5.
        /// </summary>
        public string Hour5 { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled activity or subject for hour 6.
        /// </summary>
        public string Hour6 { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled activity or subject for hour 7.
        /// </summary>
        public string Hour7 { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled activity or subject for hour 8.
        /// </summary>
        public string Hour8 { get; set; } = string.Empty;

        /// <summary>
        /// Name of the user who created the timetable entry.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the timetable entry was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Name of the user who last modified the timetable entry, if applicable.
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Timestamp of the last modification to the timetable entry, if any.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
