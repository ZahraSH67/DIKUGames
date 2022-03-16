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
using Galaga.Squadron;
using System;

namespace Galaga
{
    public class Game : DIKUGame, IGameEventProcessor, ISquadron
    {
        private Player player;
        private GameEventBus eventBus;
        public EntityContainer<Enemy> Enemies {get; private set;}
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        public int MaxEnemies {get;} = 10;
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
            eventBus.Subscribe(GameEventType.InputEvent, player);
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            var images_red = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            Squadron1 squadron1 = new Squadron1(images,images_red);
            squadron1.Enemies.Iterate(enemy => Enemies.AddEntity(enemy));
            Squadron2 squadron2 = new Squadron2(images,images_red);
            squadron2.Enemies.Iterate(enemy => Enemies.AddEntity(enemy));
            Squadron3 squadron3 = new Squadron3(images,images_red);
            squadron3.Enemies.Iterate(enemy => Enemies.AddEntity(enemy));
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosions = new AnimationContainer(MaxEnemies);
            explosionStrides = ImageStride.CreateStrides(8,Path.Combine("Assets", "Images", "Explosion.png"));
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                // TODO: move the shot's shape
                shot.Shape.Move();
                if (shot.Shape.Position.X < 0 || shot.Shape.Position.X > 1 || shot.Shape.Position.Y > 1) {
                    shot.DeleteEntity();
                } else {
                    Enemies.Iterate(enemy => {
                        CollisionData col = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                        if (col.Collision == true){
                            enemy.hitPointsDecrease();
                            shot.DeleteEntity();
                            if (enemy.isAlive() == false) {
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                enemy.DeleteEntity(); 
                            }                                             
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
            eventBus.RegisterEvent(gameEvent);
        }

        public override void Render() {
            player.Render();
            Enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
        }

        public override void Update()
        {
            eventBus.ProcessEventsSequentially();
            player.Move();
            IterateShots();
        }
        public void KeyPress(KeyboardKey key) {}
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
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
        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            for (int i = 0; i < MaxEnemies / 2; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.2f, 0.9f ), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
            for (int i = MaxEnemies / 2; i < MaxEnemies ; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.2f + (float)(i - (MaxEnemies / 2)) * 0.2f, 0.8f ), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
        }
        public void CreateEnemies2 (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            for (int i = 0; i < MaxEnemies / 2 ; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f - (float)i  * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
            for (int i = MaxEnemies / 2; i < MaxEnemies ; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.2f + (float)i  * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
        }
        public void CreateEnemies3
         (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            for (int i = 0; i < MaxEnemies / 2 ; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f - (float)i  * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
            for (int i = 0; i < MaxEnemies / 2 ; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.2f + (float)i * 0.1f, 0.9f - (float)i  * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
        }     
    }
}
