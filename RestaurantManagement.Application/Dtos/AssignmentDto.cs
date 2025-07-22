using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Dtos
{
    public class AssignmentDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the assignment.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the section ID associated with the assignment.
        /// </summary>
        public int SectionId { get; set; } = 0;

        /// <summary>
        /// Gets or sets the subject ID related to the assignment.
        /// </summary>
        public int SubjectId { get; set; } = 0;

        /// <summary>
        /// Gets or sets the title of the assignment.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the faculty ID who created or is responsible for the assignment.
        /// </summary>
        public int FacultyId { get; set; } = 0;

        /// <summary>
        /// Gets or sets the description of the assignment.
        /// </summary>
        public string Description { get; set; }=string.Empty;

        /// <summary>
        /// Gets or sets the due date of the assignment.
        /// </summary>
        public DateTime DueDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the name of the uploaded file.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the file path where the assignment file is stored.
        /// </summary>
        public string? FilePath { get; set; }

        /// <summary>
        /// Gets or sets the list of files associated with the assignment.
        /// </summary>
        public List<string>? FileList { get; set; }

        /// <summary>
        /// Gets or sets the grade or class related to the assignment.
        /// </summary>
        public string GradeOrClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the section name related to the assignment.
        /// </summary>
        public string Section { get; set; }=   string.Empty;

        /// <summary>
        /// Gets or sets the coordinator's user ID associated with the assignment.
        /// </summary>
        public int CoordinatorId { get; set; } = 0;

        /// <summary>
        /// Gets or sets the faculty roll number of the assignment creator.
        /// </summary>
        public string FacultyRollNo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full name of the faculty who assigned the work.
        /// </summary>
        public string FacultyName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the subject code of the subject assigned.
        /// </summary>
        public string SubjectCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the subject for which the assignment is created.
        /// </summary>
        public string SubjectName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user who created the assignment record.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user who last modified the assignment record.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

    }
}

