using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MacroTrackApi.Models.Entities
{
    [Table("accessTokens")]
    public class AccessTokenEntity
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public DateTime Created { get; set; }
    }
}
