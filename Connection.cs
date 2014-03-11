using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace WC
{

/**
 * \brief Handles the connection to a controller.
 */
public class Connection
{
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern IntPtr wcConnection_open( IntPtr devicePath );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern bool wcConnection_close( IntPtr connection );

	internal IntPtr handle;

	/**
	 * \brief Opens a new connection.
	 *
	 * \param devicePath The path to the controller's device.
	 * \exception FileNotFoundException The given device could not be opened.
	 */
	public Connection( string devicePath )
	{
		handle = wcConnection_open( Marshal.StringToHGlobalAnsi(devicePath) );
		if( handle == IntPtr.Zero )
			throw new FileNotFoundException( "Failed to open connection to \"" + devicePath + "\"" );
	}

	/**
	 * \brief Opens a new connection.
	 *
	 * \param configuration The controllers configuration.
	 * \exception FileNotFoundException The device could not be opened.
	 */
	public Connection( Configuration configuration ) : this( configuration.devicePath ) {}

	~Connection() { wcConnection_close( handle ); }
}

}
