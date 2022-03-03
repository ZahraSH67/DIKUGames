using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;
namespace Galaga {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float MOVEMENT_SPEED = 0.01f;
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
        public void SetMoveLeft(bool val) {
            if (val == true) {
                moveLeft = -MOVEMENT_SPEED;
            } else {
                moveLeft = 0;
            } UpdateDirection();          
        }
        public void SetMoveRight(bool val) {
            if (val == true) {
                moveRight = +MOVEMENT_SPEED;
            } else {
                moveRight = 0;
            } UpdateDirection();
        }

        public void Move() {
            if (shape.Direction.X >= 0 && shape.Position.X <= 0.9){
                shape.Move();
            } else if (shape.Direction.X <= 0 && shape.Position.X >= 0) {
                shape.Move(); 
            }        
        }
        public DIKUArcade.Math.Vec2F GetPosition() {
            return shape.Position;
        }
    }
}