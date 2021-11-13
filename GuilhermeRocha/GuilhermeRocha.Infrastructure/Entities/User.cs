using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GuilhermeRocha.Infrastructure.Entities
{
    [Table(name: "User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string Surname { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string Password { get; set; }
    }
}
