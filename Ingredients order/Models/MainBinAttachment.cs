using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingredients_order.Models
{
    public class MainBinAttachment
    {
        public List<ProcessModel> Processes { get; set; }
        public List<MachineModel> Machines { get; set; }

        public MainBinAttachment()
        {
            Processes = new List<ProcessModel>()
            {
                new ProcessModel {Id = 1, Name = "Process Area"},
                new ProcessModel {Id = 2, Name = "Packing Area"},
                new ProcessModel {Id = 3, Name = "Warehouse"},
                new ProcessModel {Id = 4, Name = "Dry good's Area"},

            };
            Machines = new List<MachineModel>()
            {
                new MachineModel {Id = 1, Name = "Process machine № 1", ProcessModelId = 1},
                new MachineModel {Id = 2, Name = "Process machine № 2", ProcessModelId = 1},
                new MachineModel {Id = 3, Name = "Process machine № 3", ProcessModelId = 1},



                new MachineModel {Id = 4, Name = "Packing machine № 1", ProcessModelId = 2},
                new MachineModel {Id = 5, Name = "Packing machine № 2", ProcessModelId = 2},
                new MachineModel {Id = 6, Name = "Packing machine № 3", ProcessModelId = 2},
                new MachineModel {Id = 7, Name = "Packing machine № 4", ProcessModelId = 2},
                new MachineModel {Id = 8, Name = "Packing machine № 5", ProcessModelId = 2},



                new MachineModel {Id = 9, Name = "Area № 1", ProcessModelId = 3},
                new MachineModel {Id = 10, Name = "Area № 2", ProcessModelId = 3},

                new MachineModel {Id = 11, Name = "Dry good's Area", ProcessModelId = 4},
            };
        }
    }
}
