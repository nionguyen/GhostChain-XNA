using System;

namespace WindowsPhoneGame2
{
    public enum Cell_Type
    {
        None,
        Type01,
        Type02,
        Type03,
        Type04,
        Type05,
        Type06,
        Type07,
        Type08
    }

    public enum Drag_Type
    {
        Start,
        Drag,
        NoDrag,
        Done
    }

    public enum Cell_State
    {
        NoTouch,
        Touching,
        Action,
        WaitAction,
        Explosion,
        Die,
        Falling
    }

    public enum Grid_State
    {
        Wait,
        Touching,
        Explosion2,
        Explosion3,
        Explosion4,
        Explosion5,
        Explosion8,
        Falling
    }
}