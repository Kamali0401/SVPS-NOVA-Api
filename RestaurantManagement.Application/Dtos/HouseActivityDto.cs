using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Dtos
{
    public class HouseActivityDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity.
        /// </summary>
        [JsonPropertyName("activityName")]
        public string ActivityName { get; set; }

        /// <summary>
        /// Gets or sets the house ID associated with the activity.
        /// </summary>
        [JsonPropertyName("houseId")]
        public int HouseId { get; set; }
        /// <summary>
        /// Gets or sets the house name.
        /// </summary>

        [JsonPropertyName("houseName")]
        public string HouseName { get; set; } =string.Empty;

        /// <summary>
        /// Gets or sets the list of students participating in the activity.
        /// </summary>
        [JsonPropertyName("studentList")]
        public string StudentList { get; set; }

        /// <summary>
        /// Gets or sets the points awarded for the activity.
        /// </summary>
        [JsonPropertyName("point")]
        public int Point { get; set; }

        /// <summary>
        /// Gets or sets the user who created the record.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the record was created.
        /// </summary>
        [JsonPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the record.
        /// </summary>
        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the record was last modified.
        /// </summary>
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }
    }

   
}
