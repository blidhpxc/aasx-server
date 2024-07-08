namespace IO.Swagger.Registry.Lib.V3.Services;

using System;
using System.Collections.Generic;

/// <inheritdoc />
public class SubmodelPropertyExtractionService : ISubmodelPropertyExtractionService
{
    /// <inheritdoc />
    public (int found, Property? jsonProperty, Property? endpointProperty) FindMatchingProperties(SubmodelElementCollection elementCollection, string? aasID,
                                                                                                  string? assetID)
    {
        var       found            = 0;
        Property? jsonProperty     = null;
        Property? endpointProperty = null;

        if (elementCollection.Value == null)
        {
            return (found, jsonProperty, endpointProperty);
        }

        foreach (var subElement in elementCollection.Value)
        {
            if (subElement is Property property)
            {
                switch (property.IdShort)
                {
                    case "aasID" when property.Value == aasID:
                    case "assetID" when property.Value == assetID:
                        found++;
                        break;
                    case "descriptorJSON":
                        jsonProperty = property;
                        break;
                    case "endpoint":
                        endpointProperty = property;
                        break;
                }
            }
        }

        return (found, jsonProperty, endpointProperty);
    }

    /// <inheritdoc />
    public void AddPropertyToCollection(SubmodelElementCollection collection, string idShort, string value, DateTime timestamp, bool checkEmpty = false)
    {
        if (checkEmpty && string.IsNullOrEmpty(value))
        {
            return;
        }

        var property = new Property(DataTypeDefXsd.String, idShort: idShort) {TimeStampCreate = timestamp, TimeStamp = timestamp, Value = value};

        collection.Value?.Add(property);
    }


    /// <inheritdoc />
    public SubmodelElementCollection CreateSubmodelElementCollection(string idShort, DateTime timestamp) =>
        new(idShort: idShort, value: new List<ISubmodelElement>()) {TimeStampCreate = timestamp, TimeStamp = timestamp};
}