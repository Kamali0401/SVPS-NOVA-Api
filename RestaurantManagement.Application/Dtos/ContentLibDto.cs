using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Dtos
{
    public class ContentLibDto
    {
        /// <summary>
        /// Unique identifier for the content entry.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Identifier of the section this content is assigned to.
        /// </summary>
        public int SectionId { get; set; } = 0;

        /// <summary>
        /// Title of the content or document.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Detailed description of the content.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Faculty member’s unique ID who uploaded the content.
        /// </summary>
        public long FacultyId { get; set; } = 0;

        /// <summary>
        /// The date when the content becomes expired or no longer valid.
        /// </summary>
        public DateTime ExpiryDate { get; set; }=DateTime.UtcNow;

        /// <summary>
        /// The name of the file associated with the content (stored as a single string).
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Path where the content file is stored.
        /// </summary>
        public string? FilePath { get; set; }

        /// <summary>
        /// Name of the user who created the content entry.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the content entry was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }=DateTime.UtcNow;

        /// <summary>
        /// Name of the user who last modified the content, if applicable.
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Timestamp of the last modification to the content entry.
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;  

        /// <summary>
        /// Name of the section the content belongs to (for display purposes).
        /// </summary>
        public string? Section { get; set; }

        /// <summary>
        /// Grade or class information associated with the content.
        /// </summary>
        public string? GradeOrClass { get; set; }

        /// <summary>
        /// A list of file names (split from FileName) if multiple files are associated.
        /// </summary>
        public List<string>? FileList { get; set; }

        /// <summary>
        /// Faculty member’s roll number who uploaded the content.
        /// </summary>
        public string FacultyRollNo { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the faculty member who uploaded the content.
        /// </summary>
        public string FacultyName { get; set; }=string.Empty;
    }
}
