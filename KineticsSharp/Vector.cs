using System;
public struct Vector
{
    public double X;
    public double Y;
    
    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector
        {
            X = v1.X + v2.X,
            Y = v1.Y + v2.Y
        };
    }
    public static Vector operator -(Vector v1, Vector v2)
    {
        return new Vector
        {
            X = v1.X - v2.X,
            Y = v1.Y - v2.Y
        };
    }
    public static Vector operator -(Vector v)
    {
        return new Vector
        {
            X = -v.X,
            Y = -v.Y
        };
    }
    public static Vector operator *(Vector v, double m)
    {
        return new Vector
        {
            X = v.X * m,
            Y = v.Y * m
        };
    }
    public static Vector operator *(double m, Vector v)
    {
        return v * m;
    }
    public static Vector operator /(Vector v, double d)
    {
        return new Vector
        {
            X = v.X / d,
            Y = v.Y / d
        };
    }

    public static double DotProduct(Vector v1, Vector v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y;
    }
    public static double CrossProduct(Vector v1, Vector v2)
    {
        return v1.X * v2.Y - v1.Y * v2.X;
    }
    public static Vector Rotate(Vector v, double rad)
    {
        return new Vector
        {
            X = v.X * Math.Cos(rad) - v.Y * Math.Sin(rad),
            Y = v.X * Math.Sin(rad) + v.Y * Math.Cos(rad)
        };
    }

    public double L2
    {
        get
        {
            return X * X + Y * Y;
        }
    }
    public double Length
    {
        get
        {
            return Math.Sqrt(L2);
        }
    }
    public Vector(double x, double y)
    {
        X = x;
        Y = y;
    }
}