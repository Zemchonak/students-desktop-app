﻿using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IDisciplinesService : IService<DisciplineDto>
    {
        public void Validate(DisciplineDto entity);
    }
}