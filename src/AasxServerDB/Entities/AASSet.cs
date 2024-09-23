namespace AasxServerDB.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    // indexes
    [Index(nameof(Id))]

    public class AASSet
    {
        // env
        [ForeignKey("EnvSet")]
        public         int      EnvId  { get; set; }
        public virtual EnvSet? EnvSet { get; set; }

        // aas
        public int Id { get; set; }

        // asset administration shell
        [StringLength(128)]
        public string? IdShort                    { get; set; }
        public string? DisplayName                { get; set; }
        [StringLength(128)]
        public string? Category                   { get; set; }
        public string? Description                { get; set; }
        public string? Extensions                 { get; set; }
        [MaxLength(2000)]
        public string? Identifier                 { get; set; }
        public string? Administration             { get; set; }
        public string? EmbeddedDataSpecifications { get; set; }
        public string? DerivedFrom                { get; set; }

        // asset information
        public string? AssetKind          { get; set; }
        public string? GlobalAssetId      { get; set; }
        public string? AssetType          { get; set; }
        public string? SpecificAssetIds   { get; set; }
        public string? DefaultThumbnail   { get; set; }

        // time stamp
        public DateTime TimeStampCreate { get; set; }
        public DateTime TimeStamp       { get; set; }
        public DateTime TimeStampTree   { get; set; }
        public DateTime TimeStampDelete { get; set; }

        // sm
        public virtual ICollection<SMSet> SMSets { get; } = new List<SMSet>();
    }
}