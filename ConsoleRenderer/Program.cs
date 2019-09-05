using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Added dependencies
using System.Numerics;
using System.IO;

namespace ConsoleRenderer
{
    class Program
    {
        //Links:
        //https://stackoverflow.com/questions/2754518/how-can-i-write-fast-colored-output-to-console

        //Enum that holds a few primitive shapes
        enum Primitives { Box, Sphere };

        struct Camera
        {
            public Vector3 position; //Position of the camera
            public Vector3 forward; //Forward direction of the camera, aka in what direction it's looking
            //I would add a Vector3 up variable here, to indicate what is up for the camera, but for simplicity's sake, I will assume up is `0,1,0`
        }
        
        static void Main(string[] args)
        {
            char[,] screen = new char[Console.WindowWidth,Console.WindowHeight-1]; //Create a 2D Array that holds the terminal data

            bool isRunning = true;

            Console.CursorVisible = false;

            //Simple loop that will run as long as the console application is open (or the 'isRunning' variable is set to false)
            while (isRunning)
            {
                ClearScreen(ref screen);
                RenderToScreen(screen, Primitives.Box, new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
                //Console.WriteLine(Console.WindowWidth);
                DrawBuffer(screen);
                //isRunning = false;
            }

            Console.ReadLine();
        }

        static void ClearScreen(ref char[,] screen)
        {
            Console.SetCursorPosition(0, 0);
        }

        //Function that can render primitive shapes to the screen
        static void RenderToScreen(char[,] screen, Primitives shape, Vector3 position, Vector3 size)
        {
            //screen[0, 0] = '0';
            Random rnd = new Random();
            
            for (int y = 0; y < screen.GetLength(1); y++)
            {
                for (int x = 0; x < screen.GetLength(0); x++)
                {
                    //screen[x, y] = (char)rnd.Next('a', 'z'); //Fill the screen with random characters. For testing purposes only
                    screen[x, y] = '-';
                }
            }

            /*
            List<int> points = new List<int>();
            points.Add(12);
            points.Add(8);

            points.Add(64);
            points.Add(16);

            points.Add(12);
            points.Add(17);

            Graphics.PolygonFill(ref screen, points);
            */

            Triangle tri;
            tri.p1 = new Vector2(12, 8);
            tri.p2 = new Vector2(64, 16);
            tri.p3 = new Vector2(12, 17);

            Graphics.TriangleFill(ref screen, tri);
        }

        //Function that renders the screen buffer to the terminal
        static void DrawBuffer(char[,] screen)
        {
            using (StreamWriter stream = new StreamWriter(Console.OpenStandardOutput())) //A streamwriter, for faster output
            {
                for (int y = 0; y < screen.GetLength(1); y++)
                {
                    for (int x = 0; x < screen.GetLength(0); x++)
                    {
                        stream.Write(screen[x, y]);
                    }
                }
            }
        }
    }
}
