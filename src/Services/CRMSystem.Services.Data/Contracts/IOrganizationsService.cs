﻿namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        Task<int> CreateOrganizationAsync(OrganizationCreateInputModel input, string userId);

        int GetId(string userId);
        T GetById<T>(string userId);

        string GetName(string userId);

        Task<int> GetCountAsync();

    }
}
