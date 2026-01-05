# DBD Notification

1. Add via Git URL: https://github.com/dat-dangba/Notification.git
2. Build Profiles -> Player Settings -> Mobile Notifications -> Android<p>
   Identifier: icon_small_notification<p>
   Type: Small<p>
   Select icon small notification (48x48, Read/Write)<p>
3. Custom LauncherManifest<p>
   Add meta-data in application<p>
   <meta-data android:name="com.google.firebase.messaging.default_notification_icon" android:resource="
   @drawable/icon_small_notification"/>