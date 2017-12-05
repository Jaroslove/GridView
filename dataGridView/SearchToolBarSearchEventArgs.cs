// Decompiled with JetBrains decompiler
// Type: ADGV.SearchToolBarSearchEventArgs
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\AdvancedGridView\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using System;
using System.Windows.Forms;

namespace ADGV
{
  public class SearchToolBarSearchEventArgs : EventArgs
  {
    public string ValueToSearch { get; private set; }

    public DataGridViewColumn ColumnToSearch { get; private set; }

    public bool CaseSensitive { get; private set; }

    public bool WholeWord { get; private set; }

    public bool FromBegin { get; private set; }

    public SearchToolBarSearchEventArgs(string Value, DataGridViewColumn Column, bool Case, bool Whole, bool fromBegin)
    {
      this.ValueToSearch = Value;
      this.ColumnToSearch = Column;
      this.CaseSensitive = Case;
      this.WholeWord = Whole;
      this.FromBegin = fromBegin;
    }
  }
}
