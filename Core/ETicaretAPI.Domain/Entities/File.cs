using ETicaretAPI.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretAPI.Domain.Entities
{
    public class File : BaseEntity
    {// |Table Per Hierarchy Yaklaşımını kullanırken base class/sınıf mız olacak

        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage {  get; set; }

        [NotMapped]
        public override DateTime UpdatedData { get => base.UpdatedData; set => base.UpdatedData = value; }
    }
}
