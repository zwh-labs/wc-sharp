using System;
using WC;


public class ExampleThread
{
	static public void Main()
	{
		Configuration configuration = new Configuration();
//		configuration.setDevicePath("COM3");
		configuration.devicePath = "/dev/pts/4";
		configuration.setWheel( 0, 4096 );
		configuration.setWheel( 1, 4096 );
		Console.WriteLine( configuration );

		Connection connection = new Connection( configuration );
		Thread thread = new Thread( connection );

		while( true )
		{
			System.Threading.Thread.Sleep( 1000 );
			for( uint i = 0; i < thread.wheelCount; i++ )
			{
				WheelMovement wm = thread.retrieveWheelMovement( i );
				Console.WriteLine("Wheel:\tchannel=" + i + "\terror=" + wm.error + "\tvalue=" + wm.increments + "\tturn=" + wm.getTurns( configuration ) );
			}
		}
	}
}
