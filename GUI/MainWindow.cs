using System;
using Gdk;
using Gtk;
using MathParser;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.GtkSharp;
using OxyPlot.Series;

public partial class MainWindow : Gtk.Window
{
	private PlotView plotView;
	private MathParser.MathParser mathParser = new MathParser.MathParser();

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
		plotView = new PlotView();
		plotView.Width = 500;
		plotView.Height = 500;
		table1.Add(plotView);
		Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.plotView]));
		w5.LeftAttach = ((uint)(1));
		w5.RightAttach = ((uint)(2));
		w5.YOptions = ((global::Gtk.AttachOptions)(4));

		PlotModel plotModel = new PlotModel();

		plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
		plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
		plotView.Model = plotModel;


		Child.ShowAll();

	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void enter(object sender, EventArgs e)
	{

	}

	protected void enter2(object o, KeyPressEventArgs args)
	{


	}

	protected void acti(object sender, EventArgs ee)
	{
		string debug;
		Func<double, double> batFn1 = (x) => mathParser.Parse("f(" + x.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture) + ")").Evaluate();


		try
		{
			ExpressionTree e = mathParser.Parse(entry2.Text);

			e.Assign();

			debug = e.ToDebug();

			double d = e.Evaluate();

			TextIter mIter = textview6.Buffer.EndIter;
			textview6.Buffer.Insert(ref mIter, debug + "\n");
			textview6.ScrollToIter(textview6.Buffer.EndIter, 0, false, 0, 0);

			mIter = textview6.Buffer.EndIter;
			textview6.Buffer.Insert(ref mIter, d.ToString() + "\n");
			textview6.ScrollToIter(textview6.Buffer.EndIter, 0, false, 0, 0);


			plotView.Model.Series.Clear();
			plotView.Model.Series.Add(new FunctionSeries(batFn1, -20, 20, 0.1));



		}
		catch (Exception fe)
		{
			TextIter mIter = textview6.Buffer.EndIter;
			textview6.Buffer.Insert(ref mIter, "parse error: " + fe.Message + "\n");
			textview6.ScrollToIter(textview6.Buffer.EndIter, 0, false, 0, 0);

		}


		entry2.Text = "";

		try
		{
		}
		catch
		{

		}

	}
}
