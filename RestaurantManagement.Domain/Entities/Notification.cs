using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SonaNova.Domain.Entities
{
    public class Notification
    {
        /// <summary>
        /// Gets or sets the type of the message or notification.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the type of the message or notification.
        /// </summary>
        [JsonPropertyName("msgType")]
        public string MsgType { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the student associated with the notification.
        /// </summary>
        [JsonPropertyName("studentId")]
        public long StudentId { get; set; }

        /// <summary>
        /// Gets or sets the notification message content.
        /// </summary>
        [JsonPropertyName("notificationMsg")]
        public string NotificationMsg { get; set; }
    }
}
