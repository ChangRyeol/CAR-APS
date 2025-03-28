﻿using SimulationEngine.SimulationInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApsModel
{
    public class CarModelGroup : IModelGroup
    {
        public ISimulationModel SimulationModel { get; private set; }
        public ISimEquipmentModel EquipmentModel { get; private set; }
        public ISimLotModel LotModel { get; private set; }
        public ISimRouteModel RouteModel { get; private set; }
        public ISimDispatchModel DispatchModel { get; private set; }
        public ISimProcessModel ProcessModel { get; private set; }
        public ISimOffTimeModel OffTimeModel { get; private set; }

        public CarModelGroup(ISimulationModel simModel, ISimEquipmentModel eqpModel, ISimLotModel lotModel, ISimRouteModel routeModel, ISimDispatchModel dispatchModel, ISimProcessModel processhModel, ISimOffTimeModel offTimeModel)
        {
            SimulationModel = simModel;
            EquipmentModel = eqpModel;
            LotModel = lotModel;
            RouteModel = routeModel;
            DispatchModel = dispatchModel;
            ProcessModel = processhModel;
            OffTimeModel = offTimeModel;
        }
    }
}
