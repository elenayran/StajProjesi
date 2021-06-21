using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables {

    public static int CurrentStage;
    public static int StageLevel = 1;
    public static float NormalTimeValue = 1f;
    public static float SlowTimeValue = 0.25f;
    public static float SlowTimeWaitingTime = 0f;

    public enum TouchTypes : byte {
        TAP,
        CIRCLE,
        SWIPE_UP,
        SWIPE_UP_RIGHT,
        SWIPE_UP_LEFT,
        SWIPE_DOWN,
        SWIPE_DOWN_RIGHT,
        SWIPE_DOWN_LEFT,
        SWIPE_RIGHT,
        SWIPE_LEFT,
        MOVE_UP,
        MOVE_UP_RIGHT,
        MOVE_UP_LEFT,
        MOVE_DOWN,
        MOVE_DOWN_RIGHT,
        MOVE_DOWN_LEFT,
        MOVE_RIGHT,
        MOVE_LEFT,
        ENDED,
        NONE
    }

    public enum GameState : byte {
        TAP_TO_PLAY,
        RUNNING,
        PAUSE,
        STAGE_COMPLETE,
        STAGE_FAILED
    }

    public enum Tags : byte {
        Player,
        Fire,
        Recruit,
        Obstacle,
        Fall,
        Wall
    }

    public enum PlayerState : byte {
        Idle,
        Moving,
        JumpingMove,
        Stumbling,
        Dying
    }

    public enum AnimTypes : byte {
        Idle,
        Running,
        Jumping,
        Die,
        Stumbling
    }

}