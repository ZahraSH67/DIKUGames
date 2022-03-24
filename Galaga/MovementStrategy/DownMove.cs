using Galaga.MovementStrategy;
using DIKUArcade.Entities;
using Galaga;

namespace Galaga.MovementStrategy {
    public class DownMove : IMovementStrategy {
        public void MoveEnemy (Enemy enemy) {
            enemy.Shape.MoveY(-0.001f);
        }
        public void MoveEnemies (EntityContainer<Enemy> enemies) {
            enemies.Iterate(enemy=>MoveEnemy(enemy));
        }
    }
}