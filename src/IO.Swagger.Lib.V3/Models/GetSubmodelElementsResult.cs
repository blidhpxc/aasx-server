/*
 * DotAAS Part 2 | HTTP/REST | Asset Administration Shell Repository Service Specification
 *
 * The Full Profile of the Asset Administration Shell Repository Service Specification as part of Specification of the Asset Administration Shell: Part 2. Publisher: Industrial Digital Twin Association (IDTA) April 2023
 *
 * OpenAPI spec version: V3.0_SSP-001
 * Contact: info@idtwin.org
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    using System.Text.Json;

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class GetSubmodelElementsResult : IEquatable<GetSubmodelElementsResult>
    {
        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        [DataMember(Name = "result")]
        public List<ISubmodelElement>? Result { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetSubmodelElementsResult {\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson() => JsonSerializer.Serialize(this, options);

        private static readonly JsonSerializerOptions options = new() {WriteIndented = true, IgnoreNullValues = true};

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GetSubmodelElementsResult)obj);
        }

        /// <summary>
        /// Returns true if GetSubmodelElementsResult instances are equal
        /// </summary>
        /// <param name="other">Instance of GetSubmodelElementsResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetSubmodelElementsResult? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Result == other.Result ||
                    (Result != null &&
                     Result.SequenceEqual(other.Result))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (Result != null)
                    hashCode = (hashCode * 59) + Result.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(GetSubmodelElementsResult left, GetSubmodelElementsResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GetSubmodelElementsResult left, GetSubmodelElementsResult right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
