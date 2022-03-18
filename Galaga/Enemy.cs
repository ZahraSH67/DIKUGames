using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Math;

namespace Galaga {


    public class Enemy : Entity {
        private int hitpoints = 7;
        //private Graphics IBaseImage;
        public IBaseImage Enrage { get; set; }
        public Vec2F StartingPosition {get;set;}
        
        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enrage) : base(shape, image) {
            Enrage = enrage;
            StartingPosition = shape.Position;
        }
        
        // Counts how much times the enemy can be hit before
        // it gets enraged
        public void hitPointsDecrease() {
            hitpoints--;
            if (hitpoints == 3) {
                Image = Enrage;
            }
        }
        
        // checks if the enemy still has points left
        // or if it should be terminated.
        public bool isAlive() {
            if (hitpoints > 0) {
                return true;
            } else {
                return false;
            }
        }
    }
}