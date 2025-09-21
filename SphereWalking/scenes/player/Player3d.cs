using Godot;

public partial class Player3d : RigidBody3D
{

	[Export]
	public float RotationSpeed { get; set; } = 8.0f;

	[Export]
	public float Speed { get; set; } = 5.0f;

	[Export]
	public float JumpImpulse { get; set; } = 4.5f;

	private Vector3 _moveDirection = Vector3.Zero;
	private Vector3 _lastStringDirection = Vector3.Forward;
	private Vector3 _localGravity = Vector3.Down;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _IntegrateForces(PhysicsDirectBodyState3D state)
	{
		_localGravity = state.TotalGravity.Normalized();

		if (_moveDirection.Length() > 0.2)
		{
			_lastStringDirection = _moveDirection.Normalized();
		}

		_moveDirection = GetModelOrientedInput();
		OrientCharacterToDirection(_lastStringDirection, state.Step);

		if (IsJumping(state))
		{
			ApplyCentralImpulse(-_localGravity * JumpImpulse);
		}

		if (IsOnFloor(state))
		{
			ApplyCentralForce(_moveDirection * Speed);
		}
	}

	private Vector3 GetModelOrientedInput()
	{
		var inputLeftRight = Input.GetActionStrength("ui_left") - Input.GetActionStrength("ui_right");
		var inputForward = Input.GetActionStrength("ui_up");

		var rawInput = new Vector2(inputLeftRight, inputForward);

		var input = Vector3.Zero;

		input.X = rawInput.X * Mathf.Sqrt(1.0f - rawInput.Y * rawInput.Y / 2.0f);
        input.Z = rawInput.Y * Mathf.Sqrt(1.0f - rawInput.X * rawInput.X / 2.0f);

		input = Transform.Basis * input;

        return input;
	}

	private void OrientCharacterToDirection(Vector3 direction, float delta)
	{
		var leftAxis = -_localGravity.Cross(direction);
		var rotationBasis = new Basis(leftAxis, -_localGravity, direction).Orthonormalized();
        Transform.Basis.GetRotationQuaternion().Slerp(rotationBasis.GetRotationQuaternion().Normalized(), delta * RotationSpeed);
    }

	private bool IsJumping(PhysicsDirectBodyState3D state)
	{
		return false;
	}

	private bool IsOnFloor(PhysicsDirectBodyState3D state)
	{
		for (int i = 0; i < state.GetContactCount(); i++)
		{
			var contactNormal = state.GetContactLocalNormal(i);
			if (contactNormal.Dot(-_localGravity) > 0.5f)
			{
				return true;
			}
		}

		return false;
	}
}
