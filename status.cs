using System;

class status
{
    public int curModes;
    public int curCaptures;
    public bool isDragging;
    public Point curPoint;
    public SLine curLine;
    protected int visibility = 4;

    public ArrayList points = new ArrayList();

    protected double d(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow(b.Y - a.Y, 2));
    }
    protected Point segmentCenter(Point a, Point b)
    {
        Point c = a;
        c.X = (a.X + b.X) / 2;
        c.Y = (a.Y + b.Y) / 2;
        return c;
    }
    public void testD(Point e)
    {
        Point a = e;
        Point b = e;
        Point c = e;

        c.X = 5;
        c.Y = 0;

        a.X = 0;
        a.Y = 0;

        b.X = 10;
        b.Y = 0;
        Console.WriteLine(d(a, c).ToString() + " " + d(b, c).ToString() + " " + d(a, b).ToString());

    }
    public Point getLine(Point midPoint)
    {
        int dx = (int)midPoint.X;
        int dy = (int)midPoint.Y;
        foreach (SLine line in points)
        {
            // Console.WriteLine(d(line.a, midPoint) + d(midPoint, line.b).ToString() + " " + d(line.a, line.b).ToString());

            if (d(line.a, midPoint) + d(midPoint, line.b) - d(line.a, line.b) < 1)
            {

                if (d(line.a, midPoint) < visibility)
                    curCaptures = (int)captures.TAKE_PT1;
                else if (d(line.b, midPoint) < visibility)
                    curCaptures = (int)captures.TAKE_PT2;
                else curCaptures = (int)captures.TAKE_CENTR;
                return midPoint;
            }



        }
        curCaptures = (int)captures.TAKE_NONE;
        return midPoint;
    }

    public void pointsDebug()
    {
        Console.WriteLine("----------------");
        foreach (SLine line in points)
        {
            Console.WriteLine(line.a.ToString() + " " + line.b.ToString());
        }
    }


}