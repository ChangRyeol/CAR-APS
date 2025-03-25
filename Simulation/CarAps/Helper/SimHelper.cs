using DataMart.SqlMapper;
using CarApsModel.ModelConfig;
using CarApsModel;
using SimulationEngine.SimulationEntity;
using SimulationEngine.SimulationInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApsModel.Helper
{
    internal class SimHelper
    {
        internal static void InitializeMart(string connectionString)
        {
            InputMart.Initialize(connectionString);
            OutputMart.Initialize(connectionString);
            InputMart.Instance.LoadFromDatabase();
        }

        internal static IModelGroup CreateModelGroup()
        {
            return new CarModelGroup(
                new CarSimulationModel(),
                new CarEquipmentModel(),
                new CarLotModel(),
                new CarRouteModel(),
                new CarDispatchModel(),
                new CarProcessModel(),
                new CarOffTimeModel()
            );
        }

        internal static PhotoSimulationOption CreateSimulationOption()
        {
            return new PhotoSimulationOption
            {
                SimulationStartTime = DateTime.Today,
                SimulationEndTime = DateTime.Today.AddDays(7),
                DispatchType = DispatchType.FIFO,
                RunUser = "Engine"
            };
        }

        internal static void RunSimulation(IModelGroup modelGroup, PhotoSimulationOption option)
        {
            SimFactory.InitializeInstance(modelGroup, option);
            SimFactory factory = SimFactory.Instance;
            factory.Initialize(modelGroup);
            factory.StartSimulation();
        }
    }
}
