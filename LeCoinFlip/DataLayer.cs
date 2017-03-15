using System;
using Quobject.SocketIoClientDotNet.Client;

namespace LeCoinFlip
{
	public class DataLayer
	{
		private int spinTimeLeft = 0;
		private static string APIendPoint = "http://192.168.1.25:3000/";
		private static Socket DataLayerSocket = IO.Socket(APIendPoint);

		public DataLayer()
		{

		}


		//DataLayerSocket.On("countDown", data =>
		//{

		//});

		public int getSpinTime()
		{
			return spinTimeLeft;
		}
	}
}