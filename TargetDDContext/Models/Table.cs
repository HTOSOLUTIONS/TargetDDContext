using System;
using System.Collections.Generic;

namespace TargetDDContext.Models;

public partial class Table
{
    public string TableSchema { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public int? RowCount { get; set; }

    public int? ColCount { get; set; }

    public string? SourceTable { get; set; }

    public virtual ICollection<Column> Columns { get; set; } = new List<Column>();

    public virtual ICollection<FamilyPath> ParentPaths { get; set; } = new List<FamilyPath>();

    public virtual ICollection<FamilyPath> ChildPaths { get; set; } = new List<FamilyPath>();


}
