using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany
{
    /// <summary>
    /// Базовый абстрактный класс для всех сущностей с основынми атрибутами и методами
    /// </summary>
    public abstract class BaseClass 
    {
        private string _name;        
        private int _id;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public BaseClass(string pname)
        {
            name = pname;
        }
        public BaseClass(int pId)
        {
            id = pId;
        }
        public virtual void Save()
        {         
        }
        public virtual void Delete(int id)
        {            
        }
    }
   
}
