using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class ProcessModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MachineModel> Machines { get; set; }
    }
    public class MachineModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcessModelId { get; set; }
        public ProcessModel Process { get; set; }
        public ICollection<BinAttachmentModel> Bins { get; set; }
    }

    public class BinAttachmentModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(12)]
        [MaxLength(12)]
        public string BinNumber { get; set; }
        public string BinStatus { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string MachineName { get; set; }
        public MachineModel Machine { get; set; }
    }
}
