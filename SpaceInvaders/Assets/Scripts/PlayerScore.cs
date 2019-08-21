using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour, IComparer<int> {

    public string Username { get; set; }
    public string Level { get; set; }
    public int Score { get; set; }

    public PlayerScore(string username, string level, string score) {
        this.Username = username;
        this.Level = level;
        this.Score = int.Parse(score);
    }

    public override string ToString() {
        return Username + ";" + Level + ";" + Score + "#";
    }

    public int Compare(int x, int y) {

        if(x == 0 || y == 0)
        {
            return 0;
        }
        return x.CompareTo(y);
    }
}
