using System.Diagnostics;

namespace StrokeShapeBug;

public partial class MyCustomControl : ContentView
{
	public MyCustomControl()
	{
		Stopwatch s = new Stopwatch();
		s.Start();
		InitializeComponent();
		s.Stop();
		Debug.WriteLine("MyCustomControl(), ElapsedMilliseconds: " + s.ElapsedMilliseconds);
	}
}