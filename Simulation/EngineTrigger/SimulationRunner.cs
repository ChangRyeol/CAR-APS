using DataMart.SqlMapper;
using CarApsModel.Helper;
using CarApsModel.ModelConfig;
using CarApsModel;
using SimulationEngine.SimulationEntity;
using SimulationEngine.SimulationInterface;

namespace EngineTrigger
{
    public class SimulationRunner
    {
        public void RunSimulation(PhotoSimulationOption option)
        {
            try {
                InputMart.Instance.LoadFromDatabase();
                InputMart.Instance.SetVersion(option.DispatchType.ToString());
                option.SimulationEndTime = option.SimulationStartTime.AddDays(option.SimulationPeriod);

                CarSimulationModel simulationModel = new CarSimulationModel();
                CarEquipmentModel equipmentModel = new CarEquipmentModel();
                CarLotModel lotModel = new CarLotModel();
                CarRouteModel routeModel = new CarRouteModel();
                CarDispatchModel dispatchModel = new CarDispatchModel();
                CarProcessModel processModel = new CarProcessModel();
                CarOffTimeModel offTimeModel = new CarOffTimeModel();

                IModelGroup modelGroup = new CarModelGroup(
                    simulationModel, equipmentModel, lotModel,
                    routeModel, dispatchModel, processModel, offTimeModel);

                OutputHelper.WriteEngineExecuteLog(option);

                SimFactory.InitializeInstance(modelGroup, option);
                SimFactory factory = SimFactory.Instance;
                factory.Initialize(modelGroup);
                factory.StartSimulation();
            }
            finally
            {
                SimFactory.ResetInstance();
                OutputMart.Instance.ClearData(); 
            }
        }
    }
}
