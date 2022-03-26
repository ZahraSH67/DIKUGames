using Galaga.MovementStrategy;
using DIKUArcade.Entities;
using System.Collections.Generic;
using System;
using DIKUArcade.Math;
using Galaga;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        private float _amplitude = 0.05f;

        private float _period = 0.045f;

        //private float _movementSpeed = 0.0003f;
       //public float movementSpeed {
        //    get { return _movementSpeed; }
        //    set { _movementSpeed = value; }
        //}

        public void MoveEnemy(Enemy enemy) {
            var currP = enemy.Shape.Position;
            var startP = enemy.StartingPosition;

            var newP = new Vec2F(0, 0);

            newP.Y = currP.Y - enemy.MOVEMENT_SPEED;
            var sineThing = (float) Math.Sin(2 * Math.PI * (startP.Y - newP.Y) / _period);
            newP.X = startP.X + _amplitude * sineThing;

            enemy.Shape.SetPosition(newP);
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}