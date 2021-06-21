using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarManagementApi.Models
{
    public class AccessCard
    {
        [Key]
        public int Id { get; set; }

        public double Credit { get; set; } = 10;

        public DateTime LastOperation { get; set; } = DateTime.Now;
    }
}