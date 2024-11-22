using System;

namespace DefaultNamespace
{
    public interface IMovementDirections
    {
        Action Forward { get; }
        Action Left { get; }
        Action Right { get; }
        Action Back { get; }
        Action Jump { get; }
        // Add more properties if you update InputLayout
    }
}