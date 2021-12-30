using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program {
  public static void Main (string[] args) {
    string[] datain = File.ReadAllLines("input.txt");
    string[] t1 = datain[0].Split(',');
    int[] datasafe = new int[t1.Length];
    for (int i=0;i<t1.Length;i++) {
      datasafe[i] = int.Parse(t1[i]);
    }
    Console.WriteLine("End of Program. Part 1 Result = "+runcode(datasafe)+".");
    for (int i=0;i<t1.Length;i++) { //Remake datasafe before P2.
      datasafe[i] = int.Parse(t1[i]);
    }
    Console.WriteLine("Noun/Verb Score = "+searcher(datasafe,19690720)+".");
  }

  public static int runcode (int[] data) {
    int pos = 0;
    while (data[pos]!=99) { //Terminates when the new instruction is 99.
      if (data[pos]==1) {
        data[data[pos+3]] = data[data[pos+1]]+data[data[pos+2]];
      } else {
        data[data[pos+3]] = data[data[pos+1]]*data[data[pos+2]];
      }
      pos += 4;
    }
    return data[0];
  }

  public static int searcher (int[] data,int target) {
    for (int noun=0;noun<100;noun++) {
      for (int verb=0;verb<100;verb++) {
        int[] test = data.ToArray();
        test[1] = noun;
        test[2] = verb;
        int result = runcode(test);
        if (result==target) {
          return 100*noun + verb;
        }
      }
    }
    Console.WriteLine("No Noun/Verb pair found.");
    return 0;
  }
}
