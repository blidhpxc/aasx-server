﻿using IO.Swagger.Models;
using System.Collections.Generic;

namespace IO.Swagger.Lib.V3.Interfaces;

using System;

/// <summary>
/// Service to apply level and extent modifications to instances of IClass.
/// </summary>
public interface ILevelExtentModifierService
{
    /// <summary>
    /// Applies level and extent transformations to a single IClass instance.
    /// </summary>
    /// <param name="that">The IClass instance to transform.</param>
    /// <param name="level">The level of transformation. Default is LevelEnum.Deep.</param>
    /// <param name="extent">The extent of transformation. Default is ExtentEnum.WithoutBlobValue.</param>
    /// <returns>The transformed IClass instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the input IClass instance is null.</exception>
    IClass ApplyLevelExtent(IClass that, LevelEnum level = LevelEnum.Deep, ExtentEnum extent = ExtentEnum.WithoutBlobValue);

    /// <summary>
    /// Applies level and extent transformations to a list of IClass instances.
    /// </summary>
    /// <param name="that">The list of IClass instances to transform.</param>
    /// <param name="level">The level of transformation. Default is LevelEnum.Deep.</param>
    /// <param name="extent">The extent of transformation. Default is ExtentEnum.WithoutBlobValue.</param>
    /// <returns>The list of transformed IClass instances.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the input list is null.</exception>
    List<IClass?> ApplyLevelExtent(List<IClass?> that, LevelEnum level = LevelEnum.Deep, ExtentEnum extent = ExtentEnum.WithoutBlobValue);
}
