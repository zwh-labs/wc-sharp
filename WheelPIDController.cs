using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace WC
{

/**
 * \brief Implements a simple PID controller.
 *
 * This module can be used if the users application does not provide any mean to set rotations directly.
 * For example in a physics simulation only a torque may be applied to a simulated wheel.\n
 * To move the wheel according to the wheel signals of the incremental rotary encoder connected to the controller,
 * the application must track both absolute wheel angles - The accumulated increments of the real wheel and the simulated wheel's angle expressed as increments.\n
 * Both values can then be used as input for the PID controller (in this case updateAngular( int, int, uint, double ) ).
 * The output (the return value of the update function) can be applied as a torque to the simulated wheel.
 */
public class WheelPIDController
{
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern IntPtr wcWheelPIDController_new();
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern bool wcWheelPIDController_delete( IntPtr wheelPIDController );

	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern void wcWheelPIDController_reset( IntPtr wheelPIDController );

	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern double wcWheelPIDController_getProportionalGain( IntPtr wheelPIDController );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern double wcWheelPIDController_getIntegralGain( IntPtr wheelPIDController );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern double wcWheelPIDController_getDerivativeGain( IntPtr wheelPIDController );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern double wcWheelPIDController_getWindupGuard( IntPtr wheelPIDController );

	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern void wcWheelPIDController_setProportionalGain( IntPtr wheelPIDController, double gain );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern void wcWheelPIDController_setIntegralGain( IntPtr wheelPIDController, double gain );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern void wcWheelPIDController_setDerivativeGain( IntPtr wheelPIDController, double gain );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern void wcWheelPIDController_setWindupGuard( IntPtr wheelPIDController, double guard );

	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern double wcWheelPIDController_update( IntPtr wheelPIDController, double currentError, double delta );
	[DllImport("libwc",CallingConvention=CallingConvention.Cdecl)] private static extern double wcWheelPIDController_updateAngular( IntPtr wheelPIDController, int targetAngleIncrements, int actualAngleIncrements, uint incrementsPerTurn, double delta );

	internal IntPtr handle;

	public WheelPIDController() { handle = wcWheelPIDController_new(); }
	~WheelPIDController() { wcWheelPIDController_delete( handle ); }

	public double proportionalGain
	{
		get { return wcWheelPIDController_getProportionalGain( handle ); }
		set { wcWheelPIDController_setProportionalGain( handle, value ); }
	}

	public double integralGain
	{
		get { return wcWheelPIDController_getIntegralGain( handle ); }
		set { wcWheelPIDController_setIntegralGain( handle, value ); }
	}

	public double derivativeGain
	{
		get { return wcWheelPIDController_getDerivativeGain( handle ); }
		set { wcWheelPIDController_setDerivativeGain( handle, value ); }
	}

	public double windupGuard
	{
		get { return wcWheelPIDController_getWindupGuard( handle ); }
		set { wcWheelPIDController_setWindupGuard( handle, value ); }
	}

	public void reset()
	{
		wcWheelPIDController_reset( handle );
	}

	public double update( double currentError, double delta )
	{
		return wcWheelPIDController_update( handle, currentError, delta );
	}

	public double updateAngular( int targetAngleIncrements, int actualAngleIncrements, uint incrementsPerTurn, double delta )
	{
		return wcWheelPIDController_updateAngular( handle, targetAngleIncrements, actualAngleIncrements, incrementsPerTurn, delta );
	}
}

}
