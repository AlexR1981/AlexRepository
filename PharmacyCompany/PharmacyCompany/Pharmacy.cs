using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany
{
    /// <summary>
    /// Класс для Аптеки
    /// </summary>
    public class Pharmacy : BaseClass
    {
        private string _adress;
        private string _phone;
        public string adress
        {
            get { return _adress; }
            set { _adress = value; }
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public Pharmacy(int pId)
            : base(pId)
        {
        }
        public Pharmacy(string pname, string padress, string pphone)
            : base(pname)
        {
            this.adress = padress;
            this.phone = pphone;
        }
        public override void Save()
        {
            DbPharmacy.SetPharmacy(this);
        }
        public override void Delete(int pid)
        {
            DbPharmacy.DeleteDataDB("SP_DelPharmacy", this.id, "Не удалось удалить аптеку " + this.name);
        }
    }
}
