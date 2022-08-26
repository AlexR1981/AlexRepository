using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany
{
    class Program
    {
        static void Main(string[] args)
        {            
            Menu mainMenu = new Menu(new List<string> { "Главное меню:", "Товарные наименования", "Аптеки", "Склады", "Партии", "Отчет о количестве товара в выбранной аптеке","Выйти" }, 6,0,0);
            ProductNameMenu productNameMenu = new ProductNameMenu(new List<string> { "", "Создать товар", "Удалить товар", "Выйти" }, 3, 1, 2);
            PharmacyMenu pharmacyMenu = new PharmacyMenu(new List<string> { "", "Создать аптеку", "Удалить аптеку", "Выйти" }, 3, 1, 2);
            WareHouseMenu wareHouseMenu = new WareHouseMenu(new List<string> { "", "Создать склад", "Удалить склад", "Выйти" }, 3, 1, 2);
            BatchMenu batchMenu = new BatchMenu(new List<string> { "", "Создать партию", "Удалить партию", "Выйти" }, 3, 1, 2);
            
            do
            {                
                mainMenu.currentItem = mainMenu.Display();
                switch (mainMenu.currentItem)
                {
                    case 1: productNameMenu.Show(); break;
                    case 2: pharmacyMenu.Show(); break;
                    case 3: wareHouseMenu.Show(); break;
                    case 4: batchMenu.Show(); break;
                    case 5: mainMenu.Report(); break;
                   
                }
            } while (mainMenu.currentItem != mainMenu.breakItem);

        }
        
    }
}
