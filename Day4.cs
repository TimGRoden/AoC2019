using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program {
  public static void Main (string[] args) {
    string[] datain = File.ReadAllLines("input.txt")[0].Split('-');
    List<int> valids = validsearch(datain[0],datain[1]);
    Console.WriteLine("I found {0} valid passwords.",valids.Count);
    List<int> newvalids = restrict(valids);
    Console.WriteLine("I found {0} better passwords.",newvalids.Count);
  }

  public static List<int> validsearch(string start,string end){
    int sval = int.Parse(start);
    int eval = int.Parse(end);
    int dist = Math.Abs(sval-eval);
    List<int> valids = new List<int>();
    for (int i=0;i<=dist;i++) {
      int pv = sval + i;
      string pass = pv.ToString();
      bool isval = true;
      bool haspair = false;
      for (int j=0;j<pass.Length-1;j++) {
        if (pass[j]==pass[j+1]){
          haspair = true;
        }
        if (pass[j]>pass[j+1]){
          isval = false;
        }
      }
      if (isval && haspair) {
        valids.Add(pv);
      }
    }
    return valids;
  }

  public static List<int> restrict(List<int> trials) {
    List<int> finals = new List<int>();
    foreach (int trial in trials) {
      List<char> foundpair = new List<char>();
      string pass = trial.ToString();
      for (int i=0;i<pass.Length-1;i++) {
        if (pass[i]==pass[i+1]) {
          foundpair.Add(pass[i]);
        }
      }
      bool uni = false;
      foreach (char val in foundpair) {
        int counter = 0;
        foreach (char t1 in foundpair) {
          if (val==t1) { //How many times does that turn up?
            counter += 1;
          }
        }
        if (counter==1) { //If it only turned up once, it's unique!
          uni = true;
        }
      }
      if (uni) {
        finals.Add(trial);
      }
    }
    return finals;
  }
}
