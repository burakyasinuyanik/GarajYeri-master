using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarajYeri.Models
{
    public class Policy:BaseModel
    {
        public string Name {  get; set; }
        public string CompanyName {  get; set; }
        public string Validity {  get; set; }
        public string? FilePath { get; set; }

        public int PolicyTypeId {  get; set; }
        public virtual PolicyType PolicyType { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
