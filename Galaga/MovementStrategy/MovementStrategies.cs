using Galaga.MovementStrategy;
using DIKUArcade.Entities;
using System.Collections.Generic;
using System;
using DIKUArcade.Math;
using Galaga;

namespace Galaga.MovementStrategy {
    public class NoMove : IMovementStrategy {
        public void MoveEnemy (Enemy enemy) {

        }
        public void MoveEnemies (EntityContainer<Enemy> enemies) {

        }

    }

    public class Down : IMovementStrategy {
        public void MoveEnemy (Enemy enemy) {
            enemy.Shape.MoveY(-0.001f);
        }
        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy=>MoveEnemy(enemy));
        }
    }

    public class ZigZagDown : IMovementStrategy {
        private float amplitude = 0.05f;
        private float period = 0.045f;
        private float movementSpeed = 0.0003f;
        
        public void MoveEnemy(Enemy enemy) {
            var currP = enemy.Shape.Position;
            var startP = enemy.StartingPosition;

            var newP = new Vec2F(0, 0);

            newP.Y = currP.Y - movementSpeed;
            var sineThing = (float) Math.Sin(2 * Math.PI * (startP.Y - newP.Y) / period);
            newP.X = startP.X + amplitude * sineThing;

            enemy.Shape.SetPosition(newP);
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}