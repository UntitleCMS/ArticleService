using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extentions;

public static class GuidExtention
{
    public static string ToBase64Url(this Guid id) =>
        Convert.ToBase64String(id.ToByteArray())
            .Replace("/", "-")
            .Replace("+", "_")
            .Replace("=", "");
}
