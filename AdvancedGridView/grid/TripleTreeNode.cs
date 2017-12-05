// Decompiled with JetBrains decompiler
// Type: ADGV.TripleTreeNode
// Assembly: AdvancedDataGridView, Version=0.1.0.10, Culture=neutral, PublicKeyToken=null
// MVID: 7A483508-A150-445B-AC90-28E7B2039647
// Assembly location: E:\prog\cs\Новая папка\AdvancedGridView\packages\ADGV.0.1.0.10\lib\net40\AdvancedDataGridView.dll

using System.Windows.Forms;

namespace ADGV
{
  public class TripleTreeNode : TreeNode
  {
    private CheckState checkState;
    private TripleTreeNode parent;

    public TripleTreeNodeType NodeType { get; private set; }

    public object Value { get; private set; }

    public TripleTreeNode Parent
    {
      get
      {
        if (this.parent != null)
          return this.parent;
        return (TripleTreeNode) null;
      }
      set
      {
        this.parent = value;
      }
    }

    public new bool Checked
    {
      get
      {
        return this.checkState == CheckState.Checked;
      }
      set
      {
        this.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
      }
    }

    public CheckState CheckState
    {
      get
      {
        return this.checkState;
      }
      set
      {
        this.checkState = value;
        this.SetCheckImage();
      }
    }

    private TripleTreeNode(string Text, object Value, CheckState State, TripleTreeNodeType NodeType)
      : base(Text)
    {
      this.CheckState = State;
      this.NodeType = NodeType;
      this.Value = Value;
    }

    public static TripleTreeNode CreateNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.Default);
    }

    public static TripleTreeNode CreateYearNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.YearDateTimeNode);
    }

    public static TripleTreeNode CreateMonthNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.MonthDateTimeNode);
    }

    public static TripleTreeNode CreateDayNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.DayDateTimeNode);
    }

    public static TripleTreeNode CreateHourNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.HourDateTimeNode);
    }

    public static TripleTreeNode CreateMinNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.MinDateTimeNode);
    }

    public static TripleTreeNode CreateSecNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.SecDateTimeNode);
    }

    public static TripleTreeNode CreateMSecNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, Value, State, TripleTreeNodeType.MSecDateTimeNode);
    }

    public static TripleTreeNode CreateEmptysNode(string Text, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, (object) null, State, TripleTreeNodeType.EmptysNode);
    }

    public static TripleTreeNode CreateAllsNode(string Text, CheckState State = CheckState.Checked)
    {
      return new TripleTreeNode(Text, (object) null, State, TripleTreeNodeType.AllsNode);
    }

    public TripleTreeNode CreateChildNode(string Text, object Value, CheckState State = CheckState.Checked)
    {
      TripleTreeNode child;
      switch (this.NodeType)
      {
        case TripleTreeNodeType.SecDateTimeNode:
          child = TripleTreeNode.CreateMSecNode(Text, Value, State);
          break;
        case TripleTreeNodeType.MinDateTimeNode:
          child = TripleTreeNode.CreateSecNode(Text, Value, State);
          break;
        case TripleTreeNodeType.HourDateTimeNode:
          child = TripleTreeNode.CreateMinNode(Text, Value, State);
          break;
        case TripleTreeNodeType.DayDateTimeNode:
          child = TripleTreeNode.CreateHourNode(Text, Value, State);
          break;
        case TripleTreeNodeType.MonthDateTimeNode:
          child = TripleTreeNode.CreateDayNode(Text, Value, State);
          break;
        case TripleTreeNodeType.YearDateTimeNode:
          child = TripleTreeNode.CreateMonthNode(Text, Value, State);
          break;
        default:
          child = (TripleTreeNode) null;
          break;
      }
      if (child != null)
        this.AddChild(child);
      return child;
    }

    public TripleTreeNode CreateChildNode(string Text, object Value)
    {
      return this.CreateChildNode(Text, Value, this.checkState);
    }

    public TripleTreeNode Clone()
    {
      TripleTreeNode tripleTreeNode;
      switch (this.NodeType)
      {
        case TripleTreeNodeType.AllsNode:
          tripleTreeNode = TripleTreeNode.CreateAllsNode(this.Text, this.checkState);
          break;
        case TripleTreeNodeType.EmptysNode:
          tripleTreeNode = TripleTreeNode.CreateEmptysNode(this.Text, this.checkState);
          break;
        case TripleTreeNodeType.MSecDateTimeNode:
          tripleTreeNode = TripleTreeNode.CreateMSecNode(this.Text, this.Value, this.checkState);
          break;
        case TripleTreeNodeType.SecDateTimeNode:
          tripleTreeNode = TripleTreeNode.CreateSecNode(this.Text, this.Value, this.checkState);
          break;
        case TripleTreeNodeType.MinDateTimeNode:
          tripleTreeNode = TripleTreeNode.CreateMinNode(this.Text, this.Value, this.checkState);
          break;
        case TripleTreeNodeType.HourDateTimeNode:
          tripleTreeNode = TripleTreeNode.CreateHourNode(this.Text, this.Value, this.checkState);
          break;
        case TripleTreeNodeType.DayDateTimeNode:
          tripleTreeNode = TripleTreeNode.CreateDayNode(this.Text, this.Value, this.checkState);
          break;
        case TripleTreeNodeType.MonthDateTimeNode:
          tripleTreeNode = TripleTreeNode.CreateMonthNode(this.Text, this.Value, this.checkState);
          break;
        case TripleTreeNodeType.YearDateTimeNode:
          tripleTreeNode = TripleTreeNode.CreateYearNode(this.Text, this.Value, this.checkState);
          break;
        default:
          tripleTreeNode = TripleTreeNode.CreateNode(this.Text, this.Value, this.checkState);
          break;
      }
      tripleTreeNode.NodeFont = this.NodeFont;
      if (this.GetNodeCount(false) > 0)
      {
        foreach (TripleTreeNode node in this.Nodes)
          tripleTreeNode.AddChild(node.Clone());
      }
      return tripleTreeNode;
    }

    private void SetCheckImage()
    {
      switch (this.checkState)
      {
        case CheckState.Checked:
          this.StateImageIndex = 1;
          break;
        case CheckState.Indeterminate:
          this.StateImageIndex = 2;
          break;
        default:
          this.StateImageIndex = 0;
          break;
      }
    }

    private void AddChild(TripleTreeNode child)
    {
      child.Parent = this;
      this.Nodes.Add((TreeNode) child);
    }
  }
}
