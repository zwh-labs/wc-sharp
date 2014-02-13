using System;
using System.Runtime.InteropServices;


namespace WC
{

public class Thread
{
	[DllImport("libwc")] private static extern IntPtr wcThread_start( IntPtr connection );
	[DllImport("libwc")] private static extern bool wcThread_stop( IntPtr thread );

	[DllImport("libwc")] private static extern bool wcThread_isRunning( IntPtr thread );

	[DllImport("libwc")] private static extern WheelMovement wcThread_retrieveWheelMovement( IntPtr thread, uint index );
	[DllImport("libwc")] private static extern uint wcThread_getWheelCount( IntPtr thread );

	internal IntPtr handle;

	/**
	 * \brief The number of tracked wheels.
	 */
	public uint wheelCount
	{
		get { return wcThread_getWheelCount( handle ); }
	}

	/**
	 * \brief True if the thread is still running.
	 */
	public bool isRunning
	{
		get { return wcThread_isRunning( handle ); }
	}

	/**
	 * \brief Starts a new thread listening on a connection.
	 *
	 * \param connection The connection to listen on.
	 */
	public Thread( Connection connection ) { handle = wcThread_start( connection.handle ); }
	~Thread() { wcThread_stop( handle ); }

	/**
	 * \brief Fetches the accumulated wheel movements.
	 *
	 * Returns the accumulated wheel movements for the given wheel index since the last call using the same index.
	 * \param index The wheel's index to retrieve.
	 * \return The accumulated wheel movements.
	 */
	public WheelMovement retrieveWheelMovement( uint index ) { return wcThread_retrieveWheelMovement( handle, index ); }
}

}
