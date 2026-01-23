using System;
using System.Collections;
using UnityEngine;

namespace DBD.Notification
{
    [RequireComponent(typeof(GameNotificationsManager))]
    public abstract class BaseNotificationManager<INSTANCE> : MonoBehaviour
    {
        public static INSTANCE Instance { get; private set; }

        [SerializeField] private GameNotificationsManager manager;

        protected virtual void Reset()
        {
            LoadManager();
        }

        private void LoadManager()
        {
            if (manager == null)
            {
                manager = GetComponent<GameNotificationsManager>();
            }
        }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<INSTANCE>();
                LoadManager();

                Transform root = transform.root;
                if (root != transform)
                {
                    DontDestroyOnLoad(root);
                }
                else
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnEnable()
        {
            if (manager == null) return;
            manager.LocalNotificationDelivered += OnDelivered;
            manager.LocalNotificationExpired += OnExpired;
        }

        protected virtual void OnDisable()
        {
            if (manager == null) return;
            manager.LocalNotificationDelivered -= OnDelivered;
            manager.LocalNotificationExpired -= OnExpired;
        }

        protected virtual IEnumerator Start()
        {
            return manager.Initialize();
        }


        protected virtual void Update()
        {
        }


        protected virtual void FixedUpdate()
        {
        }

        protected virtual void OnApplicationFocus(bool hasFocus)
        {
        }

        protected virtual void OnExpired(PendingNotification obj)
        {
        }

        protected virtual void OnDelivered(PendingNotification obj)
        {
        }

        public virtual int ScheduleNotification(string title, string body, DateTime deliveryTime, int id = -1,
            int badgeNumber = 0, string data = "")
        {
            GameNotification notification = manager.CreateNotification();

            if (notification == null)
            {
                return -1;
            }

            notification.Title = title;
            notification.Body = body;
            notification.Data = data;
            notification.BadgeNumber = badgeNumber;
            if (id > 0)
            {
                notification.Id = id;
            }

            PendingNotification notificationToDisplay = manager.ScheduleNotification(notification, deliveryTime);
            notificationToDisplay.Reschedule = true;
            if (notificationToDisplay.Notification.Id != null)
            {
                return (int)notificationToDisplay.Notification.Id;
            }

            return id;
        }
    }
}