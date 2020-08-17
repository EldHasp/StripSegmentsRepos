using Models;
using System;
using System.Windows.Markup;

namespace StripSegments
{
    public class SegmentDtoExt : MarkupExtension
    {
        public int? Id { get; set; }
        public double Begin { get; set; }
        public double End { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Id == null)
                return new SegmentDto(Begin, End);
            return new SegmentDto(Id.Value, Begin, End);
        }
    }
}
