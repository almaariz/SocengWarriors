using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
  [SerializeField] List<string> lines;

  public List<string> Lines
  {
    get { return lines; }
  }

  public void setLines(List<string> newLines)
  {
    lines = newLines;
  }
}