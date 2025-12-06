using AlienRun.scenes.lane;
using Godot;

public partial class Main : Node
{
    private Button _generatePointsButton;

    private Button _convexHullButton;

    private Button _addMidpointButton; 

    private Button _generateCurvesButton;

    private Button _generatePathButton;

    private Lane _lane;

    public override void _Ready()
	{
        _generatePointsButton = GetNode<Button>("Buttons/GeneratePointsButton");
        _convexHullButton = GetNode<Button>("Buttons/ConvexHullButton");
        _addMidpointButton = GetNode<Button>("Buttons/MidpointButton");
        _generateCurvesButton = GetNode<Button>("Buttons/GenerateCurvesButton");
        _generatePathButton = GetNode<Button>("Buttons/GeneratePathButton");

        _lane = GetNode<Lane>("Lane");
    }

    public void OnGeneratePointsButtonPressed()
    {
        _generatePointsButton.Disabled = true;
        _convexHullButton.Disabled = false;

        _lane.GenerateRandomPoints();
    }

    public void OnConvexHullButtonPressed()
    {
        _convexHullButton.Disabled = true;
        _addMidpointButton.Disabled = false;

        _lane.GenerateConvexHull();
    }

    public void OnMidpointButtonPressed()
    {
        _addMidpointButton.Disabled = true;
        _generateCurvesButton.Disabled = false;

        _lane.AddMidpoints();
    }


    public void OnGenerateCurvesButtonPressed()
    {
        _generateCurvesButton.Disabled = true;
        _generatePathButton.Disabled = false;

        _lane.GenerateCurves();
    }

    public void OnGeneratePathButtonPressed()
    { 
        _generatePathButton.Disabled = true;
        
        _lane.GeneratePath();
    }

    public void OnResetButtonPressed()
    {
        _generatePointsButton.Disabled = false;
        _convexHullButton.Disabled = true;
        _addMidpointButton.Disabled = true;
        _generateCurvesButton.Disabled = true;
        _generatePathButton.Disabled = true;

        _lane.Reset();
    }
}
