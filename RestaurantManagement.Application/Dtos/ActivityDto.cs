using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Dtos
{
    public class ActivityDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity record.
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the department ID associated with the activity.
        /// </summary>
        public long DepartmentID { get; set; } = 0;

        /// <summary>
        /// Gets or sets the concatenated file names associated with the activity, separated by a delimiter.
        /// </summary>
        public string? FileNames { get; set; }

        /// <summary>
        /// Gets or sets the activity ID.
        /// </summary>
        public long ActivityID { get; set; } = 0;

        /// <summary>
        /// Gets or sets the name of the activity.
        /// </summary>
        public string ActivityName { get; set; }=string.Empty;

        /// <summary>
        /// Gets or sets the additional data or description for the activity.
        /// </summary>
        public string Data { get; set; }= string.Empty;

        /// <summary>
        /// Gets or sets the file path where activity files are stored.
        /// </summary>
        public string? FilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who created the activity record.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the activity was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }=DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the name of the user who last modified the activity record.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time the activity record was last modified.
        /// </summary>
        public string? ModifiedDate { get; set; }=string.Empty;

        /// <summary>
        /// Gets or sets the list of individual file names split from FileNames.
        /// </summary>
        public List<string>? Files { get; set; }

        /// <summary>
        /// Gets or sets the list of file blobs or base64 representations of files associated with the activity.
        /// </summary>
        public List<string>? FileBlob { get; set; }

    }
}
