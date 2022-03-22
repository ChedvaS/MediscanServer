
using Dal;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Net.Mail;
using System.Net;

namespace Bl
{
    public static class SendReminders
    {


        public static async Task ScheduleAsync()
        {

            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            List<REMINDERDETAILStbl> l_reminders_details = reminderdetailsDal.Getall(); //get all the reminders

            List<ITrigger> l_triggers;
            IJobDetail job;
            ITrigger trigger;
            Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> jobsAndTriggers = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            foreach (REMINDERDETAILStbl reminderDetails in l_reminders_details)
            {
                REMINDERStbl currentReminder = reminderDetails.REMINDERStbl.FirstOrDefault(x => x.IDDETAIL == reminderDetails.ID);
                string currentUser = currentReminder.GMAIL;
                Dictionary<object, object> dic = new Dictionary<object, object>();
                dic.Add("reminder details", reminderDetails);
                dic.Add("user name", currentUser);
                // define the job and tie it to our RemindingJob class
                job = JobBuilder.Create<SendRemindToEmail>().WithIdentity($"remindingJobOf{currentUser}", "group1").UsingJobData(new JobDataMap(dic)).Build();
                l_triggers = new List<ITrigger>();
                foreach (REMINDERStbl rEMINDERStbl in reminderDetails.REMINDERStbl)
                {
                    // Trigger the job to every day on the time the user had entered
                    
                    trigger = TriggerBuilder.Create().WithIdentity($"triggerFor{rEMINDERStbl.IDDETAIL}", $"{rEMINDERStbl.IDDETAIL}").WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(currentReminder.HOURTAKE.Value.Hour, currentReminder.HOURTAKE.Value.Minute)).ForJob(job).Build();
                    l_triggers.Add(trigger);
                }

                jobsAndTriggers.Add(job, l_triggers);
            }

            foreach (var jobAndTrigger in jobsAndTriggers)
            {
                //foreach (var currentTrigger in jobAndTrigger.Value)
                //{
                //    await scheduler.ScheduleJob(currentTrigger);
                //}
                await scheduler.ScheduleJob(jobAndTrigger.Key, jobAndTrigger.Value, true);
            }
            //await scheduler.ScheduleJob(jobsAndTriggers, true);
           
        }

    }
}
public class SendRemindToEmail : IJob
{

    public void SendEmail(string subject, string message, string email)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("mediscanne@gmail.com");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("mediscanne@gmail.com", "mediscanne12");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
        catch(Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }


    public async Task Execute(IJobExecutionContext context)
    {
        JobDataMap dataMap = context.JobDetail.JobDataMap;
        REMINDERDETAILStbl remindersDetails = (REMINDERDETAILStbl)dataMap.Get("reminder details");
        string userName = dataMap.GetString("user name");
        SendEmail(remindersDetails.SUBJECTGMAIL, remindersDetails.COMMENT, userName);
        Debug.WriteLine("hello");
    }
}







