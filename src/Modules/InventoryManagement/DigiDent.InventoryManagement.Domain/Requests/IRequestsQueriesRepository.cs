﻿using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Requests;

public interface IRequestsQueriesRepository
{
    Task<PaginatedResponse<RequestSummary>> GetAllAsync(
        IPaginationOptions paginationOptions, CancellationToken cancellationToken);
}