using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Domain.Entities
{
    public  class StudentSemDateModel
    {
        /// <summary>
        /// Gets or sets the semester name or code.
        /// </summary>
        public string Sem { get; set; }

        /// <summary>
        /// Gets or sets the start date of the first academic year.
        /// </summary>
        public string FirstYearStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the first academic year.
        /// </summary>
        public string FirstYearEndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date of the second academic year.
        /// </summary>
        public string SecondYearStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the second academic year.
        /// </summary>
        public string SecondYearEndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date of the third academic year.
        /// </summary>
        public string ThirdYearStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the third academic year.
        /// </summary>
        public string ThirdYearEndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date of the feedback period.
        /// </summary>
        public string FeedbackStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the feedback period.
        /// </summary>
        public string FeedbackEndDate { get; set; }

        /// <summary>
        /// Gets or sets the username of the person who last modified the record.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the last modification.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
