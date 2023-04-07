using UnityEngine;

namespace _ROOT.Scripts.BuilderGame.Infrastructure.Input
{
    public interface IInputProvider
    {
        Vector2 Axis { get; }
    }
}