namespace PBF.WorkNotes.UI.Helpers;

public class ColorToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return Brushes.Black;

        try
        {
            return new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(value.ToString())
            );
        }
        catch
        {
            return Brushes.Black; // Fallback color
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            return brush.Color.ToString();
        }
        return "Black"; // Default color name
    }

    public static Brush StringToBrush(string colorString)
    {
        try
        {
            var converter = new BrushConverter();
            return (Brush)converter.ConvertFromString(colorString);
        }
        catch
        {
            // Return default brush if conversion fails
            return Brushes.Black;
        }
    }

    public static string BrushToString(Brush brush)
    {
        switch (brush)
        {
            case SolidColorBrush scb:
                return scb.Color.ToString();
            case LinearGradientBrush lgb:
                return "LinearGradientBrush"; // Could return gradient stops info
            case RadialGradientBrush rgb:
                return "RadialGradientBrush"; // Could return gradient stops info
            case ImageBrush ib:
                return "ImageBrush"; // Could return image source info
            case DrawingBrush db:
                return "DrawingBrush";
            case VisualBrush vb:
                return "VisualBrush";
            default:
                return brush.ToString();
        }
    }
}