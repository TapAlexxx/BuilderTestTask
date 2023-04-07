using UnityEngine;

namespace BuilderGame.Infrastructure.Input
{
    public interface IInputProvider
    {
        Vector2 Axis { get; }
    }
}