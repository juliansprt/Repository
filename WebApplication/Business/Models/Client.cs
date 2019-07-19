using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Business.Models
{
    public class Client
    {
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string Name { get; set; }

        [StringLength(80)]
        [Required]
        public string LastName { get; set; }
    }
}
