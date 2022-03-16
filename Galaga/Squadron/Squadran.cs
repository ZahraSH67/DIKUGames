using Galaga.Squadron;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.Squadron {
    public class Squadron1 : ISquadron {
        public int MaxEnemies {get;} = 4;
        public EntityContainer<Enemy> Enemies {get; private set;}
        public Squadron1(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            CreateEnemies(enemyStride, alternativeEnemyStride);
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
    }

    public class Squadron2 : ISquadron {
        public int MaxEnemies {get;} = 3;
        public EntityContainer<Enemy> Enemies {get; private set;}
        public Squadron2(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            CreateEnemies(enemyStride, alternativeEnemyStride);
        }

        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            for (int i = 0; i < MaxEnemies ; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.5f + (float)i * 0.1f, 0.5f + (float)i  * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
        }
    }
    public class Squadron3 : ISquadron {
        public int MaxEnemies {get;} = 3;
        public EntityContainer<Enemy> Enemies {get; private set;}
        public Squadron3(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            CreateEnemies(enemyStride, alternativeEnemyStride);
        }

        public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            for (int i = 0; i < MaxEnemies ; i++) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.9f, 0.9f - (float)i  * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    new ImageStride(30, alternativeEnemyStride))); 
            }
        }
    }
}
