using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga;

namespace galagaTests
{
    [TestFixture]
    public class TestEnemy
    {
        private Enemy enemy;
        private GameEventBus eventBus;

        [SetUp]
        public void Setup(){
            Window.CreateOpenGLContext();
            Difficulty.InDifficulty();

            enemy = new enemy(
                new DynamicShape(0.5f, 0,5f, 0,1f, 0,1f),
                new ImageStride(2, Path.Combine("Assets", "Images", "BlueMonster.png")), 
                new Vec2F(0.5f, 0.5f));

            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.ControlEvent});
            eventBus.Subscribe(GameEventType.ControlEvent, enemy);  
        }
        
    }
}