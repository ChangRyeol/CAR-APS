using SimulationEngine.BaseEntity;
using SimulationEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApsModel.InputEntity
{
    public class CarLot : Lot
    {
        public Step initStep { get; set; }
        public CarLot(int lotQty = 10)
        {
            State = LotState.WAIT;
            LotQty = lotQty;
            initStep = null;
            IsRework = false; 
        }
        public CarLot(Step initStep, Equipment equipment, LotState state)
        {
            this.initStep = initStep;
        }
        public bool IsRework { get; set; }
    }
}
