using Entities;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;
using Microsoft.Recognizers.Text.Number;

namespace Bl
{
    public class medicineBl
    {
        //שליפת רשימת תרופות
        public static List<medicineEntities> GetMedicineList()
        {
            return medicineEntities.ConvertToListEntities(Dal.medicineDal.Getall());
        }

        ////פונקציה לשליפת תרופות לפי שם משתמש
        //public static List<string> GetNameMediByUserName(string gmail)
        //{
        //    return medicineEntities.convertToEntities(medicineDal.Getall().FirstOrDefault(x => x.USERNAME == gmail));
        //}
        
        //שליפת תרופה לפי קוד
        public static medicineEntities GetMedicineById(int idMedicine)
        {
            return medicineEntities.convertToEntities(medicineDal.Getall().FirstOrDefault(x => x.ID == idMedicine));
        }

        //הוספה תרופה לרשימה
        public static void addMedicine(medicineEntities m)
        {

            Dal.medicineDal.AddMedicine(medicineEntities.ConvertToDb(m));
        }

        //עדכון תרופות ברשימה
        public static void updateMedicine(medicineEntities m)
        {
            Dal.medicineDal.update(medicineEntities.ConvertToDb(m));
        }

        //הסרת תרופה מהרשימה
        public static void deleteMedicine(int id)
        {
            Dal.medicineDal.delete(id);
        }

        //פונקציה לשליפת הטקסט מתוך המדבקה
        public static Dictionary<string, short>  PullTextFromSticker(string path, string email)
        {
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Hebrew;
            Ocr.AddSecondaryLanguage(OcrLanguage.English);
            IronOcr.OcrResult.Line [] listText;
            string TextFromImg;
            using (var input = new OcrInput())
            {
                input.AddImage(path);
                var Result = Ocr.Read(input);
                TextFromImg = Result.Text;
                listText = Result.Lines;
            }
            return InsertDataFromImgToDataBase(listText, email);
        }

        //פונקציה להכנסת הנתונים לטבלאות המתאימות בדתה בייס, שקיבלנו מתוך הסריקה 
        public static Dictionary<string, short> InsertDataFromImgToDataBase(IronOcr.OcrResult.Line[] TextFromImg, string email)
        {
            //יצירת מילון לשמירת המפתחות של כל טבלא
            Dictionary<string, short> DicOfIdTables = new Dictionary<string, short>();
            //[]TextFromImg -הוא מערך המכיל את נתוני התמונה כל מקום מכיל שורה של התמונה
            //שם המבוטח
            string namePatient = TextFromImg[0].Text;//שורה ראשונה מכילה בתוכה את שם המבוטח
            List<string> ListOfWords = namePatient.Split(' ').ToList();//הפיכת השורה לרשימה 
            foreach (var word in ListOfWords.ToList())
            {
                if (word.Any(char.IsDigit))
                    ListOfWords.Remove(word);
                else
                    if (word.Any(x => x >= 'a' && x <= 'z' || x >= 'A' && x <= 'Z'))
                    ListOfWords.Remove(word);
                else
                    if (word.Contains("מבוטח"))
                    ListOfWords.Remove(word);
            }
            namePatient = ListOfWords[0] + " " + ListOfWords[1];

            //שם תרופה
            string namemedicine = TextFromImg[1].Text;
            //מינון
            string LineOfDosage = TextFromImg[2].Text; ;
            List<int> ListOfNum = ExtractNumFromString(LineOfDosage);
            int dosage = ListOfNum[0];
            //מספר פעמים ביום
            short AmountInDay = Convert.ToInt16(ListOfNum[1]);
            //מספר ימים
            short NumOfDays = Convert.ToInt16(ListOfNum[2]);
            // לעשות בדיקה על התאריך- תאריך
            string DateLine = TextFromImg[6].Text;
            List<int> ListOfDatePart = ExtractNumFromString(DateLine);
            DateTime DateInsert = new DateTime(ListOfDatePart[2] + 2000, ListOfDatePart[1], ListOfDatePart[0], 8, 0, 0);

            //הכנסת הנתונים שהתקבלו מן הסריקה לדתה בייס
            //הערות לאופן לקיחת התרופה
            string commentt = TextFromImg[3].Text;
            //הכנסת תרופה
            medicineEntities md = new medicineEntities()
            {
                nameMedicine = namemedicine,
                userName = email
            };
          
            //בדיקה האם המשתמש קיים עם אותה התרופה
            var medicineEntities = GetMedicineList().FirstOrDefault(x => x.nameMedicine == md.nameMedicine && x.userName == md.userName);
            short idmedicine;
            if (medicineEntities == null)//אם לא קיים מוסיף חדש
            {
                addMedicine(md);//הכנסת התרופה לדתה בייס  
                idmedicine = GetMedicineList().Last().id;
            }
            else//אם קיים לוקח את קוד התרופה הישן ושם אותו במה שנכנס
                idmedicine = medicineEntities.id;
            //מוסיף למילון את מפתח של התרופה
            DicOfIdTables.Add("idMedicne", idmedicine);
            //הכנסה לטבלת מלאי תרופות 
            medicinestockEntities mds = new medicinestockEntities()
            {
                idMedicne = idmedicine,
                insertDate = DateTime.Today
            };
            medicinestockBl.addMedicinestock(mds);
            //מוסיף למילון את מפתח של מלאי תרופות
            DicOfIdTables.Add("idMedicneStock", medicinestockBl.GetMedicineSList().Last().id);
            //הכנסה לטבלת פרטי תיזכורת
            reminderdetailsEntities rd = new reminderdetailsEntities()
            {
                idMedicineStock = medicinestockBl.GetMedicineSList().Last().id,
                subjectGmail = $"היי {namePatient} עלייך לקחת את התרופה: {namemedicine}",
                comment = commentt,
                amountDays = NumOfDays,
                frequincy = AmountInDay,
                dosage = dosage.ToString(),
                startDate = DateInsert
            };
            reminderdetailsBl.addReminderDetails(rd);
            short idreminderdetail = reminderdetailsBl.GetReminderDetailsList().Last().id;
            //מוסיף למילון את מפתח של פרטי תיזכורות
            DicOfIdTables.Add("Idreminderdetails", idreminderdetail);
            //יצירת כמות תיזכורות בהתאם לנתונים
            int IaddHours = 0;//משתנה להוספת שעות 
           
            for (int i = 0; i < AmountInDay; i++)
            {
                remindersEntities r = new remindersEntities()
                {
                    idDetail = idreminderdetail,
                    dateTake = DateInsert,
                    hourTake = DateInsert.AddHours(IaddHours),
                    gmail = email
                };
                remindersBl.addReminder(r);

                //מוסיף למילון את מפתח של תיזכורות
                DicOfIdTables.Add("Idreminder"+i+1,remindersBl.GetReminderList().Last().id);
                IaddHours += 24 / AmountInDay;
            }

            return DicOfIdTables;
        }
        //פונקציה לחילוץ מספרים מתוך מחרוזת 
        public static List<int> ExtractNumFromString(string s)
        {
            List<int> ListOfNum = new List<int>();
            string currentNum = "";
            foreach (var letter in s)
            {
                if (char.IsDigit(letter))
                {
                    currentNum += letter;
                }
                else
                {
                    if (currentNum != "")
                    {
                        ListOfNum.Add(int.Parse(currentNum));
                        currentNum = "";
                    }
                }
            }
            if (currentNum != "")
            {
                ListOfNum.Add(int.Parse(currentNum));
            }
            return ListOfNum;
        }

    }
}
