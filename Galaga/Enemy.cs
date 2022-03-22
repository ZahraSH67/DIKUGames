using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Math;

namespace Galaga {


    public class Enemy : Entity {
        private int hitpoints = 5;
        //private Graphics IBaseImage;
        public IBaseImage Enrage { get; set; }
        public Vec2F StartingPosition {get;set;}
        
        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enrage) : base(shape, image) {
            Enrage = enrage;
            StartingPosition = shape.Position;
        }
        
        public void hitPointsDecrease() {
            hitpoints--;
            if (hitpoints == 2) {
                Image = Enrage;
            }
        }
        
        public bool isAlive() {
            if (hitpoints > 0) {
                return true;
            } else {
                return false;
            }
        }
    }
}