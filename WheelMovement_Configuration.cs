using System;
using System.Runtime.InteropServices;


namespace WC
{

/**
 * Configuration specific wheel movement utility functions.
 */
public static class WheelMovement_Configuration
{
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern double wcWheelMovement_getTurns( ref WheelMovement wheelMovement, IntPtr configuration );

	/**
	 * \brief Return wheel movement as fraction of a full turn.
	 *
	 * \param wheelMovement Wheel movement.
	 * \param configuration Configuration.
	 * \return The wheel's movement as fraction of a full turn.
	 */
	public static double getTurns( this WheelMovement wheelMovement, Configuration configuration )
	{
		return wcWheelMovement_getTurns( ref wheelMovement, configuration.handle );
	}
}

}
