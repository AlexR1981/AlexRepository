using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany
{

    /// <summary>
    /// Класс для склада
    /// </summary>
    public class WareHouse : BaseClass
    {
        private int _pharmacyId;
        public int pharmacyId
        {
            get { return _pharmacyId; }
            set { _pharmacyId = value; }
        }
        public WareHouse(int pId)
            : base(pId)
        {
        }
        public WareHouse(string pname, int ppharmacyid)
            : base(pname)
        {
            pharmacyId = ppharmacyid;
        }
        public override void Save()
        {
            DbPharmacy.SetWareHouse(this);
        }
        public override void Delete(int pid)
        {
            DbPharmacy.DeleteDataDB("SP_DelWareHouse", this.id, "Не удалось удалить склад " + this.name);
        }
    }
}
