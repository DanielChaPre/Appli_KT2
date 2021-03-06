﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.FacebookClient;
using Java.Security;
using Plugin.GoogleClient;
using Plugin.LocalNotification;
using Rg.Plugins.Popup;
using Plugin.CurrentActivity;
//using Plugin.LocalNotifications;

namespace Appli_KT2.Droid
{
    [Activity(Label = "Appli_KT", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            PdfSharp.Xamarin.Forms.Droid.Platform.Init();

            base.OnCreate(savedInstanceState);

            GoogleClientManager.Initialize(this);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.FormsMaps.Init(this, savedInstanceState);

            FacebookClientManager.Initialize(this);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

           // LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.appli_kt_icono2;
            NotificationCenter.CreateNotificationChannel();

            LoadApplication(new App());

            NotificationCenter.NotifyNotificationTapped(Intent);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => {
                args.Handled = false;
            };
            #if DEBUG
                        PrintHashKey(this);
            #endif
        }

        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, intent);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
        }

        public static void PrintHashKey(Context pContext)
        {
            try
            {
                PackageInfo info = Android.App.Application.Context.PackageManager.GetPackageInfo(Android.App.Application.Context.PackageName, PackageInfoFlags.Signatures);
                foreach (var signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA");
                    md.Update(signature.ToByteArray());
                    System.Diagnostics.Debug.WriteLine(Convert.ToBase64String(md.Digest()));
                }
            }
            catch (NoSuchAlgorithmException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

    }
}