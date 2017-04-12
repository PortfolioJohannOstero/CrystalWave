using UnityEngine;
using System.Collections;

public static class ControlInput
{
#if UNITY_STANDALONE
    public static bool Tap()
    {
        return Input.GetButtonUp("Control Spirit");
    }

    public static string GetInputName()
    {
        return "Space";
    }

#endif

#if UNITY_ANDROID
    public static bool Tap()
    {
        if (!IsBeingTouched())
            return false;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return false;

        return true;
    }

    public static string GetInputName()
    {
        return "Tap";
    }

    private static bool IsBeingTouched()
    {
        return Input.touchCount > 0;
    }

#endif

}
