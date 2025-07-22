using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public  class Faculty
    {

        /// <summary>
        /// Unique identifier for the faculty/user.
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// Username for login or authentication.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Password for authentication.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Role ID associated with the user.
        /// </summary>
        public long RoleId { get; set; } = 0;

        /// <summary>
        /// Role name associated with the user.
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Faculty-specific unique identifier.
        /// </summary>
        public string FacultyId { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the faculty.
        /// </summary>
        public string FacultyName { get; set; } = string.Empty;

        /// <summary>
        /// First name of the faculty.
        /// </summary>
        public string Faculty_FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Middle name of the faculty (optional).
        /// </summary>
        public string? Faculty_MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the faculty.
        /// </summary>
        public string Faculty_LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gender of the faculty.
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Date of birth of the faculty.
        /// </summary>
        public DateTime DOB { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Primary mobile number of the faculty.
        /// </summary>
        public string FacultyMobileNo_1 { get; set; } = string.Empty;

        /// <summary>
        /// Secondary mobile number of the faculty.
        /// </summary>
        public string FacultyMobileNo_2 { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the faculty.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// File storage path for faculty-related documents.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Delimited string of file names associated with the faculty.
        /// </summary>
        public string FileNames { get; set; } = string.Empty;

        /// <summary>
        /// List of parsed individual file names (derived from FileNames).
        /// </summary>
        public List<string>? files { get; set; }

        /// <summary>
        /// Blood group of the faculty.
        /// </summary>
        public string BloodGroup { get; set; } = string.Empty;

        /// <summary>
        /// Residential address of the faculty.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// User who created the faculty record.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the faculty record was created.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// User who last modified the faculty record.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp of the last modification made to the faculty record.
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    }

    public class FacultyDropdown
    {
        public long Id { get; set; }
        public string FacultyID { get; set; }
        public string FacultyName { get; set; }

    }
}
