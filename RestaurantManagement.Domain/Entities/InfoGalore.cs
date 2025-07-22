using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SonaNova.Domain.Entities
{
    public  class InfoGalore
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the name of the info file.
        /// </summary>
        [JsonPropertyName("infoFileName")]
        public string InfoFileName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the type of the info file.
        /// </summary>
        [JsonPropertyName("infoType")]
        public string InfoType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path of the info file.
        /// </summary>
        [JsonPropertyName("infoFilePath")]
        public string InfoFilePath { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the file of the info galore.
        /// </summary>
     [JsonPropertyName("infoFile")]
        public List<IFormFile>?InfoFile { get; set; }

        /// <summary>
        /// Gets or sets the valid date.
        /// </summary>
        [JsonPropertyName("validDate")]
        public DateTime ValidDate { get; set; }=DateTime.UtcNow;


        /// <summary>
        /// Gets or sets the user who created the record.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }=  string.Empty;

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
    }
    public class AttachmentModel
    {
        public string FileName { get; set; }   // Name of the image file
        public string FilePath { get; set; }
        public byte[]? BlobData { get; set; }   // Blob (byte array) for the image file
    }
    public class InfoAttachmentModel
    {
        public string FileName { get; set; }   // Name of the image file
        public string FilePath { get; set; }
        public string InfoType { get; set; }
        public byte[]? BlobData { get; set; }   // Blob (byte array) for the image file
    }
}
