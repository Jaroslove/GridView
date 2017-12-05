// Decompiled with JetBrains decompiler
// Type: ADGV.ADGVFilterEventArgs
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\AdvancedGridView\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using System;
using System.Windows.Forms;

namespace ADGV
{
  public class ADGVFilterEventArgs : EventArgs
  {
    public ADGVFilterMenu FilterMenu { get; private set; }

    public DataGridViewColumn Column { get; private set; }

    public ADGVFilterEventArgs(ADGVFilterMenu filterMenu, DataGridViewColumn column)
    {
      this.FilterMenu = filterMenu;
      this.Column = column;
    }
  }
}
