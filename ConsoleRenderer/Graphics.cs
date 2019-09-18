using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRenderer
{

    struct Edge
    {
        public Vector2 p1; //Point 1 of edge
        public Vector2 p2; //Point 2 of edge
    }

    struct BBOX
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;
    }

    struct Triangle
    {
        public Vector2 p1; //Point 1 of triangle
        public Vector2 p2; //Point 2 of triangle
        public Vector2 p3; //Point 3 of triangle
    }

    struct Quad
    {
        public Vector2 p1; //Point 1 of quad
        public Vector2 p2; //Point 2 of quad
        public Vector2 p3; //Point 3 of quad
        public Vector2 p4; //Point 4 of quad
    }

    struct Circle
    {
        public Vector2 pos; //Center of circle
        public float radius; //Radius of circle
    }

    class Graphics
    {

        /*
        static int Clamp(int x, int a, int b)
        {
            if (x < a) return a;
            if (x > b) return b;
            return x;
        }
        */

        //Faster clamp found online (thanks stackoverflow)
        static int Clamp(int n, int min, int max) => (n >= min) ? (n <= max) ? n : max : min;

        //Bounding box of triangle
        static BBOX BBOX_Triangle(Triangle tri)
        {
            BBOX bb;
            bb.x1 = (int)Math.Min(tri.p1.X, Math.Min(tri.p2.X, tri.p3.X));
            bb.y1 = (int)Math.Min(tri.p1.Y, Math.Min(tri.p2.Y, tri.p3.Y));
            bb.x2 = (int)Math.Max(tri.p1.X, Math.Max(tri.p2.X, tri.p3.X));
            bb.y2 = (int)Math.Max(tri.p1.Y, Math.Max(tri.p2.Y, tri.p3.Y));
            return bb;
        }

        static bool PointInTriangle(Triangle tri, int x, int y)
        {
            float area = (float)(0.5f * (-tri.p2.Y * tri.p3.X + tri.p1.Y * (-tri.p2.X + tri.p3.X) + tri.p1.X * (tri.p2.Y - tri.p3.Y) + tri.p2.X * tri.p3.Y));

            float s = 1f / (2f * area) * (tri.p1.Y * tri.p3.X - tri.p1.X * tri.p3.Y + (tri.p3.Y - tri.p1.Y) * x + (tri.p1.X - tri.p3.X) * y);
            float t = 1f / (2f * area) * (tri.p1.X * tri.p2.Y - tri.p1.Y * tri.p2.X + (tri.p1.Y - tri.p2.Y) * x + (tri.p2.X - tri.p1.X) * y);

            return (s > 0 && t > 0 && 1 - s - t > 0);
        }

        public static void TriangleFill(ref char[,] screen, Triangle tri)
        {
            BBOX bb = BBOX_Triangle(tri);

            bb.x1 = Math.Max(bb.x1, 0);
            bb.y1 = Math.Max(bb.y1, 0);
            bb.x2 = Math.Min(bb.x2, Console.WindowWidth);
            bb.y2 = Math.Min(bb.y2, Console.WindowHeight);

            for (int ix = bb.x1; ix < bb.x2; ix++)
            {
                for (int iy = bb.y1; iy < bb.y2; iy++)
                {
                    if (PointInTriangle(tri, ix, iy))
                    {
                        screen[Clamp(ix, 0, Console.WindowWidth / 2 - 1), Clamp(iy, 0, Console.WindowHeight - 1)] = 'b';
                    }
                }
            }
        }

        public static void QuadFill(ref char[,] screen, Quad quad)
        {
            Triangle tri1;
            tri1.p1 = quad.p1;
            tri1.p2 = quad.p2;
            tri1.p3 = quad.p3;
            TriangleFill(ref screen, tri1);

            Triangle tri2;
            tri2.p1 = quad.p1;
            tri2.p2 = tri1.p3;
            tri2.p3 = quad.p4;
            TriangleFill(ref screen, tri2);
        }

        public static void CircleFill(ref char[,] screen, Circle circ)
        {
            //BBOX of a circle is simply center -/+ radius on all sides
            for (int ix = (int)(circ.pos.X - circ.radius); ix < (int)(circ.pos.X + circ.radius); ix++)
            {
                for (int iy = (int)(circ.pos.Y - circ.radius); iy < (int)(circ.pos.Y + circ.radius); iy++)
                {
                    float dist = (float) Math.Sqrt(Math.Pow((float)ix - circ.pos.X, 2) + Math.Pow((float)iy - circ.pos.Y, 2));
                    if (dist < circ.radius)
                    {
                        screen[Clamp(ix, 0, Console.WindowWidth/2-1), Clamp(iy, 0, Console.WindowHeight-1)] = 'b';
                    }
                }
            }
        }

        //TODO: make this lol
        public static void PolygonFill(ref char[,] screen, List<int> points)
        {
            
        }
    }
}
