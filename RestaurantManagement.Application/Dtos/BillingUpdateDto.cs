using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Dtos
{
    public class BillingUpdateDto
    {
       

        /// <summary>
        /// Gets or sets the associated order ID.
        /// </summary>
        [JsonPropertyName("orderId")]
        public int? OrderId { get; set; }
       
        /// <summary>
        /// Gets or sets the mode of payment used.
        /// </summary>
        [JsonPropertyName("paymentMode")]
        public string PaymentMode { get; set; } = string.Empty;

        

        /// <summary>
        /// Gets or sets the name of the user who last modified the record.
        /// </summary>
        [JsonPropertyName("modifiedBy")]
        public string ModifiedBy { get; set; }= string.Empty;

      
    }
}
