using Entities;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;
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
        //פונקציה לשליפת הטקסט מתוך המדבקה
        public static string PullTextFromSticker(string path )
        {

            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Hebrew;
            Ocr.AddSecondaryLanguage(OcrLanguage.English);
            string s;
            using (var input = new OcrInput())
            {
                input.AddImage(path);
                // add image filters if needed 
                // In this case, even thought input is very low quality
                // IronTesseract can read what conventional Tesseract cannot.
                var Result = Ocr.Read(input);
                // Console can't print Arabic on Windows easily.  
                // Let's save to disk instead.
               // Result.SaveAsTextFile("Hebrew.txt");
                s = Result.Text;
            }

            return s;
         }
    }
}
