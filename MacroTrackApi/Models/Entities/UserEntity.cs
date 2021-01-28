using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MacroTrackApi.Models.Entities
{
    [Table("users")]
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHashed { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
