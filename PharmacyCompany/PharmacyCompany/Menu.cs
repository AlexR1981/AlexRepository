using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany
{
    /// <summary>
    /// Базовый класс для Меню
    /// </summary>
    public class Menu
    {   
        private List<string> itemName = new List<string>(); //Список для строк меню
        public int currentItem;// номер текущей строки
        public int breakItem;  // номер строки для выхода из меню
        public int createItem; // номер строки для создания объекта
        public int deleteItem; // номер строки для удаления объекта

        public Menu(List<string> _itemName, int _breakItem, int _createItem, int _deleteItem) //Конструктор класса
        {
            this.itemName = _itemName;
            this.breakItem = _breakItem;
            this.createItem = _createItem;
            this.deleteItem = _deleteItem;
        }
        public int Display()//Вывод всех строк меню на экран
        { 
            int index = 0;
            foreach(string item in itemName)
            {
                if (index != 0)
                {
                    Console.WriteLine(index + ". " + item);
                }
                else
                {
                    Console.WriteLine(item);
                }
                index ++;
            }
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }
        public void Report()//Вывод отчета о количестве товара
        {
            int pharmacyID =0;
            Console.WriteLine("Введите ID аптеки:");
            var result = Console.ReadLine();
            Int32.TryParse(result.ToString(),out pharmacyID);
            
            DataTable dtReport = null;
            dtReport = DbPharmacy.GetDataDB("[SP_GetReportProductCount]", "Не удалось получить данные!", "@APharmacyID", pharmacyID);
            foreach (DataRow row in dtReport.Rows)
             {
                 Console.WriteLine(row["Pharmacy"].ToString() + "  " + row["Product"].ToString().Trim() + "   " + row["Pcount"].ToString().Trim());
             }
        }
        public void Show()//Вывод меню на экран и обработка выбранных пунктов меню
        {
            do 
            {
                ShowTable();
                this.currentItem = this.Display(); 
                if(this.currentItem == this.createItem) this.Create(); 
                if(this.currentItem == this.deleteItem) this.Delete();                 
            } 
            while (this.currentItem != this.breakItem);
        }
        public virtual void Create() //Виртуальный метод для создания объекта 
        {

        }
        public virtual void Delete() //Виртуальный метод для удаления объекта 
        {

        }
        public virtual void ShowTable() //Виртуальный метод для вывода таблицы
        {

        }       
    }
    /// <summary>
    /// Меню для товарных наименований
    /// </summary>
    public class ProductNameMenu : Menu 
    {        
        public ProductNameMenu(List<string> _itemName, int _breakItem, int _createItem, int _deleteItem)
            : base(_itemName, _breakItem, _createItem, _deleteItem)
        {
        }
        public override void Create()
        {
            Console.WriteLine("Введите наименование товара:");
            var result = Console.ReadLine();            
            ProductName productName = new ProductName(result.ToString());//Создаем объект с товарным наименованием
            productName.Save(); //Сохраняем в БД    
        }
        public override void Delete()
        {
            int delId=0;
            Console.WriteLine("Введите id товара для удаления:");
            var result = Console.ReadLine();
            Int32.TryParse(result,out delId);
            if(delId > 0)
            {
                ProductName productName = new ProductName(delId);
                productName.Delete(delId); //Удаляем
            }
            else
            {
                Console.WriteLine("Введено некорректное значение ID");
            }            
        }
        public override void ShowTable()
        {
            DataTable dtProduct = null;
            dtProduct = DbPharmacy.GetDataDB("Sp_GetProductNameData", "Не удалось получить данные о товарных наименованиях!");
            foreach (DataRow row in dtProduct.Rows)
            {
                Console.WriteLine(row["ID"].ToString().Trim() + "  " + row["Name"].ToString() );
            }
        }
      
    }
    /// <summary>
    /// Меню для аптеки
    /// </summary>
    public class PharmacyMenu : Menu
    {
        public PharmacyMenu(List<string> _itemName, int _breakItem, int _createItem, int _deleteItem)
            : base(_itemName, _breakItem, _createItem, _deleteItem)
        {
        }
        public override void Create()
        {
            string name, adress, phone;
            Console.WriteLine("Введите наименование аптеки:");
            var result = Console.ReadLine();
            name = result.ToString(); result = String.Empty;
            Console.WriteLine("Введите адрес аптеки:");
            result = Console.ReadLine(); 
            adress = result.ToString(); result = String.Empty;
            Console.WriteLine("Введите телефон аптеки:");
            result = Console.ReadLine();
            phone = result.ToString(); 
            Pharmacy pharmacy = new Pharmacy(name, adress, phone);//Создаем объект с аптекой
            pharmacy.Save(); //Сохраняем в БД                      
        }
        
        public override void Delete()
        {
            int delId = 0;
            Console.WriteLine("Введите id аптеки для удаления:");
            var result = Console.ReadLine();
            Int32.TryParse(result, out delId);
            if (delId > 0)
            {
                Pharmacy pharmacy = new Pharmacy(delId);
                pharmacy.Delete(delId); //Удаляем аптеку
            }
            else 
            {
                Console.WriteLine("Введено некорректное значение ID");
            }
        }
        public override void ShowTable()
        {
            DataTable dtPharmacy = null;
            dtPharmacy = DbPharmacy.GetDataDB("Sp_GetPharmacyData", "Не удалось получить данные об аптеках!");
            foreach (DataRow row in dtPharmacy.Rows)
            {
                Console.WriteLine(row["ID"].ToString().Trim() + "  " + row["Name"].ToString() + "   " + row["Adress"].ToString().Trim() + "   " + row["Phone"].ToString());
            }
        }
        
    }
    /// <summary>
    /// Меню для склада
    /// </summary>
    public class WareHouseMenu : Menu
    {
        public WareHouseMenu(List<string> _itemName, int _breakItem, int _createItem, int _deleteItem)
            : base(_itemName, _breakItem, _createItem, _deleteItem)
        {
        }
        public override void Create()
        {
            string name;
            int pharmacyID;
            Console.WriteLine("Введите наименование склада:");
            var result = Console.ReadLine();
            name = result.ToString(); result = String.Empty;
            Console.WriteLine("Введите ID аптеки:");
            result = Console.ReadLine();
            Int32.TryParse(result.ToString(),out pharmacyID); 
            
            WareHouse wareHouse = new WareHouse(name, pharmacyID);//Создаем объект со складом
            wareHouse.Save(); //Сохраняем в БД   
        }
        public override void Delete()
        {
            int delId = 0;
            Console.WriteLine("Введите id склада для удаления:");
            var result = Console.ReadLine();
            Int32.TryParse(result, out delId);
            if (delId > 0)
            {
                WareHouse warehouse = new WareHouse(delId);
                warehouse.Delete(delId); //Удаляем склад
            }
            else
            {
                Console.WriteLine("Введено некорректное значение ID");
            }
        }
        public override void ShowTable()
        {
            DataTable dtWareHouse = null;
            dtWareHouse = DbPharmacy.GetDataDB("Sp_GetWareHouseData", "Не удалось получить данные о складах!");
            foreach (DataRow row in dtWareHouse.Rows)
            {
                Console.WriteLine(row["ID"].ToString().Trim() + "  " + row["Name"].ToString() + "  " + row["Pharmacy"].ToString());
            }
        }
    }
    /// <summary>
    /// Меню для партии
    /// </summary>
    public class BatchMenu : Menu
    {
        public BatchMenu(List<string> _itemName, int _breakItem, int _createItem, int _deleteItem)
            : base(_itemName, _breakItem, _createItem, _deleteItem)
        {
        }
        public override void Create()
        {            
            int productNameID=0, wareHouseID=0, countProduct=0;
            Console.WriteLine("Введите id товара:");
            var result = Console.ReadLine();
            Int32.TryParse(result, out productNameID);  result = String.Empty;
            Console.WriteLine("Введите id склада:");
            result = Console.ReadLine();
            Int32.TryParse(result, out wareHouseID); ; result = String.Empty;
            Console.WriteLine("Введите количество товара:");
            result = Console.ReadLine();
            Int32.TryParse(result, out countProduct);
            Batch batch = new Batch(productNameID, wareHouseID, countProduct);//Создаем объект с партией
            batch.Save(); //Сохраняем в БД                 
        }
        public override void Delete()
        {
            int delId = 0;            
            Console.WriteLine("Введите id партии для удаления:");
            var result = Console.ReadLine();
            Int32.TryParse(result, out delId);
            if (delId > 0)
            {
                Batch batch = new Batch(delId);
                batch.Delete(delId); //Удаляем партию
            }
            else
            {
                Console.WriteLine("Введено некорректное значение ID");
            }
        }
        public override void ShowTable()
        {
            DataTable dtProduct = null;
            dtProduct = DbPharmacy.GetDataDB("Sp_GetBatchData", "Не удалось получить данные о партиях!");
            foreach (DataRow row in dtProduct.Rows)
            {
                Console.WriteLine(row["ID"].ToString().Trim() + "   " + row["ProductNameID"].ToString().Trim()+ "  " + row["ProductName"].ToString()+ "  " + row["WarehouseID"].ToString()+ "  " + row["ProductCount"].ToString());
            }
        }
    }

}
