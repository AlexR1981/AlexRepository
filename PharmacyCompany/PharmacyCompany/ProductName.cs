using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany
{
    /// <summary>
    /// Класс для товарного наименования
    /// </summary>
    public class ProductName : BaseClass
    {
        public ProductName(string name)
            : base(name)
        {
        }
        public ProductName(int pId)
            : base(pId)
        {
        }
        public override void Save()
        {
            DbPharmacy.SetProductName(this);
        }
        public override void Delete(int pid)
        {
            DbPharmacy.DeleteDataDB("SP_DelProductName", this.id, "Не удалось удалить товарное наименование " + this.id);
        }
    }
}
