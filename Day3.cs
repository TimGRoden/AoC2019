using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


class Program {
  public static void Main (string[] args) {
    string[] datain = File.ReadAllLines("input.txt");
    string[] wire1 = datain[0].Split(',');
    string[] wire2 = datain[1].Split(',');
    List<int[]> w1points = pointlist(wire1);
    List<int[]> w2points = pointlist(wire2);
    List<int[]> crosses = findcrosses(w1points,w2points);
    Console.WriteLine("I found {0} crosses.",crosses.Count());
    int[] closest = shortest(crosses);
    int mindist = Math.Abs(closest[0])+Math.Abs(closest[1]);
    Console.WriteLine("Closest ({0},{1}), Distance {3}.",closest[0],closest[1],closest[2],mindist);
    int[] fewstep = fewsteps(crosses);
    Console.WriteLine("Shortest Steps {0} for ({1},{2})",fewstep[2],fewstep[0],fewstep[1]);
  }

  public static int[] fewsteps(List<int[]> crosses) {
    int minsteps = 10000;
    int[] minsteppy = new int[3];
    foreach (int[] coord in crosses) {
      if (coord[2]<minsteps) {
        minsteps = coord[2];
        minsteppy = coord;
      }
    }
    return minsteppy;
  }

  public static int[] move(string instr, int[] coord) {
    char dir = instr[0];
    int dist = int.Parse(instr.Substring(1));
    int[] t1 = {0,0,coord[2]};
    t1[0] += coord[0];
    t1[1] += coord[1];
    t1[2] += dist;
    if (dir=='U') {
      t1[1] = coord[1] + dist;
    } else if (dir=='D') {
      t1[1] = coord[1] - dist;
    } else if (dir=='L') {
      t1[0] = coord[0] - dist;
    } else { //It's Right.
      t1[0] = coord[0] + dist;
    }
    return t1;
  }

  public static int[] shortest(List<int[]> crosses) {
    int mindist = 10000;
    int[] closest = new int[3];
    foreach (int[] coord in crosses) {
      int xdist = Math.Abs(coord[0]);
      int ydist = Math.Abs(coord[1]);
      if ((xdist+ydist)<mindist) {
        mindist = xdist+ydist;
        closest = coord;
      }
    }
    return closest;
  }

  public static List<int[]> pointlist(string[] instructions) {
    List<int[]> points = new List<int[]>();
    int[] origin = {0,0,1};
    if (instructions[0][0]=='R') {
      origin[0] = 1;
    } else if (instructions[0][0]=='L') {
      origin[0] = -1;
    } else if (instructions[0][0]=='U') {
      origin[1] = 1;
    } else {
      origin[1] = -1;
    }
    points.Add(origin);
    int[] bigo = {0,0,0};
    points.Add(bigo);
    foreach (string inst in instructions) {
      int[] next = move(inst,points.Last());
      points.Add(next);
    }
    points.Remove(bigo);
    foreach (int[] point in points) {
    }
    return points;
  }

  public static List<int[]> findcrosses(List<int[]> wire1, List<int[]> wire2) {
    List<int[]> crosses = new List<int[]>();
    for (int i=0;i<wire1.Count()-1;i++) { //Doing this in pairs, got to stop one early
      int[] coord1 = wire1[i];
      int[] coord2 = wire1[i+1];
      if (coord1[1]==coord2[1]) { //Wire 1 is horizontal here.
        for (int j=0;j<wire2.Count()-1;j++) { //Does it intersect wire1?
          int[] coord3 = wire2[j];
          int[] coord4 = wire2[j+1];
          if (coord3[0]==coord4[0]) { //Vertical Intersections are easiest.
            if ((coord3[1]-coord1[1])*(coord4[1]-coord1[1])<=0) {
              if ((coord1[0]-coord3[0])*(coord2[0]-coord3[0])<=0) {
                int steps = coord1[2] + coord3[2]+Math.Abs(coord3[1]-coord1[1])+Math.Abs(coord3[0]-coord1[0]);
                int[] cross = {coord3[0],coord1[1],steps};
                crosses.Add(cross);
              }
            }
          } else if (coord1[1]==coord3[1]) { //Horizontal Intersection?
            int xmin = Math.Max(Math.Min(coord1[0],coord2[0]),Math.Min(coord3[0],coord4[0]));
            int xmax = Math.Min(Math.Max(coord1[0],coord2[0]),Math.Max(coord3[0],coord4[0]));
            if (xmax-xmin>=0) {  
              int xdist = xmax - xmin;
              for (int k=0;k<=xdist;k++) {
                int steps = coord1[2] + coord3[2];
                int[] cross = {xmin+k,coord1[1],steps};
                crosses.Add(cross);
              }
            }
          }
        }
      } else { //Wire 1 is vertical
        for (int j=0;j<wire2.Count()-1;j++) {
          int[] coord3 = wire2[j];
          int[] coord4 = wire2[j+1];
          if (coord3[1]==coord4[1]) {//Horizontal wire 2
            if ((coord3[0]-coord1[0])*(coord4[0]-coord1[0])<=0){ 
              if ((coord3[1]-coord1[0])*(coord3[1]-coord2[1])<=0) {
                int steps = coord1[2] + coord3[2];
                int[] cross = {coord1[0],coord3[1],steps};
                crosses.Add(cross);
              }
            }
          } else if (coord1[0]==coord3[0]) { //Vertical Intersection?
            int ymin = Math.Max(Math.Min(coord1[1],coord2[1]),Math.Min(coord3[1],coord4[1]));
            int ymax = Math.Min(Math.Max(coord1[1],coord2[1]),Math.Max(coord3[1],coord4[1]));
            if (ymax-ymin>=0) {
              int ydist = ymax - ymin;
              for (int k=0;k<=ydist;k++) {
                int steps = coord1[2] + coord3[2]+Math.Abs(coord3[1]-coord1[1])+Math.Abs(coord3[0]-coord1[0]);
                int[] cross = {coord1[0],ymin+k,steps};
                crosses.Add(cross);
              }
            }
            }
          }
      }
    }
    return crosses;
  }
}
