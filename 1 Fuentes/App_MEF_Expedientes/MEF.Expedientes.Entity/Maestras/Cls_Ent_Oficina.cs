using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MEF.Expedientes.Entity.Maestras
{
    [Table("V_OFICINA")]
    public class Cls_Ent_Oficina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID_OFICINA { get; set; }
        public string DESC_OFICINA { get; set; }
        public string DESC_CORTA { get; set; }
        public string ACRONIMO { get; set; }

    }
}
