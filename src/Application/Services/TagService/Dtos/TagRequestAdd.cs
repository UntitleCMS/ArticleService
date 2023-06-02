using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.TagService.Dtos
{
    public record TagRequestAdd
        (
            string Name,
            [RegularExpression(@"""([a-f0-9A-F]{8})|([a-f0-9A-F]{6})|([a-f0-9A-F]{3})""", ErrorMessage ="Only RGB or RGBA value")]
            string? ColorRGB
        );
}
