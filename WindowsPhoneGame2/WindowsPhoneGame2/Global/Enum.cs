
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
        ToTwo,
        ToThree,
        ToFour,
        ToFive,
        ToGold,
        ToFinal,
        Falling
    }

    public enum GameState
    {
        Running,
        PauseGame,
        BeforeEndGame,
        EndGame
    }

    public enum ModeScreenState
    {
        Select,
        Tutorial
    }

    public enum LevelScreenState
    {
        Wait,
        Moving
    }
    public enum GameMode
    {
        EasyClassic,
        NormalClassic,
        Level
    }

    public enum DirDrag
    {
        None,
        Left_Right,
        Right_Left,
        Top_Bot,
        Bot_Top
    }

}