using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.DatabaseConnection;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
//using static System.Collections.Specialized.BitVector32;

namespace RestaurantManagement.Infrastructure.Repositories
{/// <summary>
 /// Repository class for performing CRUD operations on TableMaster.
 /// </summary>
    public class SectionRepository : ISectionRepository
    {
        private readonly IDataBaseConnection _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="SectionRepository"/> class.
    /// </summary>
    /// <param name="_db">The database connection for accessing billing data.</param>
    public SectionRepository(IDataBaseConnection db)
    {
        this._db = db;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SectionMaster>> GetSectionDetails(int? id)
    {
        var spName = SPNames.SP_GETALLSECTION; // Update the stored procedure name if necessary
        return await Task.Factory.StartNew(() => _db.Connection.Query<SectionMaster>(spName,
            new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
    }

    public async Task<SectionMaster> InsertSectionDetails(SectionMaster section)
    {
        var spName = SPNames.SP_INSERTSECTION; // Name of your stored procedure
                                                      // Define parameters for the stored procedure



        var parameters = new
        {
            GradeOrClass = section.GradeOrClass,
            Section = section.Section,
            CoordinatorId = section.CoordinatorId,

            IsActive = section.IsActive,
            CreatedBy = section.CreatedBy

        };

        // Execute the stored procedure and retrieve the inserted data
        await _db.Connection.QuerySingleOrDefaultAsync<SectionMaster>(
            spName,
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return section;


    }
    /// <inheritdoc/>
    public async Task UpdateSectionDetails(SectionMaster section)
    {
        var spName = SPNames.SP_UPDATESECTION; // Update the stored procedure name if necessary


        var parameters = new
        {
            Id = section.Id,
            GradeOrClass = section.GradeOrClass,
            Section = section.Section,
            CoordinatorId = section.CoordinatorId,

            IsActive = section.IsActive,

            ModifiedBy = section.ModifiedBy


        };
        await Task.Factory.StartNew(() =>
            _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
    }

    public async Task<bool> DeleteSectionDetails(int id)
    {
        var spName = SPNames.SP_DELETESECTION; // Update the stored procedure name if necessary
        await Task.Factory.StartNew(() =>
            _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
        return true;
    }
  }

}
