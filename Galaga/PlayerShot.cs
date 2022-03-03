using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;
namespace Galaga {
    public class PlayerShot : Entity {
        private Entity entity;
        private static float extent;
        private static float direction;
        
        private PlayerShot(DynamicShape shape, IBaseImage image,float Vec2F) : base(shape,image){
            entity = new Entity(shape, image);
        }


    }
}