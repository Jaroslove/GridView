// Decompiled with JetBrains decompiler
// Type: ADGV.SearchToolBar
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\ConsoleApp13\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace ADGV
{
  public class SearchToolBar : ToolStrip
  {
    private DataGridViewColumnCollection columnsList;
    /// <summary>Требуется переменная конструктора.</summary>
    private IContainer components;
    private ToolStripButton closeButton;
    private ToolStripLabel searchLabel;
    private ToolStripComboBox columnComboBox;
    private ToolStripTextBox searchTextBox;
    private ToolStripButton fromBeginButton;
    private ToolStripButton caseSensButton;
    private ToolStripButton searchButton;
    private ToolStripButton wholeWordButton;
    private ToolStripSeparator searchSeparator;

    public event SearchToolBarSearchEventHandler Search;

    public SearchToolBar()
    {
      this.InitializeComponent();
      this.searchTextBox.Text = this.searchTextBox.ToolTipText;
      this.columnComboBox.SelectedIndex = 0;
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
      if (this.searchTextBox.TextLength <= 0 || !(this.searchTextBox.Text != this.searchTextBox.ToolTipText) || this.Search == null)
        return;
      DataGridViewColumn Column = (DataGridViewColumn) null;
      if (this.columnComboBox.SelectedIndex > 0 && this.columnsList != null && this.columnsList.GetColumnCount(DataGridViewElementStates.Visible) > 0)
      {
        DataGridViewColumn[] array = this.columnsList.Cast<DataGridViewColumn>().Where<DataGridViewColumn>((Func<DataGridViewColumn, bool>) (col => col.Visible)).ToArray<DataGridViewColumn>();
        if (array.Length == this.columnComboBox.Items.Count - 1 && array[this.columnComboBox.SelectedIndex - 1].HeaderText == this.columnComboBox.SelectedItem.ToString())
          Column = array[this.columnComboBox.SelectedIndex - 1];
      }
      this.Search((object) this, new SearchToolBarSearchEventArgs(this.searchTextBox.Text, Column, this.caseSensButton.Checked, this.wholeWordButton.Checked, this.fromBeginButton.Checked));
    }

    private void searchTextBox_TextChanged(object sender, EventArgs e)
    {
      this.searchButton.Enabled = this.searchTextBox.TextLength > 0 && this.searchTextBox.Text != this.searchTextBox.ToolTipText;
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void searchTextBox_Enter(object sender, EventArgs e)
    {
      if (this.searchTextBox.Text == this.searchTextBox.ToolTipText && this.searchTextBox.ForeColor == Color.LightGray)
        this.searchTextBox.Text = "";
      else
        this.searchTextBox.SelectAll();
      this.searchTextBox.ForeColor = SystemColors.WindowText;
    }

    private void searchTextBox_Leave(object sender, EventArgs e)
    {
      if (!(this.searchTextBox.Text.Trim() == ""))
        return;
      this.searchTextBox.Text = this.searchTextBox.ToolTipText;
      this.searchTextBox.ForeColor = Color.LightGray;
    }

    public void SetColumns(DataGridViewColumnCollection columns)
    {
      this.columnsList = columns;
      this.columnComboBox.BeginUpdate();
      this.columnComboBox.Items.Clear();
      this.columnComboBox.Items.AddRange(new object[1]
      {
        (object) new ComponentResourceManager(typeof (SearchToolBar)).GetString("columnComboBox.Items")
      });
      if (this.columnsList != null)
      {
        foreach (DataGridViewColumn columns1 in (BaseCollection) this.columnsList)
        {
          if (columns1.Visible)
            this.columnComboBox.Items.Add((object) columns1.HeaderText);
        }
      }
      this.columnComboBox.SelectedIndex = 0;
      this.columnComboBox.EndUpdate();
    }

    private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.searchTextBox.TextLength <= 0 || !(this.searchTextBox.Text != this.searchTextBox.ToolTipText) || e.KeyData != Keys.Return)
        return;
      e.SuppressKeyPress = false;
      e.Handled = true;
      this.searchButton_Click((object) this.searchButton, new EventArgs());
    }

    private void ResizeBoxes(int width, ref int w1, ref int w2)
    {
      int num1 = (int) Math.Round((double) (width - this.Width) / 2.0, 0, MidpointRounding.AwayFromZero);
      int num2 = w1;
      if (this.Width < width)
      {
        w1 = Math.Max(w1 - num1, 75);
        w2 = Math.Max(w2 - num1, 75);
      }
      else
      {
        w1 = Math.Min(w1 - num1, 150);
        w2 += this.Width - width + num2 - w1;
      }
    }

    private void SearchToolBar_Resize(object sender, EventArgs e)
    {
      this.SuspendLayout();
      int w1 = 150;
      int w2 = 150;
      int num1 = this.columnComboBox.Width + this.searchTextBox.Width;
      foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.Items)
      {
        toolStripItem.Overflow = ToolStripItemOverflow.Never;
        toolStripItem.Visible = true;
      }
      int width = this.PreferredSize.Width - num1 + w1 + w2;
      if (this.Width < width)
      {
        this.searchLabel.Visible = false;
        this.ResizeBoxes(this.PreferredSize.Width - num1 + w1 + w2, ref w1, ref w2);
        int num2 = this.PreferredSize.Width - num1 + w1 + w2;
        if (this.Width < num2)
        {
          this.caseSensButton.Overflow = ToolStripItemOverflow.Always;
          this.ResizeBoxes(this.PreferredSize.Width - num1 + w1 + w2, ref w1, ref w2);
          num2 = this.PreferredSize.Width - num1 + w1 + w2;
        }
        if (this.Width < num2)
        {
          this.wholeWordButton.Overflow = ToolStripItemOverflow.Always;
          this.ResizeBoxes(this.PreferredSize.Width - num1 + w1 + w2, ref w1, ref w2);
          num2 = this.PreferredSize.Width - num1 + w1 + w2;
        }
        if (this.Width < num2)
        {
          this.fromBeginButton.Overflow = ToolStripItemOverflow.Always;
          this.searchSeparator.Visible = false;
          this.ResizeBoxes(this.PreferredSize.Width - num1 + w1 + w2, ref w1, ref w2);
          num2 = this.PreferredSize.Width - num1 + w1 + w2;
        }
        if (this.Width < num2)
        {
          this.columnComboBox.Overflow = ToolStripItemOverflow.Always;
          this.searchTextBox.Overflow = ToolStripItemOverflow.Always;
          w1 = 150;
          w2 = Math.Max(this.Width - this.PreferredSize.Width - this.searchTextBox.Margin.Left - this.searchTextBox.Margin.Right, 75);
          this.searchTextBox.Overflow = ToolStripItemOverflow.Never;
          num2 = this.PreferredSize.Width - this.searchTextBox.Width + w2;
        }
        if (this.Width < num2)
        {
          this.searchButton.Overflow = ToolStripItemOverflow.Always;
          w2 = Math.Max(this.Width - this.PreferredSize.Width + this.searchTextBox.Width, 75);
          num2 = this.PreferredSize.Width - this.searchTextBox.Width + w2;
        }
        if (this.Width < num2)
        {
          this.closeButton.Overflow = ToolStripItemOverflow.Always;
          this.searchTextBox.Margin = new Padding(8, 2, 8, 2);
          w2 = Math.Max(this.Width - this.PreferredSize.Width + this.searchTextBox.Width, 75);
          num2 = this.PreferredSize.Width - this.searchTextBox.Width + w2;
        }
        if (this.Width < num2)
        {
          w2 = Math.Max(this.Width - this.PreferredSize.Width + this.searchTextBox.Width, 20);
          num2 = this.PreferredSize.Width - this.searchTextBox.Width + w2;
        }
        if (num2 > this.Width)
        {
          this.searchTextBox.Overflow = ToolStripItemOverflow.Always;
          this.searchTextBox.Margin = new Padding(0, 2, 8, 2);
          w2 = 150;
        }
      }
      else
        this.ResizeBoxes(width, ref w1, ref w2);
      if (this.columnComboBox.Width != w1)
        this.columnComboBox.Width = w1;
      if (this.searchTextBox.Width != w2)
        this.searchTextBox.Width = w2;
      this.ResumeLayout();
    }

    /// <summary>Освободить все используемые ресурсы.</summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    /// <summary>
    /// Обязательный метод для поддержки конструктора - не изменяйте
    /// содержимое данного метода при помощи редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SearchToolBar));
      this.closeButton = new ToolStripButton();
      this.searchLabel = new ToolStripLabel();
      this.columnComboBox = new ToolStripComboBox();
      this.searchTextBox = new ToolStripTextBox();
      this.fromBeginButton = new ToolStripButton();
      this.caseSensButton = new ToolStripButton();
      this.searchButton = new ToolStripButton();
      this.wholeWordButton = new ToolStripButton();
      this.searchSeparator = new ToolStripSeparator();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.closeButton, "closeButton");
      this.closeButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.closeButton.Name = "closeButton";
      this.closeButton.Overflow = ToolStripItemOverflow.Never;
      this.closeButton.Click += new EventHandler(this.closeButton_Click);
      componentResourceManager.ApplyResources((object) this.searchLabel, "searchLabel");
      this.searchLabel.Name = "searchLabel";
      this.columnComboBox.Name = "columnComboBox";
      this.columnComboBox.AutoToolTip = true;
      this.columnComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.columnComboBox.Items.AddRange(new object[1]
      {
        (object) componentResourceManager.GetString("columnComboBox.Items")
      });
      this.columnComboBox.Margin = new Padding(0, 2, 8, 2);
      componentResourceManager.ApplyResources((object) this.columnComboBox, "columnComboBox");
      componentResourceManager.ApplyResources((object) this.searchTextBox, "searchTextBox");
      this.searchTextBox.ForeColor = Color.LightGray;
      this.searchTextBox.Margin = new Padding(0, 2, 8, 2);
      this.searchTextBox.Name = "searchTextBox";
      this.searchTextBox.Overflow = ToolStripItemOverflow.Never;
      this.searchTextBox.Enter += new EventHandler(this.searchTextBox_Enter);
      this.searchTextBox.Leave += new EventHandler(this.searchTextBox_Leave);
      this.searchTextBox.KeyDown += new KeyEventHandler(this.searchTextBox_KeyDown);
      this.searchTextBox.TextChanged += new EventHandler(this.searchTextBox_TextChanged);
      componentResourceManager.ApplyResources((object) this.fromBeginButton, "fromBeginButton");
      this.fromBeginButton.Checked = true;
      this.fromBeginButton.CheckOnClick = true;
      this.fromBeginButton.CheckState = CheckState.Checked;
      this.fromBeginButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.fromBeginButton.Name = "fromBeginButton";
      componentResourceManager.ApplyResources((object) this.caseSensButton, "caseSensButton");
      this.caseSensButton.CheckOnClick = true;
      this.caseSensButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.caseSensButton.Name = "caseSensButton";
      componentResourceManager.ApplyResources((object) this.searchButton, "searchButton");
      this.searchButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.searchButton.Name = "searchButton";
      this.searchButton.Overflow = ToolStripItemOverflow.Never;
      this.searchButton.Click += new EventHandler(this.searchButton_Click);
      componentResourceManager.ApplyResources((object) this.wholeWordButton, "wholeWordButton");
      this.wholeWordButton.CheckOnClick = true;
      this.wholeWordButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.wholeWordButton.Margin = new Padding(1, 1, 1, 2);
      this.wholeWordButton.Name = "wholeWordButton";
      componentResourceManager.ApplyResources((object) this.searchSeparator, "searchSeparator");
      this.searchSeparator.Name = "searchSeparator";
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AllowMerge = false;
      this.GripStyle = ToolStripGripStyle.Hidden;
      this.Items.AddRange(new ToolStripItem[9]
      {
        (ToolStripItem) this.closeButton,
        (ToolStripItem) this.searchLabel,
        (ToolStripItem) this.columnComboBox,
        (ToolStripItem) this.searchTextBox,
        (ToolStripItem) this.fromBeginButton,
        (ToolStripItem) this.wholeWordButton,
        (ToolStripItem) this.caseSensButton,
        (ToolStripItem) this.searchSeparator,
        (ToolStripItem) this.searchButton
      });
      this.MaximumSize = new Size(0, 27);
      this.MinimumSize = new Size(0, 27);
      this.RenderMode = ToolStripRenderMode.Professional;
      this.Resize += new EventHandler(this.SearchToolBar_Resize);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
