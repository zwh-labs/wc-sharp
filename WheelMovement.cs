using System;
using System.Runtime.InteropServices;


namespace WC
{

/**
 * A structure representing wheel movements.
 */
[StructLayout(LayoutKind.Sequential)]
public struct WheelMovement
{
	public uint index { get; private set; }     ///< Channel index.
	public int increments { get; private set; } ///< The number of increments moved.
	public Error error { get; private set; }    ///< Error indicator.

	public WheelMovement( uint index ) : this() { this.index = index; }
	public void reset() { this.increments = 0; this.error = Error.NoError; }
	public void accumulate( int increment, Error error ) { this.increments += increment; this.error |= error; }
}

}
