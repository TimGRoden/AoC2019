using System;
using System.IO;
using System.Collections.Generic;

class Program {
  public static int FuelReq (int mass) {
    return mass/3 - 2;
  }

  public static int Part1 (int[] data) {
    int totfuel = 0;
    for (int i=0;i<data.Length;i++) {
      totfuel += FuelReq(data[i]);
    }
    return totfuel;
  }

  public static int FuelReq2 (int mass) {
    if ((mass/3-2)<1) {
      return 0;
    } else {
      int t1 = mass/3 - 2;
      return t1 + FuelReq2(t1);
    }
  }

  public static int Part2 (int[] data) {
    int totfuel = 0;
    for (int i=0;i<data.Length;i++) {
      totfuel += FuelReq2(data[i]);
    }
    return totfuel;
  }

  public static void Main (string[] args) {
    string[] datain = File.ReadAllLines("input.txt"); //Open the data as an array of strings.
    int[] datasafe = new int[datain.Length];
    for (int i=0;i<datain.Length;i++) {
      datasafe[i]=int.Parse(datain[i]);
    }
    Console.WriteLine("Part 1 Solution: Fuel needed: {0}.",Part1(datasafe));
    Console.WriteLine("Part 2 Solution: Fuel needed: {0}.",Part2(datasafe));
  }
}
