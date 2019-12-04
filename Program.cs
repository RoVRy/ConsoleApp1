using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApplication1
{
	public class PacketType
	{
		public short Ping = 0x0001;					// запрос статуса 
		public short Echo = 0x0002;					// эхо-пакет (подтверждение получения)
		public short Status = 0x0003;				// =status
		public short GetAwayMsg = 0x0006;			// запрос сообщения автоответчика
		public short AwayMsg = 0x0007;				// сообщение автоответчика
		public short Text = 0x0010;					// пакет текстовой строки либо команды
		public short GetLastTextTimeStamp = 0x0011;	// запрос timestamp последнего сообщения
		public short LastTextTimeStamp = 0x0012;		// ответ на запрос о timestamp
		public short Chat = 0x0014;					// пакет текста чата**
		public short NotifyType = 0x0020;			// нотификация печати текста
		public short SoundCall = 0x0021;				// вызов*
		public short NotifyTextClose = 0x0022;		// нотификация закрытия текстового окна 
		public short RepeaterOn = 0x0030;			// репитер включился
		public short RepeaterOff = 0x0031;			// репитер выключается
		public short RepeaterPing = 0x0032;			// запрос статуса
	}

	public class PacketFlags
	{
		public short FromServer = 0x0001;			// пакет перенаправлен сервером
		public short Broadcast = 0x0002;				// широковещательный пакет
		// {параметры сервера (бан)}
		public short ServerBanText =0x0010;			// нельзя слать текст
		public short ServerBanSound = 0x0020;		// на уин отправителя нельзя посылать звук
		public short ServerBanFiles = 0x0040;		// уину нельзя посылать файлы
		// {параметры клиента отправителя. личная блокировка клиента. он сам заблокировал}
		public short BanText = 0x0100;				// клиент заблокировал передачу текста
		public short BanSound = 0x0200;				// клиент заблокировал передачу звука
		public short BanFiles = 0x0400;				// клиент запретил передачу файлов
	}

	public class StatusAll
	{
		public short Offline = 0;					// клиент выключился
		public short Normal = 1;					// idle
		public short Busy = 2;						// занят
		public short Away = 3;						// нет на месте + автоответчик
		public short Invisible = 4;					// невидим (не подавать ответ на пинг)* пока нет

		public short HasWaveOut = 0x10;				// клиент умеет воспроизводить звук*
		public short HasWaveIn = 0x20;				// клиент умеет посылать звук*
		public short ReceivingSound = 0x40;			// клиент воспроизводит звук* 
		public short SendingSound = 0x80;			// клиент передаёт звук*
		public short Chat = 0x100;					// у клиента открыто окно чата
	}

	public struct Packet
	{
		public short PacketID = 0x0411;
		public short PacketType;
		public int PacketFlags;
		public int FromUIN;
		public int ToUIN;
		public short StatusAll;
		public short DataSize;
		public int TimeStamp;
		public byte[] buffer = new byte[512];
	}
	
	class Program
	{
		static void Main(string[] args)
		{
		PacketType pt = new PacketType();
			IPEndPoint ep = new IPEndPoint(0x7f000001,8765);
			Socket a = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			a.Bind(ep);
			a.Send(pt.SoundCall);
		}
	}
}
