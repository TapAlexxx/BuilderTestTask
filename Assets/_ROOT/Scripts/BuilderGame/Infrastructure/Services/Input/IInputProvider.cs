using UnityEngine;

namespace BuilderGame.Infrastructure.Services.Input
{
    public interface IInputProvider
    {
        Vector2 Axis { get; }
    }
}