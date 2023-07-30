using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity;

public class Tag : BaseEntity<string>
{
    // content
    public string Colour { get; set; } = string.Empty;
    public string UnicodeIcon { get; set; } = string.Empty;
}
