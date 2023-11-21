using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;
using System.Globalization;

namespace StrokeShapeBug
{
	public class StringToStrokeShapeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string stringValue = (string)value;
			if (stringValue == null) {
				return null;
			}

			try {
				string shape = "";
				if (stringValue.IndexOf(" ") != -1) {
					shape = stringValue.Substring(0, stringValue.IndexOf(" "));
				}
				else {
					shape = stringValue;
				}

				if (shape.ToUpper() == "ROUNDRECTANGLE") {

					var rect = new RoundRectangle();

					var dimensions = stringValue.Remove(0, shape.Length); //stringValue.Substring(stringValue.IndexOf(" ") + 1);
					double[] corners = new double[4];
					int count = 0;

					while (dimensions.Contains(",")) {
						corners[count] = double.Parse(dimensions.Substring(0, dimensions.IndexOf(",")));
						count++;
						dimensions = dimensions.Substring(dimensions.IndexOf(",") + 1);
					}

					if (dimensions.Length > 0) {
						corners[count] = double.Parse(dimensions);
						count++;
					}

					if (count == 4) {
						rect.CornerRadius = new CornerRadius(corners[0], corners[1], corners[2], corners[3]);
					}
					else if (count == 1) {
						rect.CornerRadius = new CornerRadius(corners[0]);
					}
					else {
						rect.CornerRadius = new CornerRadius();
					}

					return rect;
				}
				else if (shape.ToUpper() == "ELLIPSE") {

					var ellipse = new Ellipse();

					var dimensions = stringValue.Remove(0, shape.Length);
					double[] radius = new double[2];
					int count = 0;

					while (dimensions.Contains(",")) {
						radius[count] = double.Parse(dimensions.Substring(0, dimensions.IndexOf(",")));
						count++;
						dimensions = dimensions.Substring(dimensions.IndexOf(",") + 1);
					}

					if (dimensions.Length > 0) {
						radius[count] = double.Parse(dimensions);
						count++;
					}

					if (count == 2) {
						ellipse.WidthRequest = radius[0];
						ellipse.HeightRequest = radius[1];
					}
					else if (count == 1) {
						ellipse.WidthRequest = radius[0];
						ellipse.HeightRequest = radius[0];
					}
					else {
						//nothing
					}

					return ellipse;
				}
				else {
					//@TODO PT tutte le altre possibili shape
				}
			}
			catch {
				//Error
				return null;
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? 1 : 0;
		}
	}

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			Stopwatch s = new Stopwatch();
			s.Start();
			VerticalPanel.Clear();

			List<int> list = new List<int>();
			for (int i = 0; i < 100; i++) {
				list.Add(i);
			}
			BindableLayout.SetItemsSource(VerticalPanel, list);

			s.Stop();
			Debug.WriteLine("Button_Clicked(), ElapsedMilliseconds: " + s.ElapsedMilliseconds);
		}
	}

}
