﻿using AdminShellNS.Models;
using IO.Swagger.Lib.V3.Models;
using IO.Swagger.Models;
using System.Collections.Generic;

namespace IO.Swagger.Lib.V3.Interfaces;

/// <summary>
/// Service for handling pagination of lists.
/// </summary>
public interface IPaginationService
{
    /// <summary>
    /// Paginates a generic list based on provided parameters.
    /// </summary>
    /// <typeparam name="T">Type of items in the list.</typeparam>
    /// <param name="sourceList">Source list to paginate.</param>
    /// <param name="paginationParameters">Pagination parameters including cursor and limit.</param>
    /// <returns>Paginated result containing a subset of the source list.</returns>
    PagedResult GetPaginatedList<T>(List<T> sourceList, PaginationParameters paginationParameters);

    /// <summary>
    /// Paginates a list of PackageDescription objects based on provided parameters.
    /// </summary>
    /// <param name="sourceList">Source list of PackageDescription objects to paginate.</param>
    /// <param name="paginationParameters">Pagination parameters including cursor and limit.</param>
    /// <returns>Paginated result containing a subset of the source list.</returns>
    PackageDescriptionPagedResult GetPaginatedPackageDescriptionList(List<PackageDescription> sourceList, PaginationParameters paginationParameters);
}