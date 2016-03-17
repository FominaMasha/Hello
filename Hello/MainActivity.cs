using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Java.IO;
using Newtonsoft.Json;
using System;

namespace Hello
{
    [Activity(Label = "@string/NameApp", Theme = "@style/MyCustomTheme")]
    public class MainActivity : Activity
    {
        Button _connectButton;
        Person per = new Person("Вольман", "Денис", "ООО «ФТ-СОФТ»", "+79226046146");
        string FILENAME = "contacts.txt";
        const int DIALOG_ID = 0; //статическое число, однозначно идентифицирующее диалоговое окно
        
        private DrawerLayout m_DrawerLayout;
        private ListView m_RightDrawerList;
        private static readonly string[] Section = new[]
            {
                "Item1", "Item2", "Item3"
            };
        //private string m_DrawerTitle;
        //private string m_Title;


        protected override void OnCreate(Bundle bundle) 
        {
            base.OnCreate(bundle);
            //RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
           
            //m_Title = m_DrawerTitle = Title;
            m_DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            m_RightDrawerList = FindViewById<ListView>(Resource.Id.right_drawer);
            m_RightDrawerList.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Section);
            //m_DrawerList.ItemClick += DrawerListOnItemClick;

            // Get our button from the layout resource,
            // and attach an event to it
            _connectButton = FindViewById<Button>(Resource.Id.myButton);
            _connectButton.Click += ConnectButton_Click;

            Button tempButton = FindViewById<Button>(Resource.Id.tempButton);
            tempButton.Click += (o, e) => ShowDialog(0);

        }


        //private void DrawerListOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        //{
        //    Android.Support.V4.App.Fragment fragment = null;
        //    switch (itemClickEventArgs.Position)
        //    {
        //        case 0:
        //            fragment = new BrowseFragment();
        //            break;
        //        case 1:
        //            fragment = new FriendsFragment();
        //            break;
        //        case 2:
        //            fragment = new ProfileFragment();
        //            break;
        //    }

        //    SupportFragmentManager.BeginTransaction()
        //        .Replace(Resource.Id.content_frame, fragment)
        //        .Commit();

        //    m_DrawerList.SetItemChecked(itemClickEventArgs.Position, true);
        //    ActionBar.Title = m_Title = Sections[itemClickEventArgs.Position];
        //    m_Drawer.CloseDrawer(m_DrawerList);
        //}


        void ConnectButton_Click(object sender, EventArgs e)
        {
            if (_connectButton.Text != "@string/Hello")
            {
                _connectButton.Text = string.Format("Здравствуйте, Иван!"); // взятие имени из браслета по id
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
                    if (m_DrawerLayout.IsDrawerOpen(m_RightDrawerList))
                    {
                        //Right Drawer is already open, close it
                        m_DrawerLayout.CloseDrawer(m_RightDrawerList);
                    }

                    else
                    {
                        //Right Drawer is closed, open it 
                        m_DrawerLayout.OpenDrawer(m_RightDrawerList);                        
                    }                   
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

