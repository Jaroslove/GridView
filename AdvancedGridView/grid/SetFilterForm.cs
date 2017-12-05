// Decompiled with JetBrains decompiler
// Type: ADGV.SetFilterForm
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\AdvancedGridView\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace ADGV
{
  public class SetFilterForm : Form
  {
    private bool dateWithTime = true;
    private SetFilterForm.FilterType filterType;
    private ResourceManager RM;
    private Control val1contol;
    private Control val2contol;
    private bool timeFilter;
    /// <summary>Required designer variable.</summary>
    private IContainer components;
    private Button okButton;
    private Button cancelButton;
    private Label ColumnNameLabel;
    private ComboBox FilterTypeComboBox;
    private Label AndLabel;
    private string filterString;
    private string viewfilterString;
    private ErrorProvider errorProvider;

    public string FilterString
    {
      get
      {
        return this.filterString;
      }
    }

    public string ViewFilterString
    {
      get
      {
        return this.viewfilterString;
      }
    }

    private SetFilterForm()
    {
      this.InitializeComponent();
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

    public SetFilterForm(Type dataType, bool DateWithTime = true, bool TimeFilter = false)
      : this()
    {
      this.filterType = !(dataType == typeof (DateTime)) ? (dataType == typeof (int) || dataType == typeof (long) || (dataType == typeof (short) || dataType == typeof (uint)) || (dataType == typeof (ulong) || dataType == typeof (ushort) || (dataType == typeof (byte) || dataType == typeof (sbyte))) ? SetFilterForm.FilterType.Integer : (dataType == typeof (float) || dataType == typeof (double) || dataType == typeof (Decimal) ? SetFilterForm.FilterType.Float : (!(dataType == typeof (string)) ? SetFilterForm.FilterType.Unknown : SetFilterForm.FilterType.String))) : SetFilterForm.FilterType.DateTime;
      this.dateWithTime = DateWithTime;
      this.timeFilter = TimeFilter;
      switch (this.filterType)
      {
        case SetFilterForm.FilterType.DateTime:
          this.val1contol = (Control) new DateTimePicker();
          this.val2contol = (Control) new DateTimePicker();
          if (this.timeFilter)
          {
            DateTimeFormatInfo dateTimeFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
            (this.val1contol as DateTimePicker).CustomFormat = dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.LongTimePattern;
            (this.val2contol as DateTimePicker).CustomFormat = dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.LongTimePattern;
            (this.val1contol as DateTimePicker).Format = DateTimePickerFormat.Custom;
            (this.val2contol as DateTimePicker).Format = DateTimePickerFormat.Custom;
          }
          else
          {
            (this.val1contol as DateTimePicker).Format = DateTimePickerFormat.Short;
            (this.val2contol as DateTimePicker).Format = DateTimePickerFormat.Short;
          }
          this.FilterTypeComboBox.Items.AddRange((object[]) new string[5]
          {
            this.RM.GetString("setfilterform_filtertypecombobox_equal"),
            this.RM.GetString("setfilterform_filtertypecombobox_notequal"),
            this.RM.GetString("setfilterform_filtertypecombobox_before"),
            this.RM.GetString("setfilterform_filtertypecombobox_after"),
            this.RM.GetString("setfilterform_filtertypecombobox_between")
          });
          break;
        case SetFilterForm.FilterType.Float:
        case SetFilterForm.FilterType.Integer:
          this.val1contol = (Control) new TextBox();
          this.val2contol = (Control) new TextBox();
          this.val1contol.TextChanged += new EventHandler(this.eControlTextChanged);
          this.val2contol.TextChanged += new EventHandler(this.eControlTextChanged);
          this.FilterTypeComboBox.Items.AddRange((object[]) new string[7]
          {
            this.RM.GetString("setfilterform_filtertypecombobox_equal"),
            this.RM.GetString("setfilterform_filtertypecombobox_notequal"),
            this.RM.GetString("setfilterform_filtertypecombobox_larger"),
            this.RM.GetString("setfilterform_filtertypecombobox_lagerequals"),
            this.RM.GetString("setfilterform_filtertypecombobox_less"),
            this.RM.GetString("setfilterform_filtertypecombobox_lessequals"),
            this.RM.GetString("setfilterform_filtertypecombobox_between")
          });
          this.val1contol.Tag = (object) true;
          this.val2contol.Tag = (object) true;
          this.okButton.Enabled = false;
          break;
        default:
          this.val1contol = (Control) new TextBox();
          this.val2contol = (Control) new TextBox();
          this.FilterTypeComboBox.Items.AddRange((object[]) new string[8]
          {
            this.RM.GetString("setfilterform_filtertypecombobox_equal"),
            this.RM.GetString("setfilterform_filtertypecombobox_notequal"),
            this.RM.GetString("setfilterform_filtertypecombobox_begins"),
            this.RM.GetString("setfilterform_filtertypecombobox_nobegins"),
            this.RM.GetString("setfilterform_filtertypecombobox_ends"),
            this.RM.GetString("setfilterform_filtertypecombobox_noends"),
            this.RM.GetString("setfilterform_filtertypecombobox_contain"),
            this.RM.GetString("setfilterform_filtertypecombobox_nocontain")
          });
          break;
      }
      this.FilterTypeComboBox.SelectedIndex = 0;
      this.val1contol.Name = nameof (val1contol);
      this.val1contol.Location = new Point(30, 66);
      this.val1contol.Size = new Size(166, 20);
      this.val1contol.TabIndex = 4;
      this.val1contol.Visible = true;
      this.val1contol.KeyDown += new KeyEventHandler(this.eControlKeyDown);
      this.val2contol.Name = nameof (val2contol);
      this.val2contol.Location = new Point(30, 108);
      this.val2contol.Size = new Size(166, 20);
      this.val2contol.TabIndex = 5;
      this.val2contol.Visible = false;
      this.val2contol.VisibleChanged += new EventHandler(this.val2contol_VisibleChanged);
      this.val2contol.KeyDown += new KeyEventHandler(this.eControlKeyDown);
      this.Controls.Add(this.val1contol);
      this.Controls.Add(this.val2contol);
      this.errorProvider.SetIconAlignment(this.val1contol, ErrorIconAlignment.MiddleRight);
      this.errorProvider.SetIconPadding(this.val1contol, -18);
      this.errorProvider.SetIconAlignment(this.val2contol, ErrorIconAlignment.MiddleRight);
      this.errorProvider.SetIconPadding(this.val2contol, -18);
      this.val1contol.Select();
    }

    private void val2contol_VisibleChanged(object sender, EventArgs e)
    {
      this.AndLabel.Visible = this.val2contol.Visible;
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
      this.viewfilterString = (string) null;
      this.filterString = (string) null;
      this.Close();
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      if (this.HasErrors())
      {
        this.okButton.Enabled = false;
      }
      else
      {
        string str1 = "[{0}] ";
        if (this.filterType == SetFilterForm.FilterType.Unknown)
          str1 = "Convert([{0}],System.String) ";
        this.filterString = str1;
        switch (this.filterType)
        {
          case SetFilterForm.FilterType.DateTime:
            DateTime dateTime1 = ((DateTimePicker) this.val1contol).Value;
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_equal"))
            {
              if (this.dateWithTime)
              {
                if (this.timeFilter)
                {
                  SetFilterForm setFilterForm = this;
                  string str2 = setFilterForm.filterString + "= '" + dateTime1.ToString("o") + "'";
                  setFilterForm.filterString = str2;
                  break;
                }
                this.filterString = "Convert([{0}], System.String) LIKE '" + dateTime1.ToShortDateString() + "%'";
                break;
              }
              SetFilterForm setFilterForm1 = this;
              string str3 = setFilterForm1.filterString + "= '" + dateTime1.ToShortDateString() + "'";
              setFilterForm1.filterString = str3;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_before"))
            {
              if (this.timeFilter)
              {
                SetFilterForm setFilterForm = this;
                string str2 = setFilterForm.filterString + "< '" + dateTime1.ToString("o") + "'";
                setFilterForm.filterString = str2;
                break;
              }
              SetFilterForm setFilterForm1 = this;
              string str3 = setFilterForm1.filterString + "< '" + dateTime1.ToShortDateString() + "'";
              setFilterForm1.filterString = str3;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_after"))
            {
              if (this.timeFilter)
              {
                SetFilterForm setFilterForm = this;
                string str2 = setFilterForm.filterString + "> '" + dateTime1.ToString("o") + "'";
                setFilterForm.filterString = str2;
                break;
              }
              SetFilterForm setFilterForm1 = this;
              string str3 = setFilterForm1.filterString + "> '" + dateTime1.ToShortDateString() + "'";
              setFilterForm1.filterString = str3;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_between"))
            {
              DateTime dateTime2 = ((DateTimePicker) this.val2contol).Value;
              if (dateTime2 < dateTime1)
              {
                DateTime dateTime3 = dateTime1;
                dateTime1 = dateTime2;
                dateTime2 = dateTime3;
              }
              if (this.timeFilter)
              {
                SetFilterForm setFilterForm1 = this;
                string str2 = setFilterForm1.filterString + ">= '" + dateTime1.ToString("o") + "'";
                setFilterForm1.filterString = str2;
                SetFilterForm setFilterForm2 = this;
                string str3 = setFilterForm2.filterString + " AND " + str1 + "<= '" + dateTime2.ToString("o") + "'";
                setFilterForm2.filterString = str3;
                break;
              }
              SetFilterForm setFilterForm3 = this;
              string str4 = setFilterForm3.filterString + ">= '" + dateTime1.ToShortDateString() + "'";
              setFilterForm3.filterString = str4;
              SetFilterForm setFilterForm4 = this;
              string str5 = setFilterForm4.filterString + " AND " + str1 + "<= '" + dateTime2.ToShortDateString() + "'";
              setFilterForm4.filterString = str5;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_notequal"))
            {
              if (this.dateWithTime)
              {
                if (this.timeFilter)
                {
                  SetFilterForm setFilterForm = this;
                  string str2 = setFilterForm.filterString + "<> '" + dateTime1.ToString("o") + "'";
                  setFilterForm.filterString = str2;
                  break;
                }
                this.filterString = "Convert([{0}], System.String) NOT LIKE '" + dateTime1.ToShortDateString() + "%'";
                break;
              }
              SetFilterForm setFilterForm1 = this;
              string str3 = setFilterForm1.filterString + "<> '" + dateTime1.ToShortDateString() + "'";
              setFilterForm1.filterString = str3;
              break;
            }
            break;
          case SetFilterForm.FilterType.Float:
          case SetFilterForm.FilterType.Integer:
            string s1 = this.val1contol.Text;
            if (this.filterType == SetFilterForm.FilterType.Float)
              s1 = s1.Replace(",", ".");
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_equal"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "= " + s1;
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_between"))
            {
              string s2 = this.val2contol.Text;
              if (this.filterType == SetFilterForm.FilterType.Float)
                s2 = s2.Replace(",", ".");
              if (double.Parse(s1) > double.Parse(s2))
              {
                string str2 = s1;
                s1 = s2;
                s2 = str2;
              }
              SetFilterForm setFilterForm = this;
              string str3 = setFilterForm.filterString + ">= " + s1 + " AND " + str1 + "<= " + s2;
              setFilterForm.filterString = str3;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_notequal"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "<> " + s1;
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_larger"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "> " + s1;
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_lagerequals"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + ">= " + s1;
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_less"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "< " + s1;
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_lessequals"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "<= " + s1;
              setFilterForm.filterString = str2;
              break;
            }
            break;
          default:
            string str6 = this.FormatString(this.val1contol.Text);
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_equal"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "LIKE '" + str6 + "'";
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_notequal"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "NOT LIKE '" + str6 + "'";
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_begins"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "LIKE '" + str6 + "%'";
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_ends"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "LIKE '%" + str6 + "'";
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_nobegins"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "NOT LIKE '" + str6 + "%'";
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_noends"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "NOT LIKE '%" + str6 + "'";
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_contain"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "LIKE '%" + str6 + "%'";
              setFilterForm.filterString = str2;
              break;
            }
            if (this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_nocontain"))
            {
              SetFilterForm setFilterForm = this;
              string str2 = setFilterForm.filterString + "NOT LIKE '%" + str6 + "%'";
              setFilterForm.filterString = str2;
              break;
            }
            break;
        }
        if (this.filterString != str1)
        {
          this.viewfilterString = this.RM.GetString("setfilterform_viewfilterstring_mustbe") + " " + this.FilterTypeComboBox.Text + " \"" + this.val1contol.Text + "\"";
          if (this.val2contol.Visible)
          {
            SetFilterForm setFilterForm = this;
            string str2 = setFilterForm.viewfilterString + " " + this.AndLabel.Text + " \"" + this.val2contol.Text + "\"";
            setFilterForm.viewfilterString = str2;
          }
          this.DialogResult = DialogResult.OK;
        }
        else
        {
          this.filterString = (string) null;
          this.viewfilterString = (string) null;
          this.DialogResult = DialogResult.Cancel;
        }
        this.Close();
      }
    }

    private void FilterTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.val2contol.Visible = this.FilterTypeComboBox.Text == this.RM.GetString("setfilterform_filtertypecombobox_between");
      if (string.IsNullOrEmpty(this.val1contol.Text) || !this.val2contol.Visible)
        this.val1contol.Select();
      else if (this.val2contol.Visible)
        this.val2contol.Select();
      this.okButton.Enabled = !this.HasErrors();
    }

    private void eControlTextChanged(object sender, EventArgs e)
    {
      bool flag = false;
      switch (this.filterType)
      {
        case SetFilterForm.FilterType.Float:
          double result1;
          flag = !double.TryParse((sender as TextBox).Text, out result1);
          break;
        case SetFilterForm.FilterType.Integer:
          long result2;
          flag = !long.TryParse((sender as TextBox).Text, out result2);
          break;
      }
      (sender as Control).Tag = (object) (bool) (flag ? 1 : ((sender as TextBox).Text.Length == 0 ? 1 : 0));
      if (flag && (sender as TextBox).Text.Length > 0)
        this.errorProvider.SetError(sender as Control, this.RM.GetString("setfilterform_errorprovider_value"));
      else
        this.errorProvider.SetError(sender as Control, "");
      this.okButton.Enabled = !this.HasErrors();
    }

    private bool HasErrors()
    {
      if (this.val1contol.Visible && this.val1contol.Tag != null && (bool) this.val1contol.Tag)
        return true;
      if (this.val2contol.Visible && this.val2contol.Tag != null)
        return (bool) this.val2contol.Tag;
      return false;
    }

    private void eControlKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != Keys.Return)
        return;
      if (sender == this.val1contol)
      {
        if (this.val2contol.Visible)
          this.val2contol.Focus();
        else
          this.OKButton_Click((object) this.okButton, new EventArgs());
      }
      else
        this.OKButton_Click((object) this.okButton, new EventArgs());
      e.SuppressKeyPress = false;
      e.Handled = true;
    }

    /// <summary>Clean up any resources being used.</summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.RM = new ResourceManager("ADGV.Localization.ADGVStrings", typeof (SetFilterForm).Assembly);
      this.components = (IContainer) new Container();
      this.okButton = new Button();
      this.cancelButton = new Button();
      this.ColumnNameLabel = new Label();
      this.FilterTypeComboBox = new ComboBox();
      this.AndLabel = new Label();
      this.errorProvider = new ErrorProvider(this.components);
      ((ISupportInitialize) this.errorProvider).BeginInit();
      this.SuspendLayout();
      this.okButton.DialogResult = DialogResult.OK;
      this.okButton.Location = new Point(29, 139);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(75, 23);
      this.okButton.TabIndex = 0;
      this.okButton.Text = this.RM.GetString("setfilterform_okbutton_text");
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.OKButton_Click);
      this.cancelButton.DialogResult = DialogResult.Cancel;
      this.cancelButton.Location = new Point(123, 139);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(75, 23);
      this.cancelButton.TabIndex = 1;
      this.cancelButton.Text = this.RM.GetString("setfilterform_cancelbutton_text");
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.CancelButton_Click);
      this.ColumnNameLabel.AutoSize = true;
      this.ColumnNameLabel.Location = new Point(4, 9);
      this.ColumnNameLabel.Name = "ColumnNameLabel";
      this.ColumnNameLabel.Size = new Size(140, 13);
      this.ColumnNameLabel.TabIndex = 2;
      this.ColumnNameLabel.Text = this.RM.GetString("setfilterform_columnnamelabel_text");
      this.FilterTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FilterTypeComboBox.FormattingEnabled = true;
      this.FilterTypeComboBox.Location = new Point(7, 32);
      this.FilterTypeComboBox.Name = "FilterTypeComboBox";
      this.FilterTypeComboBox.Size = new Size(189, 21);
      this.FilterTypeComboBox.TabIndex = 3;
      this.FilterTypeComboBox.SelectedIndexChanged += new EventHandler(this.FilterTypeComboBox_SelectedIndexChanged);
      this.AndLabel.AutoSize = true;
      this.AndLabel.Location = new Point(7, 89);
      this.AndLabel.Name = "AndLabel";
      this.AndLabel.Size = new Size(13, 13);
      this.AndLabel.TabIndex = 6;
      this.AndLabel.Text = this.RM.GetString("setfilterform_andlabel_text");
      this.AndLabel.Visible = false;
      this.errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
      this.errorProvider.ContainerControl = (ContainerControl) this;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(205, 169);
      this.Controls.Add((Control) this.AndLabel);
      this.Controls.Add((Control) this.ColumnNameLabel);
      this.Controls.Add((Control) this.FilterTypeComboBox);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Name = nameof (SetFilterForm);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.CancelButton = (IButtonControl) this.cancelButton;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = this.RM.GetString("setfilterform_text");
      this.TopMost = true;
      ((ISupportInitialize) this.errorProvider).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private enum FilterType
    {
      Unknown,
      DateTime,
      String,
      Float,
      Integer,
    }
  }
}
