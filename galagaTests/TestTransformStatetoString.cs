using NUnit.Framework;
using Galaga;

namespace galagaTests;

public class TestTransformStateToString
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMainMenu()
    {
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.MainMenu),"MAIN_MENU");
    }

    [Test]
    public void TestGameRunning()
    {
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameRunning),"GAME_RUNNING");
    }

    [Test]
    public void TestMainGamePaused()
    {
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GamePaused),"GAME_PAUSED");
    }
}