/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are booleans that represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  
/// If there is no wall, then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    // Move left: Check if left is true, if yes, update _currX
    public void MoveLeft()
    {
        var currentPosition = (x: _currX, y: _currY);
        
        if (!_mazeMap.ContainsKey(currentPosition) || !_mazeMap[currentPosition][0]) // [0] is 'left'
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currX--; // Move left
    }

    // Move right: Check if right is true, if yes, update _currX
    public void MoveRight()
    {
        var currentPosition = (x: _currX, y: _currY);
        
        if (!_mazeMap.ContainsKey(currentPosition) || !_mazeMap[currentPosition][1]) // [1] is 'right'
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currX++; // Move right
    }    

    // Move up: Check if up is true, if yes, update _currY
    public void MoveUp()
    {
        var currentPosition = (x: _currX, y: _currY);
        
        if (!_mazeMap.ContainsKey(currentPosition) || !_mazeMap[currentPosition][2]) // [2] is 'up'
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currY--; // Move up
    }

    // Move down: Check if down is true, if yes, update _currY
    public void MoveDown()
    {
        var currentPosition = (x: _currX, y: _currY);
        
        if (!_mazeMap.ContainsKey(currentPosition) || !_mazeMap[currentPosition][3]) // [3] is 'down'
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currY++; // Move down
    }

    // Get the current status of the maze (current position)
    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
