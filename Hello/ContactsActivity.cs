using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hello
{
    [Activity(Label = "@string/Mycontacts", Theme = "@style/MyCustomTheme")]
    public class ContactsActivity : Activity
    {
        string FILENAME = "contacts.txt";
        List<Person> myContacts = new List<Person>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "contacts" layout resource
            SetContentView(Resource.Layout.Contacts);

            

            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            //this.ActionBar.SetIcon(Resource.Drawable.ic_snake_handslogostroke);

            ListView listView = FindViewById<ListView>(Resource.Id.listView1);
            //listView.FastScrollEnabled = true;
            try
            {
                // открываем поток для чтения
                BufferedReader br = new BufferedReader(new InputStreamReader(
                    OpenFileInput(FILENAME)));
                string str = "";
                // читаем содержимое
                while ((str = br.ReadLine()) != null)
                {
                    myContacts.Add(JsonConvert.DeserializeObject<Person>(str));
                }
            }
            catch (FileNotFoundException e)
            {
                e.PrintStackTrace();
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }

            ContactsScreenAdapter adapter = new ContactsScreenAdapter(this, myContacts);
            listView.SetAdapter(adapter);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                // Клик на "стрелку назад"              
                case global::Android.Resource.Id.Home:
                    this.Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}