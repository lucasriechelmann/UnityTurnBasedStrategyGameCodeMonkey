using System;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public bool Equals(GridPosition other) => this == other;
    public override bool Equals(object obj) => obj is GridPosition position &&
               x == position.x &&
               z == position.z;    
    public override int GetHashCode() => HashCode.Combine(x, z);
    public static bool operator ==(GridPosition a, GridPosition b) => (a == null && b == null) || (a.x == b.x && a.z == b.z);
    public static bool operator !=(GridPosition a, GridPosition b) => !(a == b);
    public static GridPosition operator +(GridPosition a, GridPosition b) => new(a.x + b.x, a.z + b.z);
    public static GridPosition operator -(GridPosition a, GridPosition b) => new(a.x - b.x, a.z - b.z);
    public static GridPosition operator *(GridPosition a, int multiplier) => new(a.x * multiplier, a.z * multiplier);
    public static GridPosition operator /(GridPosition a, int divisor) => new(a.x / divisor, a.z / divisor);
    public override string ToString() => $"(x: {x}, z: {z})";
}
