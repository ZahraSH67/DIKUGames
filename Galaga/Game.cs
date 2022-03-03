using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;

namespace Galaga
{
    public class Game : DIKUGame, IGameEventProcessor
    {
        private Player player;
        private GameEventBus eventBus;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // TODO: Set key event handler (inherited window field of DIKUGame class)
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);    
            
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, images)));
            }
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            
            enemyExplosions = new AnimationContainer(numEnemies);
            explosionStrides = ImageStride.CreateStrides(8,Path.Combine("Assets", "Images", "Explosion.png"));
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                // TODO: move the shot's shape
                shot.Shape.Move();
                if (shot.Shape.Position.X < 0 || shot.Shape.Position.X > 1 || shot.Shape.Position.Y > 1) {
                    shot.DeleteEntity();
                } else {
                    enemies.Iterate(enemy => {
                        CollisionData col = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                        if (col.Collision == true){
                            AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            shot.DeleteEntity();
                            enemy.DeleteEntity();                            
                        }
                    });
                }
            });
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            GameEvent gameEvent = new GameEvent();
            gameEvent.EventType = GameEventType.InputEvent;
            switch (action) {
                case KeyboardAction.KeyPress:
                    gameEvent.Message = "KeyPress";
                    break;
                case KeyboardAction.KeyRelease:
                    gameEvent.Message = "KeyRelease";
                    break;
            }
            gameEvent.IntArg1 = (int) key;
            ProcessEvent(gameEvent);
        } // TODO: Outcomment

        public override void Render() {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
        }

        public override void Update()
        {
            eventBus.ProcessEventsSequentially();
            player.Move();
            IterateShots();
        }
        public void KeyPress(KeyboardKey key) {
            // TODO: switch on key string and set the player's move direction
            switch (key) {
                case KeyboardKey.Left :
                    player.SetMoveLeft(true);
                    break;
                case KeyboardKey.Right :
                    player.SetMoveRight(true);
                    break;
            }
        }
        public void KeyRelease(KeyboardKey key) {
            // TODO: switch on key string and disable the player's move direction
            // TODO: Close window if escape is pressed
            switch (key) {
                case KeyboardKey.Left :
                    player.SetMoveLeft(false);
                    break;
                case KeyboardKey.Right : 
                    player.SetMoveRight(false);
                    break;
               case KeyboardKey.Space :
                    PlayerShot sht = new PlayerShot(
                        new DynamicShape(player.GetPosition(), new Vec2F(0.008f, 0.021f), new Vec2F(0.0f, 0.1f)),
                        playerShotImage);
                    playerShots.AddEntity(sht);
                    break;
                case KeyboardKey.Escape :
                    window.CloseWindow();
                    break;    
            }
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.InputEvent) {
                switch (gameEvent.Message) {
                    case "KeyPress":
                        KeyPress((KeyboardKey)gameEvent.IntArg1);
                        break;
                    case "KeyRelease":
                        KeyRelease((KeyboardKey)gameEvent.IntArg1);
                        break;
                default:
                    break;
                }
            }
        }
        public void AddExplosion(Vec2F position, Vec2F extent) {
            // TODO: add explosion to the AnimationContainer
            
            StationaryShape sh1 = new StationaryShape(position, extent);
            ImageStride img = new ImageStride(8,explosionStrides);
            enemyExplosions.AddAnimation(sh1, EXPLOSION_LENGTH_MS/8,img);
        }
    }
}
