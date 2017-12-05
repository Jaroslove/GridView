// Decompiled with JetBrains decompiler
// Type: ADGV.AdvancedDataGridView
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\ConsoleApp13\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADGV
{
  public class AdvancedDataGridView : DataGridView
  {
    private List<string> sortOrder = new List<string>();
    private List<string> filterOrder = new List<string>();
    private List<string> readyToShowFilters = new List<string>();
    private bool atoGenerateContextFilters = true;
    private string sortString;
    private string filterString;
    private bool dateWithTime;
    private bool timeFilter;
    private bool loadedFilter;

    public bool AutoGenerateContextFilters
    {
      get
      {
        return this.atoGenerateContextFilters;
      }
      set
      {
        this.atoGenerateContextFilters = value;
      }
    }

    public bool DateWithTime
    {
      get
      {
        return this.dateWithTime;
      }
      set
      {
        this.dateWithTime = value;
      }
    }

    public bool TimeFilter
    {
      get
      {
        return this.timeFilter;
      }
      set
      {
        this.timeFilter = value;
      }
    }

    public event EventHandler SortStringChanged;

    public event EventHandler FilterStringChanged;

    private IEnumerable<ADGVColumnHeaderCell> filterCells
    {
      get
      {
        return this.Columns.Cast<DataGridViewColumn>().Where<DataGridViewColumn>((Func<DataGridViewColumn, bool>) (c =>
        {
          if (c.HeaderCell != null)
            return c.HeaderCell is ADGVColumnHeaderCell;
          return false;
        })).Select<DataGridViewColumn, ADGVColumnHeaderCell>((Func<DataGridViewColumn, ADGVColumnHeaderCell>) (c => c.HeaderCell as ADGVColumnHeaderCell));
      }
    }

    public string SortString
    {
      get
      {
        if (this.sortString != null)
          return this.sortString;
        return "";
      }
      private set
      {
        if (!(value != this.sortString))
          return;
        this.sortString = value;
        if (this.SortedColumn != null)
          this.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
        if (this.SortStringChanged == null)
          return;
        this.SortStringChanged((object) this, new EventArgs());
      }
    }

    public string FilterString
    {
      get
      {
        if (this.filterString != null)
          return this.filterString;
        return "";
      }
      private set
      {
        if (!(value != this.filterString))
          return;
        this.filterString = value;
        if (this.FilterStringChanged == null)
          return;
        this.FilterStringChanged((object) this, new EventArgs());
      }
    }

    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
      e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
      ADGVColumnHeaderCell columnHeaderCell = new ADGVColumnHeaderCell(e.Column.HeaderCell, this.AutoGenerateContextFilters);
      columnHeaderCell.DateWithTime = this.DateWithTime;
      columnHeaderCell.TimeFilter = this.TimeFilter;
      columnHeaderCell.SortChanged += new ADGVFilterEventHandler(this.eSortChanged);
      columnHeaderCell.FilterChanged += new ADGVFilterEventHandler(this.eFilterChanged);
      columnHeaderCell.FilterPopup += new ADGVFilterEventHandler(this.eFilterPopup);
      e.Column.MinimumWidth = columnHeaderCell.MinimumSize.Width;
      if (this.ColumnHeadersHeight < columnHeaderCell.MinimumSize.Height)
        this.ColumnHeadersHeight = columnHeaderCell.MinimumSize.Height;
      e.Column.HeaderCell = (DataGridViewColumnHeaderCell) columnHeaderCell;
      base.OnColumnAdded(e);
    }

    protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
    {
      this.readyToShowFilters.Remove(e.Column.Name);
      this.filterOrder.Remove(e.Column.Name);
      this.sortOrder.Remove(e.Column.Name);
      ADGVColumnHeaderCell headerCell = e.Column.HeaderCell as ADGVColumnHeaderCell;
      if (headerCell != null)
      {
        headerCell.SortChanged -= new ADGVFilterEventHandler(this.eSortChanged);
        headerCell.FilterChanged -= new ADGVFilterEventHandler(this.eFilterChanged);
        headerCell.FilterPopup -= new ADGVFilterEventHandler(this.eFilterPopup);
      }
      base.OnColumnRemoved(e);
    }

    protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
    {
      this.readyToShowFilters.Clear();
      base.OnRowsAdded(e);
    }

    protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
    {
      this.readyToShowFilters.Clear();
      base.OnRowsRemoved(e);
    }

    protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
    {
      this.readyToShowFilters.Remove(this.Columns[e.ColumnIndex].Name);
      base.OnCellValueChanged(e);
    }

    private void eFilterPopup(object sender, ADGVFilterEventArgs e)
    {
      if (!this.Columns.Contains(e.Column))
        return;
      ADGVFilterMenu filterMenu = e.FilterMenu;
      DataGridViewColumn column = e.Column;
      Rectangle displayRectangle = this.GetCellDisplayRectangle(column.Index, -1, true);
      if (this.readyToShowFilters.Contains(column.Name))
      {
        filterMenu.Show((Control) this, displayRectangle.Left, displayRectangle.Bottom, false);
      }
      else
      {
        this.readyToShowFilters.Add(column.Name);
        if (this.filterOrder.Count<string>() > 0 && this.filterOrder.Last<string>() == column.Name)
          filterMenu.Show((Control) this, displayRectangle.Left, displayRectangle.Bottom, true);
        else
          filterMenu.Show((Control) this, displayRectangle.Left, displayRectangle.Bottom, ADGVFilterMenu.GetValuesForFilter((DataGridView) this, column.Name));
      }
    }

    private void eFilterChanged(object sender, ADGVFilterEventArgs e)
    {
      if (!this.Columns.Contains(e.Column))
        return;
      ADGVFilterMenu FilterMenu = e.FilterMenu;
      DataGridViewColumn column = e.Column;
      this.filterOrder.Remove(column.Name);
      if (FilterMenu.ActiveFilterType != ADGVFilterMenuFilterType.None)
        this.filterOrder.Add(column.Name);
      this.FilterString = this.CreateFilterString();
      if (!this.loadedFilter)
        return;
      this.loadedFilter = false;
      foreach (ADGVColumnHeaderCell columnHeaderCell in this.filterCells.Where<ADGVColumnHeaderCell>((Func<ADGVColumnHeaderCell, bool>) (f => f.FilterMenu != FilterMenu)))
        columnHeaderCell.SetLoadedFilterMode(false);
    }

    private void eSortChanged(object sender, ADGVFilterEventArgs e)
    {
      if (!this.Columns.Contains(e.Column))
        return;
      ADGVFilterMenu filterMenu = e.FilterMenu;
      DataGridViewColumn column = e.Column;
      this.sortOrder.Remove(column.Name);
      if (filterMenu.ActiveSortType != ADGVFilterMenuSortType.None)
        this.sortOrder.Add(column.Name);
      this.SortString = this.CreateSortString();
    }

    private string CreateFilterString()
    {
      StringBuilder stringBuilder = new StringBuilder("");
      string str = "";
      foreach (string index in this.filterOrder)
      {
        DataGridViewColumn column = this.Columns[index];
        if (column != null)
        {
          ADGVColumnHeaderCell headerCell = column.HeaderCell as ADGVColumnHeaderCell;
          if (headerCell != null && headerCell.FilterEnabled && headerCell.ActiveFilterType != ADGVFilterMenuFilterType.None)
          {
            stringBuilder.AppendFormat(str + "(" + headerCell.FilterString + ")", (object) column.DataPropertyName);
            str = " AND ";
          }
        }
      }
      return stringBuilder.ToString();
    }

    private string CreateSortString()
    {
      StringBuilder stringBuilder = new StringBuilder("");
      string str = "";
      foreach (string index in this.sortOrder)
      {
        DataGridViewColumn column = this.Columns[index];
        if (column != null)
        {
          ADGVColumnHeaderCell headerCell = column.HeaderCell as ADGVColumnHeaderCell;
          if (headerCell != null && headerCell.FilterEnabled && headerCell.ActiveSortType != ADGVFilterMenuSortType.None)
          {
            stringBuilder.AppendFormat(str + headerCell.SortString, (object) column.DataPropertyName);
            str = ", ";
          }
        }
      }
      return stringBuilder.ToString();
    }

    public void EnableFilter(DataGridViewColumn Column)
    {
      if (!this.Columns.Contains(Column))
        return;
      ADGVColumnHeaderCell headerCell = Column.HeaderCell as ADGVColumnHeaderCell;
      if (headerCell != null)
        this.EnableFilter(Column, headerCell.DateWithTime, headerCell.TimeFilter);
      else
        this.EnableFilter(Column, this.DateWithTime, this.TimeFilter);
    }

    public void EnableFilter(DataGridViewColumn Column, bool DateWithTime, bool TimeFilter)
    {
      if (!this.Columns.Contains(Column))
        return;
      ADGVColumnHeaderCell headerCell = Column.HeaderCell as ADGVColumnHeaderCell;
      if (headerCell != null)
      {
        if (headerCell.DateWithTime != DateWithTime || headerCell.TimeFilter != TimeFilter || !headerCell.FilterEnabled && (headerCell.FilterString.Length > 0 || headerCell.SortString.Length > 0))
          this.ClearFilter(true);
        headerCell.DateWithTime = DateWithTime;
        headerCell.TimeFilter = TimeFilter;
        headerCell.FilterEnabled = true;
        this.readyToShowFilters.Remove(Column.Name);
      }
      else
      {
        Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        ADGVColumnHeaderCell columnHeaderCell = new ADGVColumnHeaderCell(Column.HeaderCell, true);
        columnHeaderCell.DateWithTime = this.DateWithTime;
        columnHeaderCell.TimeFilter = this.TimeFilter;
        columnHeaderCell.SortChanged += new ADGVFilterEventHandler(this.eSortChanged);
        columnHeaderCell.FilterChanged += new ADGVFilterEventHandler(this.eFilterChanged);
        columnHeaderCell.FilterPopup += new ADGVFilterEventHandler(this.eFilterPopup);
        Column.MinimumWidth = columnHeaderCell.MinimumSize.Width;
        if (this.ColumnHeadersHeight < columnHeaderCell.MinimumSize.Height)
          this.ColumnHeadersHeight = columnHeaderCell.MinimumSize.Height;
        Column.HeaderCell = (DataGridViewColumnHeaderCell) columnHeaderCell;
      }
      Column.SortMode = DataGridViewColumnSortMode.Programmatic;
    }

    protected override void OnSorted(EventArgs e)
    {
      this.ClearSort(false);
      base.OnSorted(e);
    }

    public void DisableFilter(DataGridViewColumn Column)
    {
      if (!this.Columns.Contains(Column))
        return;
      ADGVColumnHeaderCell headerCell = Column.HeaderCell as ADGVColumnHeaderCell;
      if (headerCell != null)
      {
        if (headerCell.FilterEnabled && (headerCell.SortString.Length > 0 || headerCell.FilterString.Length > 0))
        {
          this.ClearFilter(true);
          headerCell.FilterEnabled = false;
        }
        else
          headerCell.FilterEnabled = false;
        this.filterOrder.Remove(Column.Name);
        this.sortOrder.Remove(Column.Name);
        this.readyToShowFilters.Remove(Column.Name);
      }
      Column.SortMode = DataGridViewColumnSortMode.Automatic;
    }

    public void LoadFilter(string Filter, string Sorting = null)
    {
      foreach (ADGVColumnHeaderCell filterCell in this.filterCells)
        filterCell.SetLoadedFilterMode(true);
      this.filterOrder.Clear();
      this.sortOrder.Clear();
      this.readyToShowFilters.Clear();
      if (Filter != null)
        this.FilterString = Filter;
      if (Sorting != null)
        this.SortString = Sorting;
      this.loadedFilter = true;
    }

    public void ClearSort(bool FireEvent = false)
    {
      foreach (ADGVColumnHeaderCell filterCell in this.filterCells)
        filterCell.ClearSorting();
      this.sortOrder.Clear();
      if (FireEvent)
        this.SortString = (string) null;
      else
        this.sortString = (string) null;
    }

    public void ClearFilter(bool FireEvent = false)
    {
      foreach (ADGVColumnHeaderCell filterCell in this.filterCells)
        filterCell.ClearFilter();
      this.filterOrder.Clear();
      if (FireEvent)
        this.FilterString = (string) null;
      else
        this.filterString = (string) null;
    }

    public DataGridViewCell FindCell(string ValueToFind, string ColumnName = null, int RowIndex = 0, int ColumnIndex = 0, bool isWholeWordSearch = true, bool isCaseSensitive = false)
    {
      if (ValueToFind != null && this.RowCount > 0 && this.ColumnCount > 0 && (ColumnName == null || this.Columns.Contains(ColumnName) && this.Columns[ColumnName].Visible))
      {
        RowIndex = Math.Max(0, RowIndex);
        if (!isCaseSensitive)
          ValueToFind = ValueToFind.ToLower();
        if (ColumnName != null)
        {
          int index1 = this.Columns[ColumnName].Index;
          if (ColumnIndex > index1)
            ++RowIndex;
          for (int index2 = RowIndex; index2 < this.RowCount; ++index2)
          {
            string lower = this.Rows[index2].Cells[index1].FormattedValue.ToString();
            if (!isCaseSensitive)
              lower = lower.ToLower();
            if (!isWholeWordSearch && lower.Contains(ValueToFind) || lower.Equals(ValueToFind))
              return this.Rows[index2].Cells[index1];
          }
        }
        else
        {
          ColumnIndex = Math.Max(0, ColumnIndex);
          for (int index1 = RowIndex; index1 < this.RowCount; ++index1)
          {
            for (int index2 = ColumnIndex; index2 < this.ColumnCount; ++index2)
            {
              string lower = this.Rows[index1].Cells[index2].FormattedValue.ToString();
              if (!isCaseSensitive)
                lower = lower.ToLower();
              if (!isWholeWordSearch && lower.Contains(ValueToFind) || lower.Equals(ValueToFind))
                return this.Rows[index1].Cells[index2];
            }
            ColumnIndex = 0;
          }
        }
      }
      return (DataGridViewCell) null;
    }
  }
}
