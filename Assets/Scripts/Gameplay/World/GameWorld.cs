using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    [SerializeField] private Vector2 bounds;
    [SerializeField] private float yOffset;
    private static GameWorld _instance;
    public static GameWorld Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameWorld>();
            }
            return _instance;
        }
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(bounds.x, 1, bounds.y));
    }

    public List<Vector3> GetAllGridPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 northwestCorner = GetNorthWestCorner();
        Vector3 southeastCorner = GetSoutheastCorner();

        for (int x = (int)northwestCorner.x; x < southeastCorner.x; x++)
        {
            for (int y = (int)northwestCorner.z; y > southeastCorner.z; y--)
            {
                positions.Add(new Vector3(x, yOffset, y));
            }
        }

        return positions;
    }

    public static Direction InvertDirection(Direction direction)
    {
        if (direction == Direction.West)
        {
            return Direction.East;
        }
        if (direction == Direction.North)
        {
            return Direction.South;
        }
        if (direction == Direction.East)
        {
            return Direction.West;
        }

        return Direction.North;
    }

    public static Direction PickRandomDirection()
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
        {
            return Direction.West;
        }
        if (rand == 1)
        {
            return Direction.North;
        }
        if (rand == 2)
        {
            return Direction.East;
        }

        return Direction.South;
    }

    public static Vector3 DirectionToVector(Direction direction)
    {
        if (direction == Direction.West)
        {
            return Vector3.left;
        }
        if (direction == Direction.North)
        {
            return Vector3.forward;
        }
        if (direction == Direction.East)
        {
            return Vector3.right;
        }

        return Vector3.back;
    }

    public Vector3 GetNorthWestCorner()
    {
        return new Vector3(Mathf.Round(-0.5f * bounds.x), yOffset, Mathf.Round(0.5f * bounds.y));
    }

    public Vector3 GetSoutheastCorner()
    {
        return new Vector3(0.5f * bounds.x, yOffset, -0.5f * bounds.y);
    }

    public Vector3 GetRandomBorderPosition(Direction border)
    {
        if (border == Direction.West)
        {
            return new Vector3(-0.5f * bounds.x, yOffset, GetRandomZValue());
        }
        if (border == Direction.North)
        {
            return new Vector3(GetRandomXValue(), yOffset, 0.5f * bounds.y);
        }
        if (border == Direction.East)
        {
            return new Vector3(0.5f * bounds.x, yOffset, GetRandomZValue());
        }

        return new Vector3(GetRandomXValue(), yOffset, -0.5f * bounds.y);
    }

    public float GetRandomXValue()
    {
        return Random.Range(-0.5f * bounds.x, 0.5f * bounds.x);
    }

    public float GetRandomZValue()
    {
        return Random.Range(-0.5f * bounds.y, 0.5f * bounds.y);
    }

    public Vector3 GetRandomWithinBoundsPosition()
    {
        float randX = GetRandomXValue();
        float randZ = GetRandomZValue();
        Vector3 randPos = new Vector3(Mathf.Round(randX), yOffset, Mathf.Round(randZ));
        return randPos;
    }
}
