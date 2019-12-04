using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApplication1
{
	public class PacketType
	{
		public short Ping = 0x0001;					// ������ ������� 
		public short Echo = 0x0002;					// ���-����� (������������� ���������)
		public short Status = 0x0003;				// =status
		public short GetAwayMsg = 0x0006;			// ������ ��������� �������������
		public short AwayMsg = 0x0007;				// ��������� �������������
		public short Text = 0x0010;					// ����� ��������� ������ ���� �������
		public short GetLastTextTimeStamp = 0x0011;	// ������ timestamp ���������� ���������
		public short LastTextTimeStamp = 0x0012;		// ����� �� ������ � timestamp
		public short Chat = 0x0014;					// ����� ������ ����**
		public short NotifyType = 0x0020;			// ����������� ������ ������
		public short SoundCall = 0x0021;				// �����*
		public short NotifyTextClose = 0x0022;		// ����������� �������� ���������� ���� 
		public short RepeaterOn = 0x0030;			// ������� ���������
		public short RepeaterOff = 0x0031;			// ������� �����������
		public short RepeaterPing = 0x0032;			// ������ �������
	}

	public class PacketFlags
	{
		public short FromServer = 0x0001;			// ����� ������������� ��������
		public short Broadcast = 0x0002;				// ����������������� �����
		// {��������� ������� (���)}
		public short ServerBanText =0x0010;			// ������ ����� �����
		public short ServerBanSound = 0x0020;		// �� ��� ����������� ������ �������� ����
		public short ServerBanFiles = 0x0040;		// ���� ������ �������� �����
		// {��������� ������� �����������. ������ ���������� �������. �� ��� ������������}
		public short BanText = 0x0100;				// ������ ������������ �������� ������
		public short BanSound = 0x0200;				// ������ ������������ �������� �����
		public short BanFiles = 0x0400;				// ������ �������� �������� ������
	}

	public class StatusAll
	{
		public short Offline = 0;					// ������ ����������
		public short Normal = 1;					// idle
		public short Busy = 2;						// �����
		public short Away = 3;						// ��� �� ����� + ������������
		public short Invisible = 4;					// ������� (�� �������� ����� �� ����)* ���� ���

		public short HasWaveOut = 0x10;				// ������ ����� �������������� ����*
		public short HasWaveIn = 0x20;				// ������ ����� �������� ����*
		public short ReceivingSound = 0x40;			// ������ ������������� ����* 
		public short SendingSound = 0x80;			// ������ ������� ����*
		public short Chat = 0x100;					// � ������� ������� ���� ����
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
