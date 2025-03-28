﻿using DataMart.Input;
using DataMart.SqlMapper;
using CarApsModel.Helper;
using CarApsModel.InputEntity;
using SimulationEngine.BaseEntity;
using SimulationEngine.SimulationEntity;
using SimulationEngine.SimulationInterface;
using SimulationEngine.SimulationObject;
using System.Collections.Generic;
using System.Linq;

namespace CarApsModel
{
    public class CarRouteModel : ISimRouteModel
    {
        public Step GetNextStep(SimLot lot)
        {
            CarLot pl = (lot.GetLot()) as CarLot; 
            //한개 lot은 rework 한번만 발생. 
            //if (lot.StepId == "INSPECTION" && pl.IsRework == false)
            //{
            //    double reworkProbability = 15; //rework 확률 = 15% 
            //    if (Utils.IsTrueWithPercent(reworkProbability)) 
            //    {
            //        return pl.Process.GetFirstStep(); //rework시 첫 step부터 다시 
            //    };
            //}
            return pl.Process.GetNextStep(lot.StepId); 
        }

        public void OnStepDone(SimLot lot, EqpSchedule plan)
        {
            OutputHelper.WriteEqpSchedule(plan, lot);

            //step done인데 INSPECTION 공정이라는 뜻 = 다음 공정부터는 rework이다 
            if (lot.StepId == "INSPECTION")
            {
                CarLot pl = (lot.GetLot()) as CarLot;
                pl.IsRework = true; 
            }
        }

        public void OnLotDone(SimLot lot, EqpSchedule plan)
        {
            OutputHelper.WriteEqpSchedule(plan, lot);
        }

        public void OnProcessed(SimLot lot, SimEquipment equipment)
        {
            //throw new NotImplementedException();
        }

        public void OnRelease(SimLot lot)
        {
            //throw new NotImplementedException();
        }

        public void OnProcessIn(SimLot lot, SimEquipment equipment)
        {
            //throw new NotImplementedException();
        }
        public bool IsSetup(SimLot lot, SimEquipment equipment)
        {
            List<SETUP_INFO> setupInfoList = InputMart.Instance.GetList<SETUP_INFO>(InputTable.SETUP_INFO);
            SETUP_INFO info = setupInfoList.Where(x => x.EQP_ID.Equals(equipment.EqpId)).FirstOrDefault();
            if (info == null) return false; 

            if(info.SETUP_CONDITION == "PRODUCT_CHANGE" && equipment.GetPreviousPlan() != null)
            {
                if (equipment.GetPreviousPlan().ProductId != lot.ProductId) {
                    return true;
                }
            }
            
            return false; 
        }
        public void OnSetupOut(SimEquipment equipment, SimLot lot, EqpSchedule plan)
        {
            OutputHelper.WriteEqpSchedule(plan, lot);
        }

        public double GetTransferTime(SimLot lot, Step step, Step nextStep)
        {
            return 3600; //1시간 
        }
    }
}
