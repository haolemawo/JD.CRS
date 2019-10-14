﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Castle.Core.Internal;
using JD.CRS.Student.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JD.CRS.Student
{
    public class StudentAppService : AsyncCrudAppService<Entitys.Student, StudentDto, int, PagedResultRequestDto,// GetAllStudentsInput,
                             CreateUpdateStudentDto, CreateUpdateStudentDto>, IStudentAppService

    {
        private readonly IRepository<Entitys.Student, int> _StudentRepository;
        public StudentAppService(IRepository<Entitys.Student, int> StudentRepository)
            : base(StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        public override Task<StudentDto> Create(CreateUpdateStudentDto input)
        {
            return base.Create(input);
        }

        public override async Task<PagedResultDto<StudentDto>> GetAll(PagedResultRequestDto input)//(GetAllStudentsInput input)
        {
            //查询
            var query = base.CreateFilteredQuery(input);
            //获取总数
            var Studentcount = query.Count();
            //获取清单
            var Studentlist = query.ToList();

            //return new PagedResultDto<StudentDto>(Studentcount, Studentlist.MapTo<List<StudentDto>>());
            return new PagedResultDto<StudentDto>()
            {
                TotalCount = Studentcount,
                Items = ObjectMapper.Map<List<StudentDto>>(Studentlist)
            };
        }
    }
}