﻿using TouristAgency.DTO.Responses;

namespace TouristAgency.RepositoryInterfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<AllCategoriesDTO>> GetAllAsync();
    }
}
