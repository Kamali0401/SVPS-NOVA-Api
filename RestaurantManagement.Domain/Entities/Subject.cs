using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Subject
    {
        /// <summary>
        /// Gets or sets the unique identifier for the subject.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the short form of the subject name.
        /// </summary>
        [JsonPropertyName("subjectShortForm")]
        public string SubjectShortForm { get; set; }

        /// <summary>
        /// Gets or sets the unique code of the subject.
        /// </summary>
        [JsonPropertyName("subjectCode")]
        public string SubjectCode { get; set; }

        /// <summary>
        /// Gets or sets the full name of the subject.
        /// </summary>
        [JsonPropertyName("subjectName")]
        public string SubjectName { get; set; }

        /// <summary>
        /// Gets or sets the grade associated with the subject.
        /// </summary>
        [JsonPropertyName("grade")]
        public string Grade { get; set; }

        /// <summary>
        /// Gets or sets the user who created the subject record.
        /// </summary>
        [JsonPropertyName("createdBy")] 
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the subject record was created.
        /// </summary>
        [JsonPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the subject record.
        /// </summary>
        [JsonPropertyName("modifiedBy")]
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the subject record was last modified.
        /// </summary>
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }
    }
}
