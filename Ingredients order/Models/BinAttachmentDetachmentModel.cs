using Ingredients_order.Models;

namespace Ingredients_order.Models
{
    public class BinAttachmentDetachmentModel
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int MachineModelId { get; set; }
        public string  BinNumber { get; set; }
        public BinAttachmentModel Bin { get; set; }
    }
}
