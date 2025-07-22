using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public  class TableMappingDetails
    {
        /// <summary>
        /// Unique identifier for the table record.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// Id of the table who choose the table.
        /// </summary>
        [JsonPropertyName("tableId")]
        public int TableId { get; set; } = 0; // Default to an empty string
        /// <summary>
        /// Id of the table who choose the table.
        /// </summary>
        [JsonPropertyName("statusId")]
        public int StatusId { get; set; } = 0; // Default to an empty string
        // <summary>
        /// Id of the table who choose the table.
        /// </summary>
        [JsonPropertyName("orderId")]
        public int orderId { get; set; } = 0; // Default to an empty string
        /// <summary>
        /// code of the table who choose the table.
        /// </summary>
        [JsonPropertyName("tableCode")]
        public string TableCode { get; set; } = string.Empty; // Default to an empty string
        /// <summary>
        /// Catagory of the table who choose the table.
        /// </summary>
        [JsonPropertyName("tableCatagory")]
        public string TableCatagory { get; set; } = string.Empty; // Default to an empty string
        /// <summary>
        /// status of the table who choose the table.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty; // Default to an empty string

        /// <summary>
        /// No of seats  for the table record.
        /// </summary>
        [JsonPropertyName("seatId")]
        public int seatId { get; set; } = 0;
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
    }
}
