using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Hello
{
    [Activity(Label = "@string/Settings", Theme = "@style/MyCustomTheme")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "settings" layout resource
            SetContentView(Resource.Layout.Settings);

            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                //  клик на "стрелку назад"              
                case global::Android.Resource.Id.Home:
                    this.Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}