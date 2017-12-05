// Decompiled with JetBrains decompiler
// Type: ADGV.ADGVColumnHeaderCell
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\ConsoleApp13\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using ADGV.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ADGV
{
  public class ADGVColumnHeaderCell : DataGridViewColumnHeaderCell
  {
    private Image filterImage = (Image) Resources.AddFilter;
    private Size filterButtonImageSize = new Size(16, 16);
    private Rectangle filterButtonOffsetBounds = Rectangle.Empty;
    private Rectangle filterButtonImageBounds = Rectangle.Empty;
    private Padding filterButtonMargin = new Padding(3, 4, 3, 4);
    private bool filterButtonPressed;
    private bool filterButtonOver;
    private bool filterEnabled;

    public ADGVFilterMenu FilterMenu { get; private set; }

    public event ADGVFilterEventHandler FilterPopup;

    public event ADGVFilterEventHandler SortChanged;

    public event ADGVFilterEventHandler FilterChanged;

    public Size MinimumSize
    {
      get
      {
        return new Size(this.filterButtonImageSize.Width + this.filterButtonMargin.Left + this.filterButtonMargin.Right, this.filterButtonImageSize.Height + this.filterButtonMargin.Bottom + this.filterButtonMargin.Top);
      }
    }

    public ADGVFilterMenuSortType ActiveSortType
    {
      get
      {
        if (this.FilterMenu != null && this.FilterEnabled)
          return this.FilterMenu.ActiveSortType;
        return ADGVFilterMenuSortType.None;
      }
    }

    public ADGVFilterMenuFilterType ActiveFilterType
    {
      get
      {
        if (this.FilterMenu != null && this.FilterEnabled)
          return this.FilterMenu.ActiveFilterType;
        return ADGVFilterMenuFilterType.None;
      }
    }

    public string SortString
    {
      get
      {
        if (this.FilterMenu != null && this.FilterEnabled)
          return this.FilterMenu.SortString;
        return "";
      }
    }

    public string FilterString
    {
      get
      {
        if (this.FilterMenu != null && this.FilterEnabled)
          return this.FilterMenu.FilterString;
        return "";
      }
    }

    public bool FilterEnabled
    {
      get
      {
        return this.filterEnabled;
      }
      set
      {
        if (!value)
        {
          this.filterButtonPressed = false;
          this.filterButtonOver = false;
        }
        if (value == this.filterEnabled)
          return;
        this.filterEnabled = value;
        bool flag = false;
        if (this.FilterMenu.FilterString.Length > 0)
        {
          this.FilterMenu_FilterChanged((object) this, new EventArgs());
          flag = true;
        }
        if (this.FilterMenu.SortString.Length > 0)
        {
          this.FilterMenu_SortChanged((object) this, new EventArgs());
          flag = true;
        }
        if (flag)
          return;
        this.RepaintCell();
      }
    }

    public bool DateWithTime
    {
      get
      {
        return this.FilterMenu.DateWithTime;
      }
      set
      {
        this.FilterMenu.DateWithTime = value;
      }
    }

    public bool TimeFilter
    {
      get
      {
        return this.FilterMenu.TimeFilter;
      }
      set
      {
        this.FilterMenu.TimeFilter = value;
      }
    }

    ~ADGVColumnHeaderCell()
    {
      if (this.FilterMenu == null)
        return;
      this.FilterMenu.FilterChanged -= new EventHandler(this.FilterMenu_FilterChanged);
      this.FilterMenu.SortChanged -= new EventHandler(this.FilterMenu_SortChanged);
    }

    public ADGVColumnHeaderCell(DataGridViewColumnHeaderCell oldCell, bool FilterEnabled = false)
    {
      this.Tag = oldCell.Tag;
      this.ErrorText = oldCell.ErrorText;
      this.ToolTipText = oldCell.ToolTipText;
      this.Value = oldCell.Value;
      this.ValueType = oldCell.ValueType;
      this.ContextMenuStrip = oldCell.ContextMenuStrip;
      this.Style = oldCell.Style;
      this.filterEnabled = FilterEnabled;
      ADGVColumnHeaderCell columnHeaderCell = oldCell as ADGVColumnHeaderCell;
      if (columnHeaderCell != null && columnHeaderCell.FilterMenu != null)
      {
        this.FilterMenu = columnHeaderCell.FilterMenu;
        this.filterImage = columnHeaderCell.filterImage;
        this.filterButtonPressed = columnHeaderCell.filterButtonPressed;
        this.filterButtonOver = columnHeaderCell.filterButtonOver;
        this.filterButtonOffsetBounds = columnHeaderCell.filterButtonOffsetBounds;
        this.filterButtonImageBounds = columnHeaderCell.filterButtonImageBounds;
        this.FilterMenu.FilterChanged += new EventHandler(this.FilterMenu_FilterChanged);
        this.FilterMenu.SortChanged += new EventHandler(this.FilterMenu_SortChanged);
      }
      else
      {
        this.FilterMenu = new ADGVFilterMenu(oldCell.OwningColumn.ValueType);
        this.FilterMenu.FilterChanged += new EventHandler(this.FilterMenu_FilterChanged);
        this.FilterMenu.SortChanged += new EventHandler(this.FilterMenu_SortChanged);
      }
    }

    public override object Clone()
    {
      return (object) new ADGVColumnHeaderCell((DataGridViewColumnHeaderCell) this, this.FilterEnabled);
    }

    private void RepaintCell()
    {
      if (!this.Displayed || this.DataGridView == null)
        return;
      this.DataGridView.InvalidateCell((DataGridViewCell) this);
    }

    internal void ClearSorting()
    {
      if (this.FilterMenu == null || !this.FilterEnabled)
        return;
      this.FilterMenu.ClearSorting();
      this.RefreshImage();
      this.RepaintCell();
    }

    internal void ClearFilter()
    {
      if (this.FilterMenu == null || !this.FilterEnabled)
        return;
      this.FilterMenu.ClearFilter();
      this.RefreshImage();
      this.RepaintCell();
    }

    private void RefreshImage()
    {
      if (this.ActiveFilterType == ADGVFilterMenuFilterType.Loaded)
        this.filterImage = (Image) Resources.SavedFilter;
      else if (this.ActiveFilterType == ADGVFilterMenuFilterType.None)
      {
        if (this.ActiveSortType == ADGVFilterMenuSortType.None)
          this.filterImage = (Image) Resources.AddFilter;
        else if (this.ActiveSortType == ADGVFilterMenuSortType.ASC)
          this.filterImage = (Image) Resources.ASC;
        else
          this.filterImage = (Image) Resources.DESC;
      }
      else if (this.ActiveSortType == ADGVFilterMenuSortType.None)
        this.filterImage = (Image) Resources.Filter;
      else if (this.ActiveSortType == ADGVFilterMenuSortType.ASC)
        this.filterImage = (Image) Resources.FilterASC;
      else
        this.filterImage = (Image) Resources.FilterDESC;
    }

    private void FilterMenu_FilterChanged(object sender, EventArgs e)
    {
      this.RefreshImage();
      this.RepaintCell();
      if (!this.FilterEnabled || this.FilterChanged == null)
        return;
      this.FilterChanged((object) this, new ADGVFilterEventArgs(this.FilterMenu, this.OwningColumn));
    }

    private void FilterMenu_SortChanged(object sender, EventArgs e)
    {
      this.RefreshImage();
      this.RepaintCell();
      if (!this.FilterEnabled || this.SortChanged == null)
        return;
      this.SortChanged((object) this, new ADGVFilterEventArgs(this.FilterMenu, this.OwningColumn));
    }

    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
      if (this.FilterEnabled && this.SortGlyphDirection != SortOrder.None)
        this.SortGlyphDirection = SortOrder.None;
      base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
      if (!this.FilterEnabled || !paintParts.HasFlag((Enum) DataGridViewPaintParts.ContentBackground))
        return;
      this.filterButtonOffsetBounds = this.GetFilterBounds(true);
      this.filterButtonImageBounds = this.GetFilterBounds(false);
      Rectangle buttonOffsetBounds = this.filterButtonOffsetBounds;
      if (!clipBounds.IntersectsWith(buttonOffsetBounds))
        return;
      ControlPaint.DrawBorder(graphics, buttonOffsetBounds, Color.Gray, ButtonBorderStyle.Solid);
      buttonOffsetBounds.Inflate(-1, -1);
      using (Brush brush = (Brush) new SolidBrush(this.filterButtonOver ? Color.LightGray : Color.White))
        graphics.FillRectangle(brush, buttonOffsetBounds);
      graphics.DrawImage(this.filterImage, buttonOffsetBounds);
    }

    private Rectangle GetFilterBounds(bool withOffset = true)
    {
      Rectangle displayRectangle = this.DataGridView.GetCellDisplayRectangle(this.ColumnIndex, -1, false);
      return new Rectangle(new Point((withOffset ? displayRectangle.Right : this.OwningColumn.Width) - this.filterButtonImageSize.Width - this.filterButtonMargin.Right, (withOffset ? displayRectangle.Bottom : displayRectangle.Height) - this.filterButtonImageSize.Height - this.filterButtonMargin.Bottom), this.filterButtonImageSize);
    }

    protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
    {
      if (this.FilterEnabled)
      {
        if (this.filterButtonImageBounds.Contains(e.X, e.Y) && !this.filterButtonOver)
        {
          this.filterButtonOver = true;
          this.RepaintCell();
        }
        else if (!this.filterButtonImageBounds.Contains(e.X, e.Y) && this.filterButtonOver)
        {
          this.filterButtonOver = false;
          this.RepaintCell();
        }
      }
      base.OnMouseMove(e);
    }

    protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
    {
      if (this.FilterEnabled)
      {
        if (!this.filterButtonImageBounds.Contains(e.X, e.Y) || e.Button != MouseButtons.Left || this.filterButtonPressed)
          return;
        this.filterButtonPressed = true;
        this.filterButtonOver = true;
        this.RepaintCell();
      }
      else
        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
    {
      if (this.FilterEnabled)
      {
        if (e.Button != MouseButtons.Left || !this.filterButtonPressed)
          return;
        this.filterButtonPressed = false;
        this.filterButtonOver = false;
        this.RepaintCell();
        if (!this.filterButtonImageBounds.Contains(e.X, e.Y) || this.FilterPopup == null)
          return;
        this.FilterPopup((object) this, new ADGVFilterEventArgs(this.FilterMenu, this.OwningColumn));
      }
      else
        base.OnMouseUp(e);
    }

    protected override void OnMouseLeave(int rowIndex)
    {
      if (this.FilterEnabled && this.filterButtonOver)
      {
        this.filterButtonOver = false;
        this.RepaintCell();
      }
      base.OnMouseLeave(rowIndex);
    }

    public void SetLoadedFilterMode(bool Enabled)
    {
      this.FilterMenu.SetLoadedFilterMode(Enabled);
      this.RefreshImage();
      this.RepaintCell();
    }
  }
}
