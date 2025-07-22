using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Dtos
{
    public class RoleDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [JsonPropertyName("roleId")]
        public int RoleId { get; set; }

        /// Gets or sets the name of the role.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the user who created the record.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time the record was created.
        /// </summary>
        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the user who last modified the record.
        /// </summary>
        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; } = null;

        /// <summary>
        /// Gets or sets the date and time the record was last modified.
        /// </summary>
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;

        /// <summary>
        /// Gets or sets the role name associated with the user.
        /// </summary>

    }
}

