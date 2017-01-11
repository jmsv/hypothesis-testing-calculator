using System;
using System.Threading;
using Org.Mentalis.Utilities;

class TestApp {
	static void Main(string[] args) {
		Timer t = null;
		Console.WriteLine("Press ENTER to exit");
		try {
			cpu = CpuUsage.Create();
			t = new Timer(new TimerCallback(TimerFunction), null, 0, 500);
		} catch (Exception e) {
			Console.WriteLine(e);
		}
		Console.ReadLine();
		if (t != null)
			t.Dispose();
	}
	static void TimerFunction(object o) {
		Console.WriteLine(cpu.Query().ToString() + "%");
	}
	private static CpuUsage cpu;
}