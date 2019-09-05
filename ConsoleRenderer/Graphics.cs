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

    class Graphics
    {

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
                        screen[ix, iy] = 'b';
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
