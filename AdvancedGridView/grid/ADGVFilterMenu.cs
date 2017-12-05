// Decompiled with JetBrains decompiler
// Type: ADGV.ADGVFilterMenu
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\AdvancedGridView\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ADGV
{
  public class ADGVFilterMenu : ContextMenuStrip
  {
    private static Point resizeStartPoint = new Point(1, 1);
    private Point resizeEndPoint = new Point(-1, -1);
    private ToolStripMenuItem SortASCMenuItem;
    private ToolStripMenuItem SortDESCMenuItem;
    private ToolStripMenuItem CancelSortMenuItem;
    private ToolStripSeparator toolStripSeparator1MenuItem;
    private ToolStripMenuItem CancelFilterMenuItem;
    private ToolStripMenuItem FiltersMenuItem;
    private ToolStripMenuItem SetupFilterMenuItem;
    private ToolStripSeparator toolStripSeparator2MenuItem;
    private ToolStripSeparator toolStripSeparator3MenuItem;
    private ToolStripMenuItem lastfilter1MenuItem;
    private ToolStripMenuItem lastfilter2MenuItem;
    private ToolStripMenuItem lastfilter3MenuItem;
    private ToolStripMenuItem lastfilter4MenuItem;
    private ToolStripMenuItem lastfilter5MenuItem;
    private System.Windows.Forms.TreeView CheckList;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private ToolStripControlHost CheckFilterListControlHost;
    private ToolStripControlHost CheckFilterListButtonsControlHost;
    private ToolStripControlHost ResizeBoxControlHost;
    private Panel CheckFilterListPanel;
    private Panel CheckFilterListButtonsPanel;
    private Dictionary<int, string> months;
    private ResourceManager RM;
    private ADGVFilterMenuFilterType activeFilterType;
    private ADGVFilterMenuSortType activeSortType;
    private string sortString;
    private string filterString;
    private TripleTreeNode[] startingNodes;
    private TripleTreeNode[] filterNodes;

    public Type DataType { get; private set; }

    public bool DateWithTime { get; set; }

    public bool TimeFilter { get; set; }

    public ADGVFilterMenuSortType ActiveSortType
    {
      get
      {
        return this.activeSortType;
      }
    }

    public ADGVFilterMenuFilterType ActiveFilterType
    {
      get
      {
        return this.activeFilterType;
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
        this.CancelSortMenuItem.Enabled = value != null && value.Length > 0;
        this.sortString = value;
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
        this.CancelFilterMenuItem.Enabled = value != null && value.Length > 0;
        this.filterString = value;
      }
    }

    public event EventHandler SortChanged;

    public event EventHandler FilterChanged;

    public ADGVFilterMenu(Type DataType)
    {
      this.DataType = DataType;
      this.DateWithTime = true;
      this.TimeFilter = false;
      this.RM = new ResourceManager("ADGV.Localization.ADGVStrings", typeof (ADGVFilterMenu).Assembly);
      this.months = new Dictionary<int, string>();
      this.months.Add(1, this.RM.GetString("month1"));
      this.months.Add(2, this.RM.GetString("month2"));
      this.months.Add(3, this.RM.GetString("month3"));
      this.months.Add(4, this.RM.GetString("month4"));
      this.months.Add(5, this.RM.GetString("month5"));
      this.months.Add(6, this.RM.GetString("month6"));
      this.months.Add(7, this.RM.GetString("month7"));
      this.months.Add(8, this.RM.GetString("month8"));
      this.months.Add(9, this.RM.GetString("month9"));
      this.months.Add(10, this.RM.GetString("month10"));
      this.months.Add(11, this.RM.GetString("month11"));
      this.months.Add(12, this.RM.GetString("month12"));
      this.SortASCMenuItem = new ToolStripMenuItem();
      this.SortDESCMenuItem = new ToolStripMenuItem();
      this.CancelSortMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1MenuItem = new ToolStripSeparator();
      this.CancelFilterMenuItem = new ToolStripMenuItem();
      this.FiltersMenuItem = new ToolStripMenuItem();
      this.SetupFilterMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2MenuItem = new ToolStripSeparator();
      this.lastfilter1MenuItem = new ToolStripMenuItem();
      this.lastfilter2MenuItem = new ToolStripMenuItem();
      this.lastfilter3MenuItem = new ToolStripMenuItem();
      this.lastfilter4MenuItem = new ToolStripMenuItem();
      this.lastfilter5MenuItem = new ToolStripMenuItem();
      this.toolStripSeparator3MenuItem = new ToolStripSeparator();
      this.CheckList = new System.Windows.Forms.TreeView();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.CheckFilterListPanel = new Panel();
      this.CheckFilterListButtonsPanel = new Panel();
      this.CheckFilterListButtonsControlHost = new ToolStripControlHost((Control) this.CheckFilterListButtonsPanel);
      this.CheckFilterListControlHost = new ToolStripControlHost((Control) this.CheckFilterListPanel);
      this.ResizeBoxControlHost = new ToolStripControlHost(new Control());
      this.SuspendLayout();
      this.BackColor = SystemColors.ControlLightLight;
      this.AutoSize = false;
      this.Padding = new Padding(0);
      this.Margin = new Padding(0);
      this.Size = new Size(287, 340);
      this.Closed += new ToolStripDropDownClosedEventHandler(this.FilterContextMenu_Closed);
      this.LostFocus += new EventHandler(this.FilterContextMenu_LostFocus);
      this.SortASCMenuItem.Name = nameof (SortASCMenuItem);
      this.SortASCMenuItem.AutoSize = false;
      this.SortASCMenuItem.Size = new Size(this.Width - 1, 22);
      this.SortASCMenuItem.Click += new EventHandler(this.SortASCMenuItem_Click);
      this.SortASCMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
      this.SortASCMenuItem.ImageScaling = ToolStripItemImageScaling.None;
      this.SortDESCMenuItem.Name = nameof (SortDESCMenuItem);
      this.SortDESCMenuItem.AutoSize = false;
      this.SortDESCMenuItem.Size = new Size(this.Width - 1, 22);
      this.SortDESCMenuItem.Click += new EventHandler(this.SortDESCMenuItem_Click);
      this.SortDESCMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
      this.SortDESCMenuItem.ImageScaling = ToolStripItemImageScaling.None;
      this.CancelSortMenuItem.Name = nameof (CancelSortMenuItem);
      this.CancelSortMenuItem.Enabled = false;
      this.CancelSortMenuItem.AutoSize = false;
      this.CancelSortMenuItem.Size = new Size(this.Width - 1, 22);
      this.CancelSortMenuItem.Text = this.RM.GetString("cancelsortmenuitem_text");
      this.CancelSortMenuItem.Click += new EventHandler(this.CancelSortMenuItem_Click);
      this.CancelSortMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
      this.toolStripSeparator1MenuItem.Name = nameof (toolStripSeparator1MenuItem);
      this.toolStripSeparator1MenuItem.Size = new Size(this.Width - 4, 6);
      this.CancelFilterMenuItem.Name = nameof (CancelFilterMenuItem);
      this.CancelFilterMenuItem.Enabled = false;
      this.CancelFilterMenuItem.AutoSize = false;
      this.CancelFilterMenuItem.Size = new Size(this.Width - 1, 22);
      this.CancelFilterMenuItem.Text = this.RM.GetString("cancelfiltermenuitem_text");
      this.CancelFilterMenuItem.Click += new EventHandler(this.CancelFilterMenuItem_Click);
      this.CancelFilterMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
      this.SetupFilterMenuItem.Name = nameof (SetupFilterMenuItem);
      this.SetupFilterMenuItem.Size = new Size(152, 22);
      this.SetupFilterMenuItem.Text = this.RM.GetString("setupfiltermenuitem_text");
      this.SetupFilterMenuItem.Click += new EventHandler(this.SetupFilterMenuItem_Click);
      this.toolStripSeparator2MenuItem.Name = nameof (toolStripSeparator2MenuItem);
      this.toolStripSeparator2MenuItem.Size = new Size(149, 6);
      this.toolStripSeparator2MenuItem.Visible = false;
      this.lastfilter1MenuItem.Name = nameof (lastfilter1MenuItem);
      this.lastfilter1MenuItem.Size = new Size(152, 22);
      this.lastfilter1MenuItem.Tag = (object) "0";
      this.lastfilter1MenuItem.Text = (string) null;
      this.lastfilter1MenuItem.Visible = false;
      this.lastfilter1MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
      this.lastfilter1MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
      this.lastfilter1MenuItem.VisibleChanged += new EventHandler(this.lastfilter1MenuItem_VisibleChanged);
      this.lastfilter2MenuItem.Name = nameof (lastfilter2MenuItem);
      this.lastfilter2MenuItem.Size = new Size(152, 22);
      this.lastfilter2MenuItem.Tag = (object) "1";
      this.lastfilter2MenuItem.Text = (string) null;
      this.lastfilter2MenuItem.Visible = false;
      this.lastfilter2MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
      this.lastfilter2MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
      this.lastfilter3MenuItem.Name = nameof (lastfilter3MenuItem);
      this.lastfilter3MenuItem.Size = new Size(152, 22);
      this.lastfilter3MenuItem.Tag = (object) "2";
      this.lastfilter3MenuItem.Text = (string) null;
      this.lastfilter3MenuItem.Visible = false;
      this.lastfilter3MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
      this.lastfilter3MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
      this.lastfilter4MenuItem.Name = nameof (lastfilter4MenuItem);
      this.lastfilter4MenuItem.Size = new Size(152, 22);
      this.lastfilter4MenuItem.Tag = (object) "3";
      this.lastfilter4MenuItem.Text = (string) null;
      this.lastfilter4MenuItem.Visible = false;
      this.lastfilter4MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
      this.lastfilter4MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
      this.lastfilter5MenuItem.Name = nameof (lastfilter5MenuItem);
      this.lastfilter5MenuItem.Size = new Size(152, 22);
      this.lastfilter5MenuItem.Tag = (object) "4";
      this.lastfilter5MenuItem.Text = (string) null;
      this.lastfilter5MenuItem.Visible = false;
      this.lastfilter5MenuItem.Click += new EventHandler(this.lastfilter1MenuItem_Click);
      this.lastfilter5MenuItem.TextChanged += new EventHandler(this.lastfilter1MenuItem_TextChanged);
      this.FiltersMenuItem.Name = nameof (FiltersMenuItem);
      this.FiltersMenuItem.AutoSize = false;
      this.FiltersMenuItem.Size = new Size(this.Width - 1, 22);
      this.FiltersMenuItem.Image = (Image) ADGV.Properties.Resources.Filter;
      this.FiltersMenuItem.ImageScaling = ToolStripItemImageScaling.None;
      this.FiltersMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.SetupFilterMenuItem,
        (ToolStripItem) this.toolStripSeparator2MenuItem,
        (ToolStripItem) this.lastfilter1MenuItem,
        (ToolStripItem) this.lastfilter2MenuItem,
        (ToolStripItem) this.lastfilter3MenuItem,
        (ToolStripItem) this.lastfilter4MenuItem,
        (ToolStripItem) this.lastfilter5MenuItem
      });
      this.FiltersMenuItem.MouseEnter += new EventHandler(this.SortASCMenuItem_MouseEnter);
      this.FiltersMenuItem.Paint += new PaintEventHandler(this.FiltersMenuItem_Paint);
      this.toolStripSeparator3MenuItem.Name = nameof (toolStripSeparator3MenuItem);
      this.toolStripSeparator3MenuItem.Size = new Size(this.Width - 4, 6);
      this.okButton.Name = nameof (okButton);
      this.okButton.BackColor = Control.DefaultBackColor;
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Margin = new Padding(0);
      this.okButton.Size = new Size(75, 23);
      this.okButton.Text = this.RM.GetString("okbutton_text");
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.cancelButton.Name = nameof (cancelButton);
      this.cancelButton.BackColor = Control.DefaultBackColor;
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Margin = new Padding(0);
      this.cancelButton.Size = new Size(75, 23);
      this.cancelButton.Text = this.RM.GetString("cancelbutton_text");
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.ResizeBoxControlHost.Name = nameof (ResizeBoxControlHost);
      this.ResizeBoxControlHost.Control.Cursor = Cursors.SizeNWSE;
      this.ResizeBoxControlHost.AutoSize = false;
      this.ResizeBoxControlHost.Padding = new Padding(0);
      this.ResizeBoxControlHost.Margin = new Padding(this.Width - 45, 0, 0, 0);
      this.ResizeBoxControlHost.Size = new Size(10, 10);
      this.ResizeBoxControlHost.Paint += new PaintEventHandler(this.ResizeGrip_Paint);
      this.ResizeBoxControlHost.MouseDown += new MouseEventHandler(this.ResizePictureBox_MouseDown);
      this.ResizeBoxControlHost.MouseUp += new MouseEventHandler(this.ResizePictureBox_MouseUp);
      this.ResizeBoxControlHost.MouseMove += new MouseEventHandler(this.ResizePictureBox_MouseMove);
      this.CheckFilterListControlHost.Name = nameof (CheckFilterListControlHost);
      this.CheckFilterListControlHost.AutoSize = false;
      this.CheckFilterListControlHost.Size = new Size(this.Width - 35, 180);
      this.CheckFilterListControlHost.Padding = new Padding(0);
      this.CheckFilterListControlHost.Margin = new Padding(0);
      this.CheckFilterListButtonsControlHost.Name = nameof (CheckFilterListButtonsControlHost);
      this.CheckFilterListButtonsControlHost.AutoSize = false;
      this.CheckFilterListButtonsControlHost.Size = new Size(this.Width - 35, 24);
      this.CheckFilterListButtonsControlHost.Padding = new Padding(0);
      this.CheckFilterListButtonsControlHost.Margin = new Padding(0);
      this.CheckFilterListPanel.Name = nameof (CheckFilterListPanel);
      this.CheckFilterListPanel.AutoSize = false;
      this.CheckFilterListPanel.Size = this.CheckFilterListControlHost.Size;
      this.CheckFilterListPanel.Padding = new Padding(0);
      this.CheckFilterListPanel.Margin = new Padding(0);
      this.CheckFilterListPanel.BackColor = this.BackColor;
      this.CheckFilterListPanel.BorderStyle = BorderStyle.None;
      this.CheckFilterListPanel.Controls.Add((Control) this.CheckList);
      this.CheckList.Name = nameof (CheckList);
      this.CheckList.AutoSize = false;
      this.CheckList.Padding = new Padding(0);
      this.CheckList.Margin = new Padding(0);
      this.CheckList.Bounds = new Rectangle(4, 4, this.CheckFilterListPanel.Width - 8, this.CheckFilterListPanel.Height - 8);
      this.CheckList.StateImageList = this.GetCheckImages();
      this.CheckList.CheckBoxes = false;
      this.CheckList.MouseLeave += new EventHandler(this.CheckList_MouseLeave);
      this.CheckList.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.CheckList_NodeMouseClick);
      this.CheckList.KeyDown += new KeyEventHandler(this.CheckList_KeyDown);
      this.CheckList.MouseEnter += new EventHandler(this.CheckList_MouseEnter);
      this.CheckList.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.CheckList_NodeMouseDoubleClick);
      this.CheckFilterListButtonsPanel.Name = nameof (CheckFilterListButtonsPanel);
      this.CheckFilterListButtonsPanel.AutoSize = false;
      this.CheckFilterListButtonsPanel.Size = this.CheckFilterListButtonsControlHost.Size;
      this.CheckFilterListButtonsPanel.Padding = new Padding(0);
      this.CheckFilterListButtonsPanel.Margin = new Padding(0);
      this.CheckFilterListButtonsPanel.BackColor = this.BackColor;
      this.CheckFilterListButtonsPanel.BorderStyle = BorderStyle.None;
      this.CheckFilterListButtonsPanel.Controls.AddRange(new Control[2]
      {
        (Control) this.okButton,
        (Control) this.cancelButton
      });
      this.okButton.Location = new Point(this.CheckFilterListButtonsPanel.Width - 164, 0);
      this.cancelButton.Location = new Point(this.CheckFilterListButtonsPanel.Width - 79, 0);
      this.Items.AddRange(new ToolStripItem[10]
      {
        (ToolStripItem) this.SortASCMenuItem,
        (ToolStripItem) this.SortDESCMenuItem,
        (ToolStripItem) this.CancelSortMenuItem,
        (ToolStripItem) this.toolStripSeparator1MenuItem,
        (ToolStripItem) this.CancelFilterMenuItem,
        (ToolStripItem) this.FiltersMenuItem,
        (ToolStripItem) this.toolStripSeparator3MenuItem,
        (ToolStripItem) this.CheckFilterListControlHost,
        (ToolStripItem) this.CheckFilterListButtonsControlHost,
        (ToolStripItem) this.ResizeBoxControlHost
      });
      this.ResumeLayout(false);
      if (this.DataType == typeof (DateTime))
      {
        this.FiltersMenuItem.Text = this.RM.GetString("filtersmenuitem_text_datetime");
        this.SortASCMenuItem.Text = this.RM.GetString("sortascmenuitem_text_datetime");
        this.SortDESCMenuItem.Text = this.RM.GetString("sortdescmenuitem_text_datetime");
        this.SortASCMenuItem.Image = (Image) ADGV.Properties.Resources.ASCnum;
        this.SortDESCMenuItem.Image = (Image) ADGV.Properties.Resources.DESCnum;
      }
      else if (this.DataType == typeof (bool))
      {
        this.FiltersMenuItem.Text = this.RM.GetString("filtersmenuitem_text_text");
        this.SortASCMenuItem.Text = this.RM.GetString("sortascmenuitem_text_boolean");
        this.SortDESCMenuItem.Text = this.RM.GetString("sortdescmenuitem_text_boolean");
        this.SortASCMenuItem.Image = (Image) ADGV.Properties.Resources.ASCbool;
        this.SortDESCMenuItem.Image = (Image) ADGV.Properties.Resources.DESCbool;
      }
      else if (this.DataType == typeof (int) || this.DataType == typeof (long) || (this.DataType == typeof (short) || this.DataType == typeof (uint)) || (this.DataType == typeof (ulong) || this.DataType == typeof (ushort) || (this.DataType == typeof (byte) || this.DataType == typeof (sbyte))) || (this.DataType == typeof (Decimal) || this.DataType == typeof (float) || this.DataType == typeof (double)))
      {
        this.FiltersMenuItem.Text = this.RM.GetString("filtersmenuitem_text_numeric");
        this.SortASCMenuItem.Text = this.RM.GetString("sortascmenuitem_text_numeric");
        this.SortDESCMenuItem.Text = this.RM.GetString("sortdescmenuitem_text_numeric");
        this.SortASCMenuItem.Image = (Image) ADGV.Properties.Resources.ASCnum;
        this.SortDESCMenuItem.Image = (Image) ADGV.Properties.Resources.DESCnum;
      }
      else
      {
        this.FiltersMenuItem.Text = this.RM.GetString("filtersmenuitem_text_text");
        this.SortASCMenuItem.Text = this.RM.GetString("sortascmenuitem_text_text");
        this.SortDESCMenuItem.Text = this.RM.GetString("sortdescmenuitem_text");
        this.SortASCMenuItem.Image = (Image) ADGV.Properties.Resources.ASCtxt;
        this.SortDESCMenuItem.Image = (Image) ADGV.Properties.Resources.DESCtxt;
      }
      this.FiltersMenuItem.Enabled = this.DataType != typeof (bool);
      this.FiltersMenuItem.Checked = this.ActiveFilterType == ADGVFilterMenuFilterType.Custom;
      this.MinimumSize = new Size(this.PreferredSize.Width, this.PreferredSize.Height);
      this.ResizeMenu(this.MinimumSize.Width, this.MinimumSize.Height);
    }

    private void CheckList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      TripleTreeNode node = e.Node as TripleTreeNode;
      this.SetNodesCheckedState(this.CheckList.Nodes, false);
      node.CheckState = CheckState.Unchecked;
      this.NodeCheckChange(node);
      this.okButton_Click((object) this, new EventArgs());
    }

    private void CheckList_MouseEnter(object sender, EventArgs e)
    {
      this.CheckList.Focus();
    }

    private ImageList GetCheckImages()
    {
      ImageList imageList = new ImageList();
      Bitmap bitmap1 = new Bitmap(16, 16);
      Bitmap bitmap2 = new Bitmap(16, 16);
      Bitmap bitmap3 = new Bitmap(16, 16);
      using (Bitmap bitmap4 = new Bitmap(16, 16))
      {
        using (Graphics g = Graphics.FromImage((Image) bitmap4))
        {
          CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.UncheckedNormal);
          bitmap1 = (Bitmap) bitmap4.Clone();
          CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.CheckedNormal);
          bitmap2 = (Bitmap) bitmap4.Clone();
          CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.MixedNormal);
          bitmap3 = (Bitmap) bitmap4.Clone();
        }
      }
      imageList.Images.Add("uncheck", (Image) bitmap1);
      imageList.Images.Add("check", (Image) bitmap2);
      imageList.Images.Add("mixed", (Image) bitmap3);
      return imageList;
    }

    public static IEnumerable<DataGridViewCell> GetValuesForFilter(DataGridView grid, string columnName)
    {
      return grid.Rows.Cast<DataGridViewRow>().Where<DataGridViewRow>((Func<DataGridViewRow, bool>) (r => !r.IsNewRow)).Select<DataGridViewRow, DataGridViewCell>((Func<DataGridViewRow, DataGridViewCell>) (r => r.Cells[columnName]));
    }

    public void Show(Control control, int x, int y, IEnumerable<DataGridViewCell> vals)
    {
      this.RefreshFilterMenu(vals);
      if (this.activeFilterType == ADGVFilterMenuFilterType.Custom)
        this.SetNodesCheckedState(this.CheckList.Nodes, false);
      this.DuplicateNodes();
      this.Show(control, x, y);
    }

    public void Show(Control control, int x, int y, bool RestoreFilter)
    {
      if (RestoreFilter)
        this.RestoreFilterNodes();
      this.DuplicateNodes();
      this.Show(control, x, y);
    }

    private void RefreshFilterMenu(IEnumerable<DataGridViewCell> vals)
    {
      this.CheckList.BeginUpdate();
      this.CheckList.Nodes.Clear();
      if (vals != null)
      {
        TripleTreeNode allsNode = TripleTreeNode.CreateAllsNode(this.RM.GetString("tripletreenode_allnode_text") + "            ", CheckState.Checked);
        allsNode.NodeFont = new Font(this.CheckList.Font, FontStyle.Bold);
        this.CheckList.Nodes.Add((TreeNode) allsNode);
        if (vals.Count<DataGridViewCell>() > 0)
        {
          IEnumerable<DataGridViewCell> source1 = vals.Where<DataGridViewCell>((Func<DataGridViewCell, bool>) (c =>
          {
            if (c.Value != null)
              return c.Value != DBNull.Value;
            return false;
          }));
          if (vals.Count<DataGridViewCell>() != source1.Count<DataGridViewCell>())
          {
            TripleTreeNode emptysNode = TripleTreeNode.CreateEmptysNode(this.RM.GetString("tripletreenode_nullnode_text") + "               ", CheckState.Checked);
            emptysNode.NodeFont = new Font(this.CheckList.Font, FontStyle.Bold);
            this.CheckList.Nodes.Add((TreeNode) emptysNode);
          }
          if (this.DataType == typeof (DateTime))
          {
            foreach (IGrouping<int, DataGridViewCell> source2 in (IEnumerable<IGrouping<int, DataGridViewCell>>) source1.GroupBy<DataGridViewCell, int>((Func<DataGridViewCell, int>) (year => ((DateTime) year.Value).Year)).OrderBy<IGrouping<int, DataGridViewCell>, int>((Func<IGrouping<int, DataGridViewCell>, int>) (y => y.Key)))
            {
              TripleTreeNode yearNode = TripleTreeNode.CreateYearNode(source2.Key.ToString(), (object) source2.Key, CheckState.Checked);
              this.CheckList.Nodes.Add((TreeNode) yearNode);
              foreach (IGrouping<int, DataGridViewCell> source3 in (IEnumerable<IGrouping<int, DataGridViewCell>>) source2.GroupBy<DataGridViewCell, int>((Func<DataGridViewCell, int>) (month => ((DateTime) month.Value).Month)).OrderBy<IGrouping<int, DataGridViewCell>, int>((Func<IGrouping<int, DataGridViewCell>, int>) (m => m.Key)))
              {
                TripleTreeNode childNode1 = yearNode.CreateChildNode(this.months[source3.Key], (object) source3.Key);
                foreach (IGrouping<int, DataGridViewCell> source4 in (IEnumerable<IGrouping<int, DataGridViewCell>>) source3.GroupBy<DataGridViewCell, int>((Func<DataGridViewCell, int>) (day => ((DateTime) day.Value).Day)).OrderBy<IGrouping<int, DataGridViewCell>, int>((Func<IGrouping<int, DataGridViewCell>, int>) (d => d.Key)))
                {
                  if (!this.TimeFilter)
                    childNode1.CreateChildNode(source4.Key.ToString("D2"), source4.First<DataGridViewCell>().Value);
                  else if (!this.DateWithTime)
                  {
                    childNode1.CreateChildNode(source4.Key.ToString("D2"), source4.First<DataGridViewCell>().Value).CreateChildNode("## " + this.RM.GetString("checknodetree_hour"), (object) null).CreateChildNode("## " + this.RM.GetString("checknodetree_minute"), (object) null).CreateChildNode("## " + this.RM.GetString("checknodetree_second"), (object) null).CreateChildNode("### " + this.RM.GetString("checknodetree_millisecond"), (object) null);
                  }
                  else
                  {
                    TripleTreeNode childNode2 = childNode1.CreateChildNode(source4.Key.ToString("D2"), (object) source4.Key);
                    foreach (IGrouping<int, DataGridViewCell> source5 in (IEnumerable<IGrouping<int, DataGridViewCell>>) source4.GroupBy<DataGridViewCell, int>((Func<DataGridViewCell, int>) (hour => ((DateTime) hour.Value).Hour)).OrderBy<IGrouping<int, DataGridViewCell>, int>((Func<IGrouping<int, DataGridViewCell>, int>) (h => h.Key)))
                    {
                      TripleTreeNode childNode3 = childNode2.CreateChildNode(source5.Key.ToString("D2") + " " + this.RM.GetString("checknodetree_hour"), (object) source5.Key);
                      foreach (IGrouping<int, DataGridViewCell> source6 in (IEnumerable<IGrouping<int, DataGridViewCell>>) source5.GroupBy<DataGridViewCell, int>((Func<DataGridViewCell, int>) (min => ((DateTime) min.Value).Minute)).OrderBy<IGrouping<int, DataGridViewCell>, int>((Func<IGrouping<int, DataGridViewCell>, int>) (mn => mn.Key)))
                      {
                        TripleTreeNode childNode4 = childNode3.CreateChildNode(source6.Key.ToString("D2") + " " + this.RM.GetString("checknodetree_minute"), (object) source6.Key);
                        foreach (IGrouping<int, DataGridViewCell> source7 in (IEnumerable<IGrouping<int, DataGridViewCell>>) source6.GroupBy<DataGridViewCell, int>((Func<DataGridViewCell, int>) (sec => ((DateTime) sec.Value).Second)).OrderBy<IGrouping<int, DataGridViewCell>, int>((Func<IGrouping<int, DataGridViewCell>, int>) (s => s.Key)))
                        {
                          TripleTreeNode childNode5 = childNode4.CreateChildNode(source7.Key.ToString("D2") + " " + this.RM.GetString("checknodetree_second"), (object) source7.Key);
                          foreach (IGrouping<int, DataGridViewCell> source8 in (IEnumerable<IGrouping<int, DataGridViewCell>>) source7.GroupBy<DataGridViewCell, int>((Func<DataGridViewCell, int>) (msec => ((DateTime) msec.Value).Millisecond)).OrderBy<IGrouping<int, DataGridViewCell>, int>((Func<IGrouping<int, DataGridViewCell>, int>) (ms => ms.Key)))
                            childNode5.CreateChildNode(source8.Key.ToString("D3") + " " + this.RM.GetString("checknodetree_millisecond"), source8.First<DataGridViewCell>().Value);
                        }
                      }
                    }
                  }
                }
              }
            }
          }
          else if (this.DataType == typeof (bool))
          {
            IEnumerable<DataGridViewCell> source2 = source1.Where<DataGridViewCell>((Func<DataGridViewCell, bool>) (c => (bool) c.Value));
            if (source2.Count<DataGridViewCell>() != source1.Count<DataGridViewCell>())
              this.CheckList.Nodes.Add((TreeNode) TripleTreeNode.CreateNode(this.RM.GetString("tripletreenode_boolean_false"), (object) false, CheckState.Checked));
            if (source2.Count<DataGridViewCell>() > 0)
              this.CheckList.Nodes.Add((TreeNode) TripleTreeNode.CreateNode(this.RM.GetString("tripletreenode_boolean_true"), (object) true, CheckState.Checked));
          }
          else
          {
            foreach (IGrouping<object, DataGridViewCell> source2 in (IEnumerable<IGrouping<object, DataGridViewCell>>) source1.GroupBy<DataGridViewCell, object>((Func<DataGridViewCell, object>) (c => c.Value)).OrderBy<IGrouping<object, DataGridViewCell>, object>((Func<IGrouping<object, DataGridViewCell>, object>) (g => g.Key)))
              this.CheckList.Nodes.Add((TreeNode) TripleTreeNode.CreateNode(source2.First<DataGridViewCell>().FormattedValue.ToString(), source2.Key, CheckState.Checked));
          }
        }
      }
      this.CheckList.EndUpdate();
    }

    private void DuplicateNodes()
    {
      this.startingNodes = new TripleTreeNode[this.CheckList.Nodes.Count];
      int index = 0;
      foreach (TripleTreeNode node in this.CheckList.Nodes)
      {
        this.startingNodes[index] = node.Clone();
        ++index;
      }
    }

    private void DuplicateFilterNodes()
    {
      this.filterNodes = new TripleTreeNode[this.CheckList.Nodes.Count];
      int index = 0;
      foreach (TripleTreeNode node in this.CheckList.Nodes)
      {
        this.filterNodes[index] = node.Clone();
        ++index;
      }
    }

    private void RestoreNodes()
    {
      this.CheckList.Nodes.Clear();
      if (this.startingNodes == null)
        return;
      this.CheckList.Nodes.AddRange((TreeNode[]) this.startingNodes);
    }

    private void RestoreFilterNodes()
    {
      this.CheckList.Nodes.Clear();
      if (this.filterNodes == null)
        return;
      this.CheckList.Nodes.AddRange((TreeNode[]) this.filterNodes);
    }

    private TripleTreeNode GetAllsNode()
    {
      TripleTreeNode tripleTreeNode = (TripleTreeNode) null;
      int num = 0;
      foreach (TripleTreeNode node in this.CheckList.Nodes)
      {
        if (node.NodeType == TripleTreeNodeType.AllsNode)
        {
          tripleTreeNode = node;
          break;
        }
        if (num <= 2)
          ++num;
        else
          break;
      }
      return tripleTreeNode;
    }

    private TripleTreeNode GetNullNode()
    {
      TripleTreeNode tripleTreeNode = (TripleTreeNode) null;
      int num = 0;
      foreach (TripleTreeNode node in this.CheckList.Nodes)
      {
        if (node.NodeType == TripleTreeNodeType.EmptysNode)
        {
          tripleTreeNode = node;
          break;
        }
        if (num <= 2)
          ++num;
        else
          break;
      }
      return tripleTreeNode;
    }

    private void SetNodesCheckedState(TreeNodeCollection nodes, bool isChecked)
    {
      foreach (TripleTreeNode node in nodes)
      {
        node.Checked = isChecked;
        if (node.Nodes != null && node.Nodes.Count > 0)
          this.SetNodesCheckedState(node.Nodes, isChecked);
      }
    }

    private CheckState UpdateNodesCheckState(TreeNodeCollection nodes)
    {
      CheckState checkState = CheckState.Unchecked;
      bool flag1 = true;
      bool flag2 = true;
      foreach (TripleTreeNode node in nodes)
      {
        if (node.NodeType != TripleTreeNodeType.AllsNode)
        {
          if (node.Nodes.Count > 0)
            node.CheckState = this.UpdateNodesCheckState(node.Nodes);
          if (flag1)
          {
            checkState = node.CheckState;
            flag1 = false;
          }
          else if (checkState != node.CheckState)
            flag2 = false;
        }
      }
      if (flag2)
        return checkState;
      return CheckState.Indeterminate;
    }

    private void RefreshNodesState()
    {
      CheckState checkState = this.UpdateNodesCheckState(this.CheckList.Nodes);
      this.GetAllsNode().CheckState = checkState;
      this.okButton.Enabled = checkState != CheckState.Unchecked;
    }

    private void CheckList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      TreeViewHitTestInfo treeViewHitTestInfo = this.CheckList.HitTest(e.X, e.Y);
      if (treeViewHitTestInfo == null || treeViewHitTestInfo.Location != TreeViewHitTestLocations.StateImage)
        return;
      this.NodeCheckChange(e.Node as TripleTreeNode);
    }

    private void CheckList_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Space)
        return;
      this.NodeCheckChange(this.CheckList.SelectedNode as TripleTreeNode);
    }

    private void NodeCheckChange(TripleTreeNode node)
    {
      node.CheckState = node.CheckState != CheckState.Checked ? CheckState.Checked : CheckState.Unchecked;
      if (node.NodeType == TripleTreeNodeType.AllsNode)
      {
        this.SetNodesCheckedState(this.CheckList.Nodes, node.Checked);
        this.okButton.Enabled = node.Checked;
      }
      else
      {
        if (node.Nodes.Count > 0)
          this.SetNodesCheckedState(node.Nodes, node.Checked);
        this.RefreshNodesState();
      }
    }

    private string CreateFilterString(IEnumerable<TripleTreeNode> nodes)
    {
      StringBuilder stringBuilder = new StringBuilder("");
      string str = !(this.DataType == typeof (DateTime)) || this.TimeFilter || !this.DateWithTime ? ", " : " OR ";
      if (nodes != null && nodes.Count<TripleTreeNode>() > 0)
      {
        if (this.DataType == typeof (DateTime))
        {
          foreach (TripleTreeNode node in nodes)
          {
            if (node.Checked && (node.NodeType == TripleTreeNodeType.DayDateTimeNode && (!this.TimeFilter || !this.DateWithTime) || node.NodeType == TripleTreeNodeType.MSecDateTimeNode && this.TimeFilter && this.DateWithTime))
            {
              if (this.TimeFilter && this.DateWithTime)
                stringBuilder.Append("'" + ((DateTime) node.Value).ToString("o") + "'" + str);
              else if (!this.TimeFilter && this.DateWithTime)
                stringBuilder.Append("(Convert([{0}], System.String) LIKE '" + ((DateTime) node.Value).ToShortDateString() + "%')" + str);
              else
                stringBuilder.Append("'" + ((DateTime) node.Value).ToShortDateString() + "'" + str);
            }
            else if (node.CheckState != CheckState.Unchecked && node.Nodes.Count > 0)
            {
              string filterString = this.CreateFilterString((IEnumerable<TripleTreeNode>) ParallelEnumerable.Cast<TripleTreeNode>(node.Nodes.AsParallel()).Where<TripleTreeNode>((Func<TripleTreeNode, bool>) (sn => sn.CheckState != CheckState.Unchecked)));
              if (filterString.Length > 0)
                stringBuilder.Append(filterString + str);
            }
          }
        }
        else if (this.DataType == typeof (bool))
        {
          using (IEnumerator<TripleTreeNode> enumerator = nodes.GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              TripleTreeNode current = enumerator.Current;
              stringBuilder.Append(current.Value.ToString());
            }
          }
        }
        else if (this.DataType == typeof (int) || this.DataType == typeof (long) || (this.DataType == typeof (short) || this.DataType == typeof (uint)) || (this.DataType == typeof (ulong) || this.DataType == typeof (ushort) || (this.DataType == typeof (byte) || this.DataType == typeof (sbyte))))
        {
          foreach (TripleTreeNode node in nodes)
            stringBuilder.Append(node.Value.ToString() + str);
        }
        else if (this.DataType == typeof (float) || this.DataType == typeof (double) || this.DataType == typeof (Decimal))
        {
          foreach (TripleTreeNode node in nodes)
            stringBuilder.Append(node.Value.ToString().Replace(",", ".") + str);
        }
        else
        {
          foreach (TripleTreeNode node in nodes)
            stringBuilder.Append("'" + this.FormatString(node.Value.ToString()) + "'" + str);
        }
      }
      if (stringBuilder.Length > str.Length && this.DataType != typeof (bool))
        stringBuilder.Remove(stringBuilder.Length - str.Length, str.Length);
      return stringBuilder.ToString();
    }

    private string FormatString(string Text)
    {
      string str1 = "";
      string[] strArray = new string[7]
      {
        "%",
        "[",
        "]",
        "*",
        "\"",
        "`",
        "\\"
      };
      for (int index = 0; index < Text.Length; ++index)
      {
        string str2 = Text[index].ToString();
        str1 = !((IEnumerable<string>) strArray).Contains<string>(str2) ? str1 + str2 : str1 + "[" + str2 + "]";
      }
      return str1.Replace("'", "''");
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      TripleTreeNode allsNode = this.GetAllsNode();
      this.FiltersMenuItem.Checked = false;
      if (allsNode != null && allsNode.Checked)
      {
        this.CancelFilterMenuItem_Click((object) null, new EventArgs());
      }
      else
      {
        string filterString1 = this.FilterString;
        this.FilterString = "";
        this.activeFilterType = ADGVFilterMenuFilterType.CheckList;
        if (this.CheckList.Nodes.Count > 1)
        {
          TripleTreeNode nullNode = this.GetNullNode();
          if (nullNode != null && nullNode.Checked)
            this.FilterString = "[{0}] IS NULL";
          if (this.CheckList.Nodes.Count > 2 || nullNode == null)
          {
            string filterString2 = this.CreateFilterString((IEnumerable<TripleTreeNode>) ParallelEnumerable.Cast<TripleTreeNode>(this.CheckList.Nodes.AsParallel()).Where<TripleTreeNode>((Func<TripleTreeNode, bool>) (n =>
            {
              if (n.NodeType != TripleTreeNodeType.AllsNode && n.NodeType != TripleTreeNodeType.EmptysNode)
                return n.CheckState != CheckState.Unchecked;
              return false;
            })));
            if (filterString2.Length > 0)
            {
              if (this.FilterString.Length > 0)
                this.FilterString += " OR ";
              if (this.DataType == typeof (DateTime))
              {
                if (!this.TimeFilter && this.DateWithTime)
                {
                  this.FilterString += filterString2;
                }
                else
                {
                  ADGVFilterMenu adgvFilterMenu = this;
                  string str = adgvFilterMenu.FilterString + "[{0}] IN (" + filterString2 + ")";
                  adgvFilterMenu.FilterString = str;
                }
              }
              else if (this.DataType == typeof (bool))
              {
                ADGVFilterMenu adgvFilterMenu = this;
                string str = adgvFilterMenu.FilterString + "{0}=" + filterString2;
                adgvFilterMenu.FilterString = str;
              }
              else if (this.DataType == typeof (int) || this.DataType == typeof (long) || (this.DataType == typeof (short) || this.DataType == typeof (uint)) || (this.DataType == typeof (ulong) || this.DataType == typeof (ushort) || (this.DataType == typeof (byte) || this.DataType == typeof (sbyte))) || this.DataType == typeof (string))
              {
                ADGVFilterMenu adgvFilterMenu = this;
                string str = adgvFilterMenu.FilterString + "[{0}] IN (" + filterString2 + ")";
                adgvFilterMenu.FilterString = str;
              }
              else
              {
                ADGVFilterMenu adgvFilterMenu = this;
                string str = adgvFilterMenu.FilterString + "Convert([{0}],System.String) IN (" + filterString2 + ")";
                adgvFilterMenu.FilterString = str;
              }
            }
          }
        }
        this.DuplicateFilterNodes();
        if (filterString1 != this.FilterString && this.FilterChanged != null)
          this.FilterChanged((object) this, new EventArgs());
      }
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.RestoreNodes();
      this.Close();
    }

    private void SortASCMenuItem_Click(object sender, EventArgs e)
    {
      this.SortASCMenuItem.Checked = true;
      this.SortDESCMenuItem.Checked = false;
      this.activeSortType = ADGVFilterMenuSortType.ASC;
      string sortString = this.SortString;
      this.SortString = "[{0}] ASC";
      if (!(sortString != this.SortString) || this.SortChanged == null)
        return;
      this.SortChanged((object) this, new EventArgs());
    }

    private void SortDESCMenuItem_Click(object sender, EventArgs e)
    {
      this.SortASCMenuItem.Checked = false;
      this.SortDESCMenuItem.Checked = true;
      this.activeSortType = ADGVFilterMenuSortType.DESC;
      string sortString = this.SortString;
      this.SortString = "[{0}] DESC";
      if (!(sortString != this.SortString) || this.SortChanged == null)
        return;
      this.SortChanged((object) this, new EventArgs());
    }

    private void CancelSortMenuItem_Click(object sender, EventArgs e)
    {
      string sortString = this.SortString;
      this.ClearSorting();
      if (!(sortString != this.SortString) || this.SortChanged == null)
        return;
      this.SortChanged((object) this, new EventArgs());
    }

    public void ClearSorting()
    {
      string sortString = this.SortString;
      this.SortASCMenuItem.Checked = false;
      this.SortDESCMenuItem.Checked = false;
      this.activeSortType = ADGVFilterMenuSortType.None;
      this.SortString = (string) null;
    }

    private void SetupFilterMenuItem_Click(object sender, EventArgs e)
    {
      SetFilterForm setFilterForm = new SetFilterForm(this.DataType, this.DateWithTime, this.TimeFilter);
      if (setFilterForm.ShowDialog() != DialogResult.OK)
        return;
      this.AddCustomFilter(setFilterForm.FilterString, setFilterForm.ViewFilterString);
    }

    private void AddCustomFilter(string FilterString, string ViewFilterString)
    {
      int FiltersMenuItemIndex = -1;
      for (int index = 2; index < this.FiltersMenuItem.DropDownItems.Count && this.FiltersMenuItem.DropDown.Items[index].Available; ++index)
      {
        if (this.FiltersMenuItem.DropDownItems[index].Text == ViewFilterString && this.FiltersMenuItem.DropDownItems[index].Tag.ToString() == FilterString)
        {
          FiltersMenuItemIndex = index;
          break;
        }
      }
      if (FiltersMenuItemIndex < 2)
      {
        for (int index = this.FiltersMenuItem.DropDownItems.Count - 2; index > 1; --index)
        {
          if (this.FiltersMenuItem.DropDownItems[index].Available)
          {
            this.FiltersMenuItem.DropDownItems[index + 1].Text = this.FiltersMenuItem.DropDownItems[index].Text;
            this.FiltersMenuItem.DropDownItems[index + 1].Tag = this.FiltersMenuItem.DropDownItems[index].Tag;
          }
        }
        FiltersMenuItemIndex = 2;
        this.FiltersMenuItem.DropDownItems[2].Text = ViewFilterString;
        this.FiltersMenuItem.DropDownItems[2].Tag = (object) FilterString;
      }
      this.SetCustomFilter(FiltersMenuItemIndex);
    }

    private void SetCustomFilter(int FiltersMenuItemIndex)
    {
      if (this.activeFilterType == ADGVFilterMenuFilterType.CheckList)
        this.SetNodesCheckedState(this.CheckList.Nodes, false);
      string str = this.FiltersMenuItem.DropDownItems[FiltersMenuItemIndex].Tag.ToString();
      string text = this.FiltersMenuItem.DropDownItems[FiltersMenuItemIndex].Text;
      if (FiltersMenuItemIndex != 2)
      {
        for (int index = FiltersMenuItemIndex; index > 2; --index)
        {
          this.FiltersMenuItem.DropDownItems[index].Text = this.FiltersMenuItem.DropDownItems[index - 1].Text;
          this.FiltersMenuItem.DropDownItems[index].Tag = this.FiltersMenuItem.DropDownItems[index - 1].Tag;
        }
        this.FiltersMenuItem.DropDownItems[2].Text = text;
        this.FiltersMenuItem.DropDownItems[2].Tag = (object) str;
      }
      for (int index = 3; index < this.FiltersMenuItem.DropDownItems.Count; ++index)
        (this.FiltersMenuItem.DropDownItems[index] as ToolStripMenuItem).Checked = false;
      (this.FiltersMenuItem.DropDownItems[2] as ToolStripMenuItem).Checked = true;
      this.activeFilterType = ADGVFilterMenuFilterType.Custom;
      string filterString = this.FilterString;
      this.FilterString = str;
      this.SetNodesCheckedState(this.CheckList.Nodes, false);
      this.DuplicateFilterNodes();
      this.FiltersMenuItem.Checked = true;
      this.okButton.Enabled = false;
      if (!(filterString != this.FilterString) || this.FilterChanged == null)
        return;
      this.FilterChanged((object) this, new EventArgs());
    }

    private void lastfilter1MenuItem_VisibleChanged(object sender, EventArgs e)
    {
      this.toolStripSeparator2MenuItem.Visible = !this.lastfilter1MenuItem.Visible;
      (sender as ToolStripMenuItem).VisibleChanged -= new EventHandler(this.lastfilter1MenuItem_VisibleChanged);
    }

    private void lastfilter1MenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
      for (int FiltersMenuItemIndex = 2; FiltersMenuItemIndex < this.FiltersMenuItem.DropDownItems.Count; ++FiltersMenuItemIndex)
      {
        if (this.FiltersMenuItem.DropDownItems[FiltersMenuItemIndex].Text == toolStripMenuItem.Text && this.FiltersMenuItem.DropDownItems[FiltersMenuItemIndex].Tag.ToString() == toolStripMenuItem.Tag.ToString())
        {
          this.SetCustomFilter(FiltersMenuItemIndex);
          break;
        }
      }
    }

    private void lastfilter1MenuItem_TextChanged(object sender, EventArgs e)
    {
      (sender as ToolStripMenuItem).Available = true;
      (sender as ToolStripMenuItem).TextChanged -= new EventHandler(this.lastfilter1MenuItem_TextChanged);
    }

    private void CancelFilterMenuItem_Click(object sender, EventArgs e)
    {
      string filterString = this.FilterString;
      this.ClearFilter();
      if (!(filterString != this.FilterString) || this.FilterChanged == null)
        return;
      this.FilterChanged((object) this, new EventArgs());
    }

    public void ClearFilter()
    {
      for (int index = 2; index < this.FiltersMenuItem.DropDownItems.Count - 1; ++index)
        (this.FiltersMenuItem.DropDownItems[index] as ToolStripMenuItem).Checked = false;
      this.activeFilterType = ADGVFilterMenuFilterType.None;
      this.SetNodesCheckedState(this.CheckList.Nodes, true);
      string filterString = this.FilterString;
      this.FilterString = (string) null;
      this.filterNodes = (TripleTreeNode[]) null;
      this.FiltersMenuItem.Checked = false;
      this.okButton.Enabled = true;
    }

    private void SortASCMenuItem_MouseEnter(object sender, EventArgs e)
    {
      if (!(sender as ToolStripMenuItem).Enabled)
        return;
      (sender as ToolStripMenuItem).Select();
    }

    private void FilterContextMenu_Closed(object sender, EventArgs e)
    {
      this.ClearResizeBox();
      this.startingNodes = (TripleTreeNode[]) null;
    }

    private void FilterContextMenu_LostFocus(object sender, EventArgs e)
    {
      if (this.ContainsFocus)
        return;
      this.Close();
    }

    private void CheckList_MouseLeave(object sender, EventArgs e)
    {
      this.Focus();
    }

    private void FiltersMenuItem_Paint(object sender, PaintEventArgs e)
    {
      Rectangle rectangle = new Rectangle(this.FiltersMenuItem.Width - 12, 7, 10, 10);
      ControlPaint.DrawMenuGlyph(e.Graphics, rectangle, MenuGlyph.Arrow, Color.Black, Color.Transparent);
    }

    private void ResizePictureBox_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.ClearResizeBox();
    }

    private void ResizePictureBox_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.Visible || e.Button != MouseButtons.Left)
        return;
      this.PaintResizeBox(e.X, e.Y);
    }

    private void ResizePictureBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.resizeEndPoint.X == -1)
        return;
      this.ClearResizeBox();
      if (!this.Visible || e.Button != MouseButtons.Left)
        return;
      this.ResizeMenu(Math.Max(e.X + this.Width - this.ResizeBoxControlHost.Width, this.MinimumSize.Width), Math.Max(e.Y + this.Height - this.ResizeBoxControlHost.Height, this.MinimumSize.Height));
    }

    private void ResizeMenu(int W, int H)
    {
      this.SortASCMenuItem.Width = W - 1;
      this.SortDESCMenuItem.Width = W - 1;
      this.CancelSortMenuItem.Width = W - 1;
      this.CancelFilterMenuItem.Width = W - 1;
      this.SetupFilterMenuItem.Width = W - 1;
      this.FiltersMenuItem.Width = W - 1;
      this.CheckFilterListControlHost.Size = new Size(W - 35, H - 160);
      this.CheckFilterListPanel.Size = new Size(W - 35, H - 160);
      this.CheckList.Bounds = new Rectangle(4, 4, W - 35 - 8, H - 160 - 8);
      this.CheckFilterListButtonsControlHost.Size = new Size(W - 35, 24);
      this.CheckFilterListButtonsControlHost.Size = new Size(W - 35, 24);
      this.okButton.Location = new Point(W - 35 - 164, 0);
      this.cancelButton.Location = new Point(W - 35 - 79, 0);
      this.ResizeBoxControlHost.Margin = new Padding(W - 46, 0, 0, 0);
      this.Size = new Size(W, H);
    }

    private void ResizeGrip_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.DrawImage((Image) ADGV.Properties.Resources.ResizeGrip, 0, 0);
    }

    private void PaintResizeBox(int X, int Y)
    {
      this.ClearResizeBox();
      X += this.Width - this.ResizeBoxControlHost.Width;
      Y += this.Height - this.ResizeBoxControlHost.Height;
      X = Math.Max(X, this.MinimumSize.Width - 1);
      Y = Math.Max(Y, this.MinimumSize.Height - 1);
      Point screen1 = this.PointToScreen(ADGVFilterMenu.resizeStartPoint);
      Point screen2 = this.PointToScreen(new Point(X, Y));
      ControlPaint.DrawReversibleFrame(new Rectangle()
      {
        X = Math.Min(screen1.X, screen2.X),
        Width = Math.Abs(screen1.X - screen2.X),
        Y = Math.Min(screen1.Y, screen2.Y),
        Height = Math.Abs(screen1.Y - screen2.Y)
      }, Color.Black, FrameStyle.Dashed);
      this.resizeEndPoint.X = screen2.X;
      this.resizeEndPoint.Y = screen2.Y;
    }

    private void ClearResizeBox()
    {
      if (this.resizeEndPoint.X == -1)
        return;
      Point screen = this.PointToScreen(ADGVFilterMenu.resizeStartPoint);
      ControlPaint.DrawReversibleFrame(new Rectangle(screen.X, screen.Y, this.resizeEndPoint.X, this.resizeEndPoint.Y)
      {
        X = Math.Min(screen.X, this.resizeEndPoint.X),
        Width = Math.Abs(screen.X - this.resizeEndPoint.X),
        Y = Math.Min(screen.Y, this.resizeEndPoint.Y),
        Height = Math.Abs(screen.Y - this.resizeEndPoint.Y)
      }, Color.Black, FrameStyle.Dashed);
      this.resizeEndPoint.X = -1;
    }

    public void SetLoadedFilterMode(bool Enabled)
    {
      this.SetupFilterMenuItem.Enabled = !Enabled;
      this.CancelFilterMenuItem.Enabled = Enabled;
      if (Enabled)
      {
        this.activeFilterType = ADGVFilterMenuFilterType.Loaded;
        this.sortString = (string) null;
        this.filterString = (string) null;
        this.filterNodes = (TripleTreeNode[]) null;
        this.FiltersMenuItem.Checked = false;
        for (int index = 2; index < this.FiltersMenuItem.DropDownItems.Count - 1; ++index)
          (this.FiltersMenuItem.DropDownItems[index] as ToolStripMenuItem).Checked = false;
        this.CheckList.Nodes.Clear();
        TripleTreeNode allsNode = TripleTreeNode.CreateAllsNode(this.RM.GetString("tripletreenode_allnode_text") + "            ", CheckState.Checked);
        allsNode.NodeFont = new Font(this.CheckList.Font, FontStyle.Bold);
        allsNode.CheckState = CheckState.Indeterminate;
        this.CheckList.Nodes.Add((TreeNode) allsNode);
      }
      else
        this.activeFilterType = ADGVFilterMenuFilterType.None;
    }
  }
}
