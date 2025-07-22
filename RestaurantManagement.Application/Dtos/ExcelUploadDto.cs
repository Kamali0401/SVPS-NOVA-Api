using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Dtos
{
    public  class ExcelUploadDto
    {
        
            /// <summary>
            /// Gets or sets the type of user performing the upload (e.g., Student, Faculty).
            /// </summary>
            public string TypeofUser { get; set; }

            /// <summary>
            /// Gets or sets the semester associated with the data in the Excel file.
            /// </summary>
            public string? Sem { get; set; }

            /// <summary>
            /// Gets or sets the section of the students/faculty involved.
            /// </summary>
            public string? Section { get; set; }

            /// <summary>
            /// Gets or sets the academic year associated with the upload.
            /// </summary>
            public string? Year { get; set; }

            /// <summary>
            /// Gets or sets the department ID for which the file is being uploaded.
            /// </summary>
            public long department { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the uploaded file contains attendance data.
            /// </summary>
            public bool isAttendnce { get; set; }

            /// <summary>
            /// Gets or sets the uploaded Excel file.
            /// </summary>
            public IFormFile FormFiles { get; set; }
        
    }
}
