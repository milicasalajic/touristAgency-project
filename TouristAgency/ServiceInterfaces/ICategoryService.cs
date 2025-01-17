﻿using TouristAgency.DTO.Responses;

namespace TouristAgency.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<AllCategoriesDTO>> GetAllAsync();
    }
}
