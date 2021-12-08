using Entities;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
  public  class medicineBl
    {
        //שליפת רשימת תרופות
        public static List<medicineEntities> GetMedicineList()
        {
            return medicineEntities.ConvertToListEntities(medicineDal.Getall());
        }

        //שליפת תרופה לפי קוד
        public static medicineEntities GetMedicineById(int idMedicine)
        {
            return medicineEntities.convertToEntities(medicineDal.Getall().FirstOrDefault(x => x.ID == idMedicine));
        }

        //הוספה תרופה לרשימה
        public static void addMedicine(medicineEntities m)
        {
            medicineDal.AddMedicine(medicineEntities.ConvertToDb(m));
        }

        //עדכון תרופות ברשימה
        public static void updateMedicine(medicineEntities m)
        {
            medicineDal.update(medicineEntities.ConvertToDb(m));
        }

        //הסרת תרופה מהרשימה
        public static void deleteMedicine(int id)
        {
            medicineDal.delete(id);
        }
    }
}
