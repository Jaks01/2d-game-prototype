using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace learn_opentk
{
    class ObjectFactory
    {
        public static Vertex[] CreateSolidCube(float side, Color4 color)
        {
            side = side / 2f; // half side - and other half
            Vertex[] vertices =
            {
               new Vertex(new Vector4(-side, -side, -side, 1.0f),   color),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
               new Vertex(new Vector4(-side, side, side, 1.0f),     color),

               new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
               new Vertex(new Vector4(side, side, -side, 1.0f),     color),
               new Vertex(new Vector4(side, -side, side, 1.0f),     color),
               new Vertex(new Vector4(side, -side, side, 1.0f),     color),
               new Vertex(new Vector4(side, side, -side, 1.0f),     color),
               new Vertex(new Vector4(side, side, side, 1.0f),      color),

               new Vertex(new Vector4(-side, -side, -side, 1.0f),   color),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
               new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
               new Vertex(new Vector4(side, -side, side, 1.0f),     color),

               new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
               new Vertex(new Vector4(-side, side, side, 1.0f),     color),
               new Vertex(new Vector4(side, side, -side, 1.0f),     color),
               new Vertex(new Vector4(side, side, -side, 1.0f),     color),
               new Vertex(new Vector4(-side, side, side, 1.0f),     color),
               new Vertex(new Vector4(side, side, side, 1.0f),      color),

               new Vertex(new Vector4(-side, -side, -side, 1.0f),   color),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
               new Vertex(new Vector4(side, -side, -side, 1.0f),    color),
               new Vertex(new Vector4(-side, side, -side, 1.0f),    color),
               new Vertex(new Vector4(side, side, -side, 1.0f),     color),

               new Vertex(new Vector4(-side, -side, side, 1.0f),    color),
               new Vertex(new Vector4(side, -side, side, 1.0f),     color),
               new Vertex(new Vector4(-side, side, side, 1.0f),     color),
               new Vertex(new Vector4(-side, side, side, 1.0f),     color),
               new Vertex(new Vector4(side, -side, side, 1.0f),     color),
               new Vertex(new Vector4(side, side, side, 1.0f),      color),
            };
            return vertices;
        }

        public static TexturedVertex[] CreateTexturedCube(float side, float textureWidth, float textureHeight)
        {
            float h = textureHeight;
            float w = textureWidth;
            side = side / 2f; // half side - and other half

            TexturedVertex[] vertices =
            {
        new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(w, h)),

        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

        new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),

        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, 0)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

        new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(0, 0)),

        new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, 0)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
        new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
        new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),
    };
            return vertices;
        }

        public static Vertex[] CreateQuad(float size, float z, Color4 color)
        {
            size /= 2;
            Vertex[] vertices =
            {
                new Vertex(new Vector4(-size, size, z, 1.0f), color),
                new Vertex(new Vector4(size, size, z, 1.0f),  color),
                new Vertex(new Vector4(size, -size, z, 1.0f), color),
                new Vertex(new Vector4(-size, -size, z, 1.0f),color),
            };
            return vertices;
        }

        public static Vertex[] triangle =
        {
             new Vertex(new Vector4(-0.25f, 0.25f, 0.5f, 1-0f), Color4.Tomato),
             new Vertex(new Vector4( 0.0f, -0.25f, 0.5f, 1-0f), Color4.Indigo),
             new Vertex(new Vector4( 0.25f, 0.25f, 0.5f, 1-0f), Color4.GreenYellow),
        };
        public static Vertex[] square =
        {
             new Vertex(new Vector4(-0.55f, 0.55f, 0.5f, 1-0f), Color4.Tomato),
             new Vertex(new Vector4( 0.55f, 0.55f, 0.5f, 1-0f), Color4.Indigo),
             new Vertex(new Vector4( -0.55f, -0.55f, 0.5f, 1-0f), Color4.GreenYellow),
             new Vertex(new Vector4( -0.55f, -0.55f, 0.5f, 1-0f), Color4.GreenYellow),
             new Vertex(new Vector4( 0.55f, 0.55f, 0.5f, 1-0f), Color4.Indigo),
             new Vertex(new Vector4( 0.55f, -0.55f, 0.5f, 1-0f), Color4.Tomato),
        };
    }
}
