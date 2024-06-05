using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarajYeri.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }= Guid.NewGuid();
        public bool IsDeleted { get; set; }= false; 
        public bool IsActive {  get; set; }
        public DateTime DateCreated { get; set; }= DateTime.Now;
        public DateTime? DateDeleted {  get; set; }
    }
}
