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
        public static string PullTextFromSticker(string path,string email)
        {
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Hebrew;
            Ocr.AddSecondaryLanguage(OcrLanguage.English);

            string TextFromImg;
            using (var input = new OcrInput())
            {
                input.AddImage(path);
                var Result = Ocr.Read(input);
                TextFromImg = Result.Text;
                var listText = Result.Lines;
                InsertDataFromImgToDataBase(listText,email);
            }
            return TextFromImg;
        }

        //פונקציה להכנסת הנתונים לטבלאות המתאימות בדתה בייס, שקיבלנו מתוך הסריקה 
        public static void InsertDataFromImgToDataBase(IronOcr.OcrResult.Line[] TextFromImg, string email)
        {
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
            DateTime DateInsert = new DateTime(ListOfDatePart[2] + 2000, ListOfDatePart[1], ListOfDatePart[0]);

            //הכנסת הנתונים שהתקבלו מן הסריקה לדתה בייס
            //הערות לאופן לקיחת התרופה
            string commentt = TextFromImg[3].Text;
            //הכנסת תרופה
            medicineEntities md = new medicineEntities()
            {
                nameMedicine = namemedicine,
                userName = email
            };
            addMedicine(md);//הכנסת התרופה לדתה בייס
            //הכנסה לטבלת מלאי תרופות 
            medicinestockEntities mds = new medicinestockEntities()
            {
                idMedicne = GetMedicineList().Last().id,
                insertDate = DateTime.Today
            };
            medicinestockBl.addMedicinestock(mds);
            //הכנסה לטבלת פרטי תיזכורת
            reminderdetailsEntities rd = new reminderdetailsEntities()
            {
                idMedicineStock = medicinestockBl.GetMedicineSList().Last().id,
                subjectGmail = $"היי {namemedicine} עלייך לקחת את התרופה: {namemedicine}",
                comment = commentt,
                amountDays = NumOfDays,
                frequincy = AmountInDay,
                dosage = dosage.ToString(),
                startDate = DateInsert
            };
            reminderdetailsBl.addReminderDetails(rd);

            int index = 0;//אינדקס שימספר את מספר הפעמים שנכתב תאריך בתיזכורות האינדקס גדל עד ששוה למספר הפעמים ביום ואז מגדיל  את התאריך של התיזכורת ליום הבא 
            //יצירת כמות תיזכורות בהתאם לנתונים
            for (int i = 0; i < NumOfDays * AmountInDay; i++)
            {
                if (index > AmountInDay)
                {
                    DateInsert = DateInsert.AddDays(1);
                    index = 0;
                }
                remindersEntities r = new remindersEntities()
                {
                    idDetail = reminderdetailsBl.GetReminderDetailsList().Last().id,
                    dateTake = DateInsert,
                    hourTake = DateInsert.AddHours(24 / AmountInDay)
                };
                index++;
                remindersBl.addReminderDetails(r);
            }
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
