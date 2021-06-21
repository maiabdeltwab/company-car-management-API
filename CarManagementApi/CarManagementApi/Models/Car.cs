using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarManagementApi.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public bool IsInParking { get; set; } = false;

        [ForeignKey("Employee")]
        [Required]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        [ForeignKey("AccessCard")]
        [Required]
        public int AccessCardId { get; set; }

        public virtual AccessCard AccessCard { get; set; }
    }
}