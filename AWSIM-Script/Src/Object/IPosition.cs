using System;
namespace AWSIM_Script.Object
{
	// abstract type for position.
	// So far, we use {lane, offset} to specify the position.
	// In future, we might add other method to specify it.
	public interface IPosition
	{
        string LaneName { get; }
        float Position { get; }
    }
}

