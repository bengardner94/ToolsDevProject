using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Maths
{
    public static float Magnitude(Vector2 a)
    {
        float magnitude = Mathf.Sqrt((a.x * a.x) + (a.y * a.y));
        return magnitude;
    }

    public static Vector2 Normalise(Vector2 a)
    {
        float mag = Magnitude(a);
        if (mag <= 0)
            return Vector2.zero;
        else
        {
            Vector2 normalised = new Vector2(a.x / mag, a.y / mag);
            return normalised;
        }
    }

    public static float Dot(Vector2 lhs, Vector2 rhs)
    {
        Vector2 nlhs = Normalise(lhs);
        Vector2 nrhs = Normalise(rhs);
        float dot = (nlhs.x * nrhs.x) + (nlhs.y * nrhs.y);
        return dot;
    }

    /// <summary>
    /// Returns the radians of the angle between two vectors
    /// </summary>
    public static float Angle(Vector2 lhs, Vector2 rhs)
    {
        float dot = Dot(lhs, rhs);
        float angle = Mathf.Acos(dot);
        return angle;
    }

    /// <summary>
    /// Translates a vector by X angle in degrees
    /// </summary>
    public static Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float newX = (vector.x * Mathf.Cos(radians)) - (vector.y * Mathf.Sin(radians));
        float newY = (vector.x * Mathf.Sin(radians)) + (vector.y * Mathf.Cos(radians));
        Vector2 rotation = new Vector2(newX, newY);
        return rotation;
    }
}

