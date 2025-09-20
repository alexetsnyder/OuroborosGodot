using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
    public const float RotationSpeed = 1.0f;

    private Vector3 _moveDirection = new();
    private Vector3 _lastStrongDirection = new();

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

        if (_moveDirection.Length() > 0.2)
        {
            _lastStrongDirection = _moveDirection.Normalized();
        }

        // Handle Jump.
        //if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        //{
        //	velocity.Y = JumpVelocity;
        //}

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        //Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        //Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        //if (direction != Vector3.Zero)
        //{
        //	velocity.X = direction.X * Speed;
        //	velocity.Z = direction.Z * Speed;
        //}
        //else
        //{
        //	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        //	velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        //}

        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        _moveDirection = direction;

        //if (_lastStrongDirection == Vector3.Zero)
        //{
        //    _lastStrongDirection = _moveDirection.Normalized();
        //}
        OrientCharacterToDirection(_lastStrongDirection, (float)delta);

        Velocity = velocity + _moveDirection * Speed;
		MoveAndSlide();
	}

    private void OrientCharacterToDirection(Vector3 direction, float delta)
    {
        var localGravity = GetGravity();
        var leftAxis = -localGravity.Cross(direction);
        var rotationBasis = new Basis(leftAxis, -localGravity,  direction).Orthonormalized();
        Transform.Basis.GetRotationQuaternion().Slerp(rotationBasis.GetRotationQuaternion().Normalized(), delta * RotationSpeed);
    }
}
