using UnityEngine;
using System.Collections;

namespace CustomInput
{
    public static class Input
    {
        public static Gyroscope gyro;
        public static Vector3 acceleration;
        public static DeviceOrientation deviceOrientation;

        public static Touch[] touches;
        public static int touchCount;

        public static Touch GetTouch(int index)
        {
            return touches[index];
        }
    }

    public struct Touch
    {
        public int fingerId;
        public Vector2 position;
        public Vector2 deltaPosition;
        public float deltaTime;
        public int tapCount;
        public TouchPhase phase;
    }
}