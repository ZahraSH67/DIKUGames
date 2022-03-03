using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using Galaga;

namespace Galaga {
    public class PlayerShot : Entity {
        private Entity entity;
        private static Vec2F extent;
        private static Vec2F direction;
        
        private PlayerShot(DynamicShape shape, IBaseImage image,Vec2F vec) : base(shape,image){
            entity = new Entity(shape, image);
            extent = new Vec2F(0.008f, 0.021f);
            direction = new Vec2F(0.0f, 0.1f);
        }
    }
}