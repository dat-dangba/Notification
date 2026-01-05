using System;
using TMPro;

namespace DBD.Notification.Sample
{
    public class NotificationManager : BaseNotificationManager<NotificationManager>
    {
        public TextMeshProUGUI text;

        /// <summary>
        /// Event fired when a queued local notification is cancelled because the application is in the foreground
        /// when it was meant to be displayed.
        /// </summary>
        protected override void OnExpired(PendingNotification obj)
        {
            base.OnExpired(obj);
            Log($"OnExpired {obj.Notification.Title} {obj.Notification.Body}");
        }

        /// <summary>
        /// Event fired when a scheduled local notification is delivered while the app is in the foreground.
        /// </summary>
        protected override void OnDelivered(PendingNotification obj)
        {
            base.OnDelivered(obj);
            Log($"OnDelivered {obj.Notification.Title} {obj.Notification.Body}");
        }

        private void Log(string s)
        {
            string t = text.text;
            text.text = t + "\n" + s;
        }

        public void ScheduleNoty()
        {
            ScheduleNotification("Title", "Body", DateTime.Now.AddSeconds(10));
        }
    }
}