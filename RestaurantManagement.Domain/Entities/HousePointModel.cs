using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SonaNova.Domain.Entities
{
    public  class HousePointModel
    {
       
        
            /// <summary>
            /// Gets or sets the row number.
            /// </summary>
            [JsonPropertyName("id")]
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the total points for the house.
            /// </summary>
            [JsonPropertyName("totalPoints")]
            public int TotalPoints { get; set; }

            /// <summary>
            /// Gets or sets the house ID.
            /// </summary>
            [JsonPropertyName("houseId")]
            public int HouseId { get; set; }

            /// <summary>
            /// Gets or sets the house name.
            /// </summary>
            [JsonPropertyName("houseName")]
            public string HouseName { get; set; }
        }
    
}
