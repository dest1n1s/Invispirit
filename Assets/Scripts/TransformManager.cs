// <copyright file="TransformManager.cs" company="ECYSL">
//     Copyright (c) ECYSL. All rights reserved.
// </copyright>

using Assets.Scripts.Math;
using UnityEngine;

/// <summary>
/// The manager of the movement of rigidbody.
/// </summary>
public static class TransformManager
{
    /// <summary>
    /// Control the rigidbody to start move at a certain speed.
    /// </summary>
    /// <param name="rigidbody">The rigidbody.</param>
    /// <param name="direction">The direction to.</param>
    /// <param name="speed">The speed the rigidbody move.</param>
    public static void Move(this Rigidbody2D rigidbody, double direction, double speed)
    {
        Vector2 e = new Vector2((float)speed, 0).Rotate(direction);
        rigidbody.velocity = e;
    }
}