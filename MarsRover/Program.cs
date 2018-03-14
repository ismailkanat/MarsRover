using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var marsRover = new MarsRover(5, 5);
            marsRover.AddRover(1, 2, 'N');
            marsRover.SendCommand("LMLMLMLMM");
            marsRover.AddRover(3, 3, 'E');
            marsRover.SendCommand("MMRMMRMRRM");
            marsRover.GetPositions();
            Console.Read();
        }
    }

    public class Rover
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int DirectionAngle { get; set; }
        public Rover(int x, int y, char directionName)
        {
            X = x;
            Y = y;
            DirectionAngle = ConvertDirectionLetterToAngle(directionName);
        }
        public int ConvertDirectionLetterToAngle(char directionName)
        {
            var angle = 0;
            switch (directionName)
            {
                case 'S':
                    angle = 0;
                    break;
                case 'E':
                    angle = 90;
                    break;
                case 'N':
                    angle = 180;
                    break;
                case 'W':
                    angle = 270;
                    break;
            }
            return angle;
        }
        public char ConvertDirectionAngleToLetter(int angle)
        {
            var letter = '\0';
            switch (angle % 360)
            {
                case 0:
                    letter = 'S';
                    break;
                case 90:
                    letter = 'E';
                    break;
                case 180:
                    letter = 'N';
                    break;
                case 270:
                    letter = 'W';
                    break;
            }
            return letter;
        }
        public void Dump()
        {
            Console.WriteLine($"{X} {Y} {ConvertDirectionAngleToLetter(DirectionAngle)}");
        }
    }

    public class MarsRover
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Rover> RoverList { get; }

        public MarsRover(int width, int height)
        {
            Width = width;
            Height = height;
            RoverList = new List<Rover>();
        }

        public void AddRover(int x, int y, char directionName)
        {
            RoverList.Add(new Rover(x, y, directionName));
        }

        public void SendCommand(string commands)
        {
            commands.ToList().ForEach(Move);
        }

        public void Move(char commandLetter)
        {
            var lastRover = RoverList[RoverList.Count - 1];
            var direction = lastRover.DirectionAngle;

            switch (commandLetter)
            {
                case 'L':
                    direction = (direction + 90) % 360;
                    RoverList[RoverList.Count - 1].DirectionAngle = direction;
                    return;
                case 'R':
                    direction = (direction + 270) % 360;
                    RoverList[RoverList.Count - 1].DirectionAngle = direction;
                    return;
            }

            int dx = 0, dy = 0, movement = 1;

            switch (direction)
            {
                case 0:
                    dy = -movement;
                    break;
                case 90:
                    dx = movement;
                    break;
                case 180:
                    dy = movement;
                    break;
                case 270:
                    dx = -movement;
                    break;
            }

            var targetX = lastRover.X + dx;
            var targetY = lastRover.Y + dy;

            RoverList[RoverList.Count - 1].X = targetX;
            RoverList[RoverList.Count - 1].Y = targetY;
        }

        public void GetPositions()
        {
            RoverList.ForEach(x => x.Dump());
        }

    }
}
