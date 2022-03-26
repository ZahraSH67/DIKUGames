using Galaga.MovementStrategy;
using DIKUArcade.Entities;
using System.Collections.Generic;
using System;
using DIKUArcade.Math;
using Galaga;

namespace Galaga.MovementStrategy {
      public class Down : IMovementStrategy {
        public void MoveEnemy (Enemy enemy) {
            enemy.Shape.MoveY(-enemy.MOVEMENT_SPEED);
        }
        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy=>MoveEnemy(enemy));
        }
    }
}    