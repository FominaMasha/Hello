using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;
using Newtonsoft.Json;

namespace Hello
{
    [Activity(Label = "@string/NameApp", Theme = "@style/MyCustomTheme")]//назваие в Actionbar 
    public class MainActivity : Activity
    {
        Button _connectButton;
        Person per = new Person("Вольман", "Денис", "ООО «ФТ-СОФТ»", "+79226046146");
        string FILENAME = "contacts.txt";
        const int DIALOG_ID = 0; //статическое число, однозначно идентифицирующее диалоговое окно

        protected override void OnCreate(Bundle bundle) //метод создания самого layout
        {
            base.OnCreate(bundle);
            //RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            

            // Get our button from the layout resource,
            // and attach an event to it
            _connectButton = FindViewById<Button>(Resource.Id.myButton);
            _connectButton.Click += ConnectButton_Click;

            Button exampleButton = FindViewById<Button>(Resource.Id.exampleButton);
            exampleButton.Click += (o, e) => ShowDialog(0);

        }

        void ConnectButton_Click(object sender, EventArgs e)
        {
            if (_connectButton.Text != "@string/Hello")
            {
                _connectButton.Text = string.Format("Здравствуйте, Иван!"); // взятие имени из браслета по id(в идеале)
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent contactsIntent;
            switch (item.ItemId)
            {
                case Resource.Id.myContacts:
                    // Intent это объект, который обеспечивает связывание отдельных компонент во время выполнения (например, двух активити )
                    contactsIntent = new Intent(this, typeof(ContactsActivity));
                    StartActivity(contactsIntent);
                    break;
                case Resource.Id.settings:
                    contactsIntent = new Intent(this, typeof(SettingsActivity));
                    StartActivity(contactsIntent);
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }

        protected override Dialog OnCreateDialog(int id)
        {
            Dialog dialog;
            switch (id)
            {
                case DIALOG_ID:
                    var alertDialog = new AlertDialog.Builder(this);
                    alertDialog.SetTitle("Добавить в список контактов?");
                    alertDialog.SetCancelable(false);
                    alertDialog.SetNegativeButton("Нет", (EventHandler<DialogClickEventArgs>)null);
                    alertDialog.SetPositiveButton("Да", OnClickYes);
                    alertDialog.SetMessage(per.Surname + " " + per.Name + "\t\t" + per.Company + "\n"
                        + per.Phone);
                    dialog = alertDialog.Create();
                    break;
                default:
                    dialog = null;
                    break;
            }
            return dialog;
        }

        private void OnClickYes(object sender, DialogClickEventArgs e)
        {
            string serialized = JsonConvert.SerializeObject(per);

            try
            {
                // отрываем поток для записи
                BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(
                    OpenFileOutput(FILENAME, FileCreationMode.Append)));
                // пишем данные
                bw.Write(serialized);
                bw.Write("\n");
                // закрываем поток
                bw.Close();
            }
            catch (FileNotFoundException error)
            {
                error.PrintStackTrace();
            }
            catch (IOException error)
            {
                error.PrintStackTrace();
            }

        }
    }
}

