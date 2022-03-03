using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga {


    public class Enemy : Entity {
        private Entity entity;
        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private float moveDown = 0.0f;
        private float speed = 0.5f;
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image) {}
    

        public void Render() {
            entity.RenderEntity();
        }
        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
            shape.Direction.Y = moveDown;
        }

        private void Move() {
            if (shape.Direction.X == moveLeft) {
                moveLeft = speed;
                shape.Move();
                UpdateDirection();
            } else if (shape.Direction.X == moveRight) {
                moveRight = speed;
                shape.Move();
                UpdateDirection();
            } else if (shape.Direction.Y == moveDown) {
                moveDown = speed;
                shape.Move();
                UpdateDirection();
            }
        }
    }

}