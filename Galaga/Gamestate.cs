using DIKUArcade.Entities;

namespace Galaga {
    public enum gs {
        gameOver,
        gameActive
    }

    public class Gamestate {
        private float _gameOverHeight = 0.1f;
        private gs _state = gs.gameActive;
        public gs State { get { return _state; } }

        public Gamestate() {
        }

        public Gamestate(float gameOverHeight) {
            _gameOverHeight = gameOverHeight;
        }

        public void GameOver(EntityContainer<Enemy> enemies) {
            

            foreach (Enemy enemy in enemies) {
                if (enemy.Shape.Position.Y < _gameOverHeight) {
                    _state = gs.gameOver;
                }
            }
        }
    }
}
