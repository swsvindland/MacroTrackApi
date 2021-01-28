using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MacroTrackApi.Models.Entities
{
    [Table("authCodes")]
    public class AuthCodeEntity
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public int Code { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}
