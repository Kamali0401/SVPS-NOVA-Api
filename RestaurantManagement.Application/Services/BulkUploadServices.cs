using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SonaNova.Application.Services
{
    public class BulkUploadServices : IBulkUploadService
    {

        private readonly IBulkuploadRepository _BulkUploadRepository;
        private readonly IMapper _mapper;

        public BulkUploadServices(IBulkuploadRepository bulkUploadRepository, IMapper mapper)
        {
            _BulkUploadRepository = bulkUploadRepository;
            _mapper = mapper;
        }

        public async Task<string> bulkuploadstudent(DataTable target)
        {
            return await _BulkUploadRepository.bulkuploadstudent(target);
        }

        public async Task<string> bulkuploadmark(string target, string section)
        {
            return await _BulkUploadRepository.bulkuploadmark(target, section);
        }

        public async Task<string> bulkuploadfaculty(DataTable target)
        {
            return await _BulkUploadRepository.bulkuploadfaculty(target);
        }

        public async Task<string> bulkuploadsubject(DataTable target)
        {
            return await _BulkUploadRepository.bulkuploadsubject(target);
        }

        public async Task<string> bulkuploadholidaycalendar(DataTable target)
        {
            return await _BulkUploadRepository.bulkuploadholidaycalendar(target);
        }

        public async Task<string> bulkuploadacademiccalendar(DataTable target)
        {
            return await _BulkUploadRepository.bulkuploadacademiccalendar(target);
        }

        public async Task<string> bulkuploadtimetable(DataTable target)
        {
            return await _BulkUploadRepository.bulkuploadtimetable(target);
        }

    }
}
