using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
    public const float RotationSpeed = 1.0f;

    private Vector3 _moveDirection = new();
    private Vector3 _lastStrongDirection = new();

    private Vector3 _localGravity = new();

    private Vector3 _gravityVelocity = new();

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        _localGravity = GetGravity();

        if (_localGravity != Vector3.Zero)
        {
            Rotation = ((new Quaternion(-Transform.Basis.Y, _localGravity)) * Transform.Basis.GetRotationQuaternion()).Normalized().GetEuler();
        }
        
        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += _localGravity * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            velocity = _localGravity * JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity = direction * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

       // UpDirection = -_localGravity.Normalized();
        Velocity = velocity;
        MoveAndSlide();
    }

    private void OrientCharacterToDirection(Vector3 direction, float delta)
    {
        var leftAxis = -_localGravity.Cross(direction);
        var rotationBasis = new Basis(leftAxis, -_localGravity,  direction).Orthonormalized();
        //var quat = Transform.Basis.GetRotationQuaternion().Slerp(rotationBasis.GetRotationQuaternion().Normalized(), delta * RotationSpeed);
        Transform.Basis.Slerp(rotationBasis, delta * RotationSpeed);
    }
}
