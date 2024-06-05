using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GarajYeri.Models
{
    public class VehicleProcess:BaseModel
    {
        public string? Description {  get; set; }
        public  double Odemeter {  get; set; }
        public double? Price { get; set; }
        public int VehicleProcessTypeId {  get; set; }
        public virtual VehicleProcessType VehicleProcessType { get; set; }
        public int VehicleId {  get; set; }
        [JsonIgnore]
        public virtual Vehicle Vehicle { get; set; }
    }
}
