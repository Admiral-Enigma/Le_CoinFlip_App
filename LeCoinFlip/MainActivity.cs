using Android.App;
using Android.Widget;
using Android.OS;
using Quobject.SocketIoClientDotNet.Client;
using System;
using Newtonsoft.Json.Linq;
using Android.Webkit;
using Android.Content.PM;

namespace LeCoinFlip
{
	[Activity(Label = "Le Coin Flip", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]

	public class MainActivity : Activity
	{
		public TextView countDown;
		private DataLayer datalayer = new DataLayer();


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button>(Resource.Id.myButton);
			TextView connectedStatus = FindViewById<TextView>(Resource.Id.statusText);
			countDown = FindViewById<TextView>(Resource.Id.countDown);
			WebView webView = FindViewById<WebView>(Resource.Id.webView);

			webView.Settings.JavaScriptEnabled = true;
			webView.LoadUrl("http://192.168.1.25:3000/spinner");

			var socket = IO.Socket("http://192.168.1.25:3000/");

			socket.On("countDown", data =>
			{
				//timeLeft = data; //json.Value<int>("time");
				editCountDown("Time to next spin: " + data);
			});

			socket.On("spin", data =>
			{
				editCountDown("Time to next spin: Spinning...");
			});

			socket.On(Socket.EVENT_CONNECT, () =>
			{
				RunOnUiThread(() =>
				{
					connectedStatus.Text = "Status: Connected";
				});
			});

			socket.On(Socket.EVENT_DISCONNECT, () =>
			{
				RunOnUiThread(() =>
				{
					connectedStatus.Text = "Status: Not connected";
				});
			});
		}

		public void editCountDown(string text)
		{
			RunOnUiThread(() =>
			{
				countDown.Text = text;
			});
		}
	}
}
