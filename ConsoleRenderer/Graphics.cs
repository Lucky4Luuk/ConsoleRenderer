using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRenderer
{

    class Graphics
    {
        //Taken from: https://alienryderflex.com/polygon_fill/
        //TODO: This is broken :(
        //Last for-loop seems to not do anything at all
        public static void PolygonFill(ref char[,] screen, List<Vector2> points)
        {
            int nodes, pixelX, pixelY, i, j, swap;
            int[] nodeX = new int[Constants.MAX_POLY_CORNERS];
            int polyCorners = points.Count();

            int[] polyX = new int[polyCorners];
            int[] polyY = new int[polyCorners];

            //Converts points to int's, stored in a way the algorithm likes
            for (int index = 0; index < polyCorners; index++)
            {
                polyX[index] = (int)Math.Floor(points[index].X);
                polyY[index] = (int)Math.Floor(points[index].Y);
            }

            for (pixelY=0; pixelY<Console.WindowHeight; pixelY++)
            {
                nodes = 0; j = polyCorners - 1;
                for (i = 0; i < polyCorners; i++)
                {
                    if (polyY[i] < (double)pixelY && polyY[j] >= (double)pixelY
                      ||polyY[j] < (double)pixelY && polyY[i] >= (double)pixelY)
                    {
                        nodeX[nodes++] = (int)(polyX[i] + (pixelY - polyY[i]) / (polyY[j] - polyY[i]) * (polyX[j] - polyX[i]));
                    }
                    j = i;
                }

                i = 0;
                while (i < nodes - 1)
                {
                    if (nodeX[i] > nodeX[i+1])
                    {
                        swap = nodeX[i];
                        nodeX[i] = nodeX[i + 1];
                        nodeX[i + 1] = swap;
                        if (i > 0) i--;
                    } else
                    {
                        i++;
                    }
                }

                for (i=0; i<nodes; i+=2)
                {
                    if (nodeX[i  ] >= Console.WindowWidth) break;
                    if (nodeX[i+1] >  0)
                    {
                        if (nodeX[i] < 0) nodeX[i] = 0;
                        if (nodeX[i + 1] > Console.WindowWidth) nodeX[i + 1] = Console.WindowWidth;
                        for (pixelX = nodeX[i]; pixelX < nodeX[i + 1]; pixelX++) screen[pixelX, pixelY] = 'b';
                    }
                }
            }
        }
    }
}
