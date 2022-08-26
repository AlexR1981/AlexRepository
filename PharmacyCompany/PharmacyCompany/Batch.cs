using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany
{
    /// <summary>
    /// Класс для партии
    /// </summary>
    public class Batch
    {

        private int _id;
        private int _productNameId;
        private int _wareHouseId;
        private int _productCount;
        public int productNameId
        {
            get { return _productNameId; }
            set { _productNameId = value; }
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int wareHouseId
        {
            get { return _wareHouseId; }
            set { _wareHouseId = value; }
        }
        public int productCount
        {
            get { return _productCount; }
            set { _productCount = value; }
        }
        public Batch(int pproductNameId, int pwareHouseId, int pproductCount)
        {
            productNameId = pproductNameId;
            wareHouseId = pwareHouseId;
            productCount = pproductCount;
        }
        public Batch(int pId)
        {
            id = pId;
        }
        public void Save()
        {
            DbPharmacy.SetBatch(this);
        }
        public void Delete(int pid)
        {
            DbPharmacy.DeleteBatch(pid);
        }
    }
}
