using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using System.IO;
using System.Collections.Generic;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor
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
        private ZigZagDown downMove = new ZigZagDown();
        private Score scoreHandler;
        private Gamestate gamestate = new Gamestate();
        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // Player is the avatar of the ship the player controls
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));


            window.SetKeyEventHandler(KeyHandler);


            // eventBus takes input and transform it to actions
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            eventBus.Subscribe(GameEventType.InputEvent, this); 
            eventBus.Subscribe(GameEventType.InputEvent, player);

            // Enemy are the enemies. Here we add them to the canvas,
            // and have avatars for both regular blue and enraged red enemies
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            var images_red = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            Squadron1 squadron1 = new Squadron1(images,images_red);
            squadron1.Enemies.Iterate(enemy => Enemies.AddEntity(enemy));
            Squadron2 squadron2 = new Squadron2(images,images_red);
            squadron2.Enemies.Iterate(enemy => Enemies.AddEntity(enemy));
            Squadron3 squadron3 = new Squadron3(images,images_red);
            squadron3.Enemies.Iterate(enemy => Enemies.AddEntity(enemy));

            // playerShot is the avatar for the lasershots from the player
            // When the shots collide with the enemies they'll explode.
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosions = new AnimationContainer(MaxEnemies);
            explosionStrides = ImageStride.CreateStrides(8,Path.Combine("Assets", "Images", "Explosion.png"));

            // Shows the score the player gets on the screen.
            var scorePos = new Vec2F(0.5f, 0.0f);
            var scoreExt = new Vec2F(0.5f, 0.5f);
            
            scoreHandler = new Score(scorePos, scoreExt);
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                // Makes the players shot move up, and
                // delete the enemies when they've been shot enough.
                shot.Shape.Move();
                if (shot.Shape.Position.X < 0 || shot.Shape.Position.X > 1 || shot.Shape.Position.Y > 1) {
                    shot.DeleteEntity();
                } else {
                    // checks if the enemies has been hit and should be deleted in an explosion.
                    Enemies.Iterate(enemy => {
                        CollisionData col = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                        if (col.Collision == true){
                            enemy.hitPointsDecrease();
                            shot.DeleteEntity();
                            if (enemy.isAlive() == false) {
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                enemy.DeleteEntity();
                                
                                scoreHandler.AddPoints();
                                // virker ikke 
                                if (Enemies.CountEntities() == 1) {
                                    Enemies.ClearContainer();

                                    var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
                                    var images_red = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "RedMonster.png"));

                                    Squadron1 newSquad = new Squadron1(images, images_red);
                                    newSquad.Enemies.Iterate(enemy => Enemies.AddEntity(enemy));

                                    downMove.movementSpeed *= 2;
                                }
                            }                                             
                        }
                    });
                }
            });
        }

        // Keyhandler checks if a key is pressed and when it is released.
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
            if (gamestate.State == gs.gameActive) {
                player.Render();
                Enemies.RenderEntities();
                playerShots.RenderEntities();
                enemyExplosions.RenderAnimations();
            }

            scoreHandler.RenderScore();
        }

        public override void Update() {
            eventBus.ProcessEventsSequentially();
            
            if (gamestate.State == gs.gameActive) {
                player.Move();
                downMove.MoveEnemies(Enemies);
                IterateShots();
            }

            //gamestate.GameOver(Enemies);       
        }

        // KeyPress gives the program on what the program 
        // should do when a specific key is pressed.
        public void KeyPress(KeyboardKey key) {}
        // KeyRelease gives the program on what the program 
        // should do when a specific key is released.
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

        // ProcessEvent processes the the pressed keys and send the message
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
            // adds explosion to the AnimationContainer
            
            StationaryShape sh1 = new StationaryShape(position, extent);
            ImageStride img = new ImageStride(8,explosionStrides);
            enemyExplosions.AddAnimation(sh1, EXPLOSION_LENGTH_MS/8,img);
        }   
    }
}
