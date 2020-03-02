using System;
using Foundation;
using Plugin.GoogleClient;
using UIKit;
using UserNotifications;

namespace Appli_KT2.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Ask the user for permission to get notifications on iOS 10.0+
                UNUserNotificationCenter.Current.RequestAuthorization(
                    UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                    (approved, error) => {
                        if (approved)
                        {
                            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
                        }
                    });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                // Ask the user for permission to get notifications on iOS 8.0+
                var settings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            LoadApplication(new App());
            GoogleClientManager.Initialize("IzaSyAdfu7B4WIVn3G2HpfM8OE3PtXilqlWTaI");
            //FacebookClientManager.Initialize(app, options);
            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            base.OpenUrl(app, url, options);
            return GoogleClientManager.OnOpenUrl(app, url, options);
           // return FacebookClientManager.OpenUrl(app, url, options);
        }

        /*    public override void OnActivated(UIApplication uiApplication)
            {
                base.OnActivated(uiApplication);
                FacebookClientManager.OnActivated();
            }



            public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
            {
                // Convert iOS NSUrl to C#/netxf/BCL System.Uri

                return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);

            }*/
    }
}
