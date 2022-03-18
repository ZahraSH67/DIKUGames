using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Events;
using DIKUArcade.Input;
namespace Galaga {
    public class Player :IGameEventProcessor {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float MOVEMENT_SPEED = 0.01f;

        // Player is the avatar of the ship the player controls
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape; 
        }
        public void Render() {
            entity.RenderEntity();
            //System.Console.WriteLine(shape.Position.X);
            
        }
        private void UpdateDirection () {
            shape.Direction.X = moveLeft + moveRight;
        }

        // makes player move left
        private void SetMoveLeft(bool val) {
            if (val == true) {
                moveLeft = -MOVEMENT_SPEED;
            } else {
                moveLeft = 0;
            } UpdateDirection();          
        }

        // makes player move right
        private void SetMoveRight(bool val) {
            if (val == true) {
                moveRight = +MOVEMENT_SPEED;
            } else {
                moveRight = 0;
            } UpdateDirection();
        }

        // moves the shape, and makes sure that the player does not
        // pass the borders of the canvas.
        public void Move() {
            if (shape.Direction.X >= 0 && shape.Position.X <= 0.9){
                shape.Move();
            } else if (shape.Direction.X <= 0 && shape.Position.X >= 0) {
                shape.Move(); 
            }        
        }

        // Gets the players current position
        public DIKUArcade.Math.Vec2F GetPosition() {
            return shape.Position;
        }

        // Makes the player turn to the left or right when the
        // correct keys are pressed.
        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left :
                    SetMoveLeft(true);
                    break;
                case KeyboardKey.Right :
                    SetMoveRight(true);
                    break;
            }
        }

        // Stops the player form moving when a key is released.
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left :
                    SetMoveLeft(false);
                    break;
                case KeyboardKey.Right : 
                    SetMoveRight(false);
                    break;   
            }
        }

        // Processes the key input.
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
    }
}
        