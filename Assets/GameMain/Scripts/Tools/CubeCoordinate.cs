using System;
using UnityEngine;

public struct CubeCoordinate
{
    public int Q { get; }
    public int R { get; }
    public int S { get; }

    public CubeCoordinate(int q, int r, int s)
    {
        if (q + r + s != 0)
        {
            throw new ArgumentException("The sum of q, r and s must be 0.");
        }

        Q = q;
        R = r;
        S = s;
    }

    public static int Distance(CubeCoordinate a, CubeCoordinate b)
    {
        return (Mathf.Abs(a.Q - b.Q) + Mathf.Abs(a.R - b.R) + Mathf.Abs(a.S - b.S)) / 2;
    }

    public static Vector2 GetHexagonCenter(CubeCoordinate cubeCoordinate, float size)
    {
        float x = (Mathf.Sqrt(3) * cubeCoordinate.Q + Mathf.Sqrt(3) / 2 * cubeCoordinate.R) * size;
        float y = (3f / 2f * cubeCoordinate.R) * size;

        return new Vector2(x, y);
    }
    public static CubeCoordinate GetCubeCoordinateFromPosition(Vector2 position, float hexSize)
    {
        var x = position.x;
        var y = position.y;
        var q = (x * (float)Math.Sqrt(3) / 3f - y / 3f) / hexSize;
        var r = y * 2f / 3f / hexSize;
        var z = -q - r;
        var rx = Math.Round(q);
        var ry = Math.Round(r);
        var rz = Math.Round(z);
        var xDiff = Math.Abs(rx - q);
        var yDiff = Math.Abs(ry - r);
        var zDiff = Math.Abs(rz - z);
        if (xDiff > yDiff && xDiff > zDiff) {
            rx = -(ry + rz);
        } else if (yDiff > zDiff) {
            ry = -(rx + rz);
        } else {
            rz = -(rx + ry);
        }
        return new CubeCoordinate((int)rx, (int)ry, (int)rz);
    }
    public static CubeCoordinate OffsetToCubeCoordinate(int col, int row)
    {
        int q = col - (row - (row & 1)) / 2;
        int r = row;
        int s = -q - r;

        return new CubeCoordinate(q, r, s);
    }
}