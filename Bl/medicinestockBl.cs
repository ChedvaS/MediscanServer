using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Dal;

namespace Bl
{
    public class medicinestockBl


    {

        //שליפת רשימה
        public static List<medicinestockEntities> GetMedicineSList()
        {
            return medicinestockEntities.ConvertToListEntities(medicinestockDal.Getall());
        }
        //הוספה
        public static void addMedicinestock(medicinestockEntities mds)
        {
            medicinestockDal.AddMedicineStock(medicinestockEntities.ConvertToDb(mds));
        }
        // שליפת רשימת מלאי התרופות לפי קוד
        public static medicinestockEntities Getmedicinestock(int idMedicine)
        {
            return medicinestockEntities.convertToEntities(medicinestockDal.Getall().FirstOrDefault(x => x.ID==idMedicine));
        }

        //עדכון תרופות ברשימה
        public static void updateMedicine(medicinestockEntities m)
        {
            Dal.medicinestockDal.update(medicinestockEntities.ConvertToDb(m));
        }

    }
}
