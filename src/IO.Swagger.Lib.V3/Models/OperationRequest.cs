/*
 * DotAAS Part 2 | HTTP/REST | Repository Service Specification
 *
 * The entire Repository Service Specification as part of Details of the Asset Administration Shell Part 2
 *
 * OpenAPI spec version: V3.0
 *
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public partial class OperationRequest : IEquatable<OperationRequest>
    {
        /// <summary>
        /// Gets or Sets InoutputArguments
        /// </summary>

        [DataMember(Name = "inoutputArguments")]
        public List<OperationVariable>? InoutputArguments { get; set; }

        /// <summary>
        /// Gets or Sets InputArguments
        /// </summary>

        [DataMember(Name = "inputArguments")]
        public List<OperationVariable>? InputArguments { get; set; }

        /// <summary>
        /// Gets or Sets RequestId
        /// </summary>

        [MaxLength(128)]
        [DataMember(Name = "requestId")]
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets or Sets Timeout
        /// </summary>

        [DataMember(Name = "timeout")]
        public int? Timeout { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OperationRequest {\n");
            sb.Append("  InoutputArguments: ").Append(InoutputArguments).Append("\n");
            sb.Append("  InputArguments: ").Append(InputArguments).Append("\n");
            sb.Append("  RequestId: ").Append(RequestId).Append("\n");
            sb.Append("  Timeout: ").Append(Timeout).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OperationRequest)obj);
        }

        /// <summary>
        /// Returns true if OperationRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of OperationRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OperationRequest? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    InoutputArguments == other.InoutputArguments ||
                    (InoutputArguments != null &&
                     InoutputArguments.SequenceEqual(other.InoutputArguments))
                ) &&
                (
                    InputArguments == other.InputArguments ||
                    (InputArguments != null &&
                     InputArguments.SequenceEqual(other.InputArguments))
                ) &&
                (
                    RequestId == other.RequestId ||
                    (RequestId != null &&
                     RequestId.Equals(other.RequestId))
                ) &&
                (
                    Timeout == other.Timeout ||
                    (Timeout != null &&
                     Timeout.Equals(other.Timeout))
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
                if (InoutputArguments != null)
                    hashCode = (hashCode * 59) + InoutputArguments.GetHashCode();
                if (InputArguments != null)
                    hashCode = (hashCode * 59) + InputArguments.GetHashCode();
                if (RequestId != null)
                    hashCode = (hashCode * 59) + RequestId.GetHashCode();
                if (Timeout != null)
                    hashCode = hashCode * 0x3B + Timeout.GetHashCode();
                return hashCode;
            }
        }

        #region Operators

#pragma warning disable 1591

        public static bool operator ==(OperationRequest left, OperationRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OperationRequest left, OperationRequest right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591

        #endregion Operators
    }
}