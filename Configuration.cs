using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace WC
{

/**
 * \brief Controller configuration
 *
 * The configuration is used to specify:
 *  - how to connect to a controller
 *  - how many wheels there are
 *  - and how each wheel is configured.
 */
public class Configuration
{
	[DllImport("libwc")] private static extern IntPtr wcConfiguration_new();
	[DllImport("libwc")] private static extern bool wcConfiguration_delete( IntPtr configuration );

	[DllImport("libwc")] private static extern IntPtr wcConfiguration_getDevicePath( IntPtr configuration );
	[DllImport("libwc")] private static extern uint wcConfiguration_getWheelCount( IntPtr configuration );
	[DllImport("libwc")] private static extern uint wcConfiguration_getWheelIncrementsPerTurn( IntPtr configuration, uint wheelIndex );
	[DllImport("libwc")] private static extern void wcConfiguration_setDevicePath( IntPtr configuration, IntPtr devicePath );
	[DllImport("libwc")] private static extern bool wcConfiguration_setWheel( IntPtr configuration, uint wheelIndex, uint incrementsPerTurn );

	[DllImport("libwc")] private static extern int wcConfiguration_snprint( IntPtr s, UIntPtr n, IntPtr configuration );

	internal IntPtr handle;

	public Configuration() { handle = wcConfiguration_new(); }
	~Configuration() { wcConfiguration_delete( handle ); }

	/**
	 * \brief The device path.
	 *
	 * This is a platform specific string identifying a path to the controller's device.\n
	 * On Linux this might for example be "/dev/ttyACM0" - or "COM1" on Windows.\n
	 */
	public string devicePath
	{
		get { return Marshal.PtrToStringAnsi( wcConfiguration_getDevicePath( handle ) ); }
		set { wcConfiguration_setDevicePath( handle, Marshal.StringToHGlobalAnsi( value ) ); }
	}

	/**
	 * \brief The number of wheels configured.
	 *
	 * This will return the highest index used in a call to setWheel( uint wheelIndex, uint incrementsPerTurn ) + 1.
	 */
	public uint wheelCount
	{
		get { return wcConfiguration_getWheelCount( handle ); }
	}

	/**
	* \brief Returns the number of increments per turn of a wheel.
	*
	* Returns the number of increments per turn for the given wheel's index previously set by setWheel( uint wheelIndex, uint incrementsPerTurn ) .
	* \param wheelIndex Index of the wheel.
	* \return The number of increments per turn for the given wheel's index.
	*/
	public uint getWheelIncrementsPerTurn( uint wheelIndex ) { return wcConfiguration_getWheelIncrementsPerTurn( handle, wheelIndex ); }

	/**
	* \brief Sets the configuration of a wheel.
	*
	* Each wheel's incremental rotary encoder generates a fixed number of increments for a full turn.\n
	* Set number of increments for each wheel using this function.\n
	* \note This function will allocate memory for each wheel's index up to the given index if it is not yet configured.
	* \param wheelIndex Index of the wheel.
	* \param incrementsPerTurn The number of increments per turn for this wheel.
	* \return True on success.
	*/
	public bool setWheel( uint wheelIndex, uint incrementsPerTurn ) { return wcConfiguration_setWheel( handle, wheelIndex, incrementsPerTurn ); }

	/**
	 * \brief Returns a string representation of this configuration.
	 */
	public override string ToString()
	{
		int size = wcConfiguration_snprint( IntPtr.Zero, UIntPtr.Zero, handle );
		if( size < 0 )
			throw new Exception();
		size += 1;	// + 1 for terminating '\0'
		IntPtr buf = Marshal.AllocHGlobal( size );
		wcConfiguration_snprint( buf, (UIntPtr)size, handle );
		string ret = Marshal.PtrToStringAnsi( buf );
		Marshal.FreeHGlobal( buf );
		return ret;
	}
}

}
