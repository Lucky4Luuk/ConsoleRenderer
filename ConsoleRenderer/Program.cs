using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Added dependencies
using System.Numerics;
using System.IO;
using System.Diagnostics;

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
            char[,] screen = new char[Console.WindowWidth/2,Console.WindowHeight-1]; //Create a 2D Array that holds the terminal data
            
            bool isRunning = true;
            bool showDebug = true;

            Stopwatch sw = new Stopwatch();
            float deltaTime = 0;
            int fps = 0;

            Console.CursorVisible = false;

            float tri_pos = 0;

            //Simple loop that will run as long as the console application is open (or the 'isRunning' variable is set to false)
            while (isRunning)
            {
                sw.Restart();
                ClearScreen(ref screen);
                //RenderToScreen(screen, Primitives.Box, new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));

                Triangle tri;
                tri.p1 = new Vector2(12 + tri_pos, 8);
                tri.p2 = new Vector2(64 + tri_pos, 16);
                tri.p3 = new Vector2(12 + tri_pos, 17);

                //Graphics.TriangleFill(ref screen, tri);

                Circle circ;
                circ.pos = new Vector2(12 + tri_pos, 16);
                circ.radius = 8f;

                //Graphics.CircleFill(ref screen, circ);
                Graphics.TriangleFill(ref screen, tri);

                tri_pos = (tri_pos + deltaTime) % 50;

                DrawBuffer(screen);

                sw.Stop();
                deltaTime = sw.ElapsedMilliseconds / 1000f; //Get delta time in seconds elapsed
                fps = (int)(1000f / (sw.ElapsedMilliseconds == 0 ? 1 : sw.ElapsedMilliseconds)); //Get frames per second based on delta time in milliseconds elapsed

                if (showDebug)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(fps);
                    Console.WriteLine(sw.ElapsedMilliseconds);
                }
                //isRunning = false;
            }

            Console.ReadLine();
        }

        static void ClearScreen(ref char[,] screen)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < screen.GetLength(1); y++)
            {
                for (int x = 0; x < screen.GetLength(0); x++)
                {
                    screen[x, y] = ' ';
                }
            }
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
                        stream.Write(screen[x, y]);
                    }
                }
            }
        }
    }
}
