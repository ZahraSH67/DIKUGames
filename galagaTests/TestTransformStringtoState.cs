using NUnit.Framework;
using Galaga;

namespace galagaTests;

public class TestTransformStringtoState
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMainMenu()
    {
        Assert.AreEqual(StateTransformer.TransformStringToState("MAIN_MENU"),GameStateType.MainMenu);
    }

    [Test]
    public void TestGameRunning()
    {
        Assert.AreEqual(StateTransformer.TransformStringToState("GAME_RUNNING"),GameStateType.GameRunning);
    }

    [Test]
    public void TestMainGamePaused()
    {
        Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"),GameStateType.GamePaused);
    }
}