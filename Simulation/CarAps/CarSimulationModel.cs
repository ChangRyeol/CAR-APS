using DataMart.SqlMapper;
using SimulationEngine.SimulationInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApsModel
{
    public class CarSimulationModel : ISimulationModel
    {
        public void OnBeginInitialize() => Console.WriteLine("[Model] OnBeginInitialize triggered.");
        public void OnEndInitialize() => Console.WriteLine("[Model] OnEndInitialize triggered.");
        public void OnStart() => Console.WriteLine("[Model] OnStart triggered.");
        public void OnDayChanged(DateTime date) => Console.WriteLine($"[Model] OnDayChanged triggered for {date.ToShortDateString()}.");
        public void OnDone() {
            Console.WriteLine("[Model] OnDone triggered. DB Save Start ...");
            OutputMart.Instance.SaveAll();
            Console.WriteLine("[Model] OnDone triggered. DB Save End");
        }
    }
}
