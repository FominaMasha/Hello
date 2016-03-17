using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Hello
{
    public class ContactsScreenAdapter : BaseAdapter<string>
    {
        List<Person> items;
        Activity context;
        public ContactsScreenAdapter(Activity context, List<Person> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override string this[int position]
        {
            get { return items[position].Surname + items[position].Name + items[position].Company + items[position].Phone; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
            var item = items[position];
            view.FindViewById<TextView>(Resource.Id.surname).Text = item.Surname;
            view.FindViewById<TextView>(Resource.Id.name).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.company).Text = item.Company;
            view.FindViewById<TextView>(Resource.Id.phone).Text = item.Phone;

            var callButton = view.FindViewById<ImageButton>(Resource.Id.buttonCall);
            callButton.SetImageResource(Resource.Drawable.ic_call_black_48dp);
            callButton.Click += delegate {
                var uri = Android.Net.Uri.Parse("tel:" + item.Phone);
                var intent = new Intent(Intent.ActionDial, uri);
                context.StartActivity(intent);
            };

            var smsButton = view.FindViewById<ImageButton>(Resource.Id.buttonSms);
            smsButton.SetImageResource(Resource.Drawable.ic_sms_black_48dp);
            smsButton.Click += delegate {
                var uri = Android.Net.Uri.Parse("smsto:" + item.Phone);
                var intent = new Intent(Intent.ActionSendto, uri);
                context.StartActivity(intent);
            };

            return view;
        }

    }
}