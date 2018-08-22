using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace XCase.Model
{
	/// <summary>
	/// Basically the same as <see cref="Point"/> but is a class, not a struct.
	/// Used where reference comparison is needed instead value comparison.
	/// </summary>
	public class rPoint
	{
		public double X;
		public double Y;

		public object tag; 

		public static implicit operator Point(rPoint p)
		{
			return new Point(p.X, p.Y);
		}

		public rPoint(double x, double y)
		{
			X = x;
			Y = y;
		}

		public rPoint(Point point): this(point.X, point.Y)
		{
			
		}

		public void Set(Point point)
		{
			X = point.X;
			Y = point.Y;
		}

	    public bool AlmostEqual(Point point)
	    {
            return System.Math.Abs(X - point.X) < 1 && System.Math.Abs(Y - point.Y) < 1;
	    }

        public bool AlmostEqual(rPoint point)
        {
            return System.Math.Abs(X - point.X) < 1 && System.Math.Abs(Y - point.Y) < 1;
        }
	}

	public class ObservablePointCollection: ObservableCollection<rPoint>
	{
		public class PointCollectionChangedEventArgs: NotifyCollectionChangedEventArgs
		{
			public bool PointPositionsMovedOnly { get; set; }

			public PointCollectionChangedEventArgs(NotifyCollectionChangedAction action)
				: base(action)
			{ 

			}
		}

		/// <summary>
		/// Calls <see cref="Collection{T}.Add"/> for each point in <paramref name="points"/>
		/// </summary>
		/// <param name="points">points to add</param>
		public void AppendRange(IEnumerable<rPoint> points)
		{
			foreach (rPoint point in points)
			{
				Add(point);
			}
		}

		/// <summary>
		/// Calls <see cref="Collection{T}.Add"/> for each point in <paramref name="points"/>
		/// </summary>
		/// <param name="points">points to add</param>
		public void AppendRange(IEnumerable<Point> points)
		{
			foreach (Point point in points)
			{
				Add(new rPoint(point));
			}
		}

		/// <summary>
		/// Similar to <see cref="AppendRange(System.Collections.Generic.IEnumerable{XCase.Model.rPoint})"/> 
		/// but new instances for points are created and added into the collection instead of the 
		/// existing rPoints. 
		/// </summary>
		/// <param name="points"></param>
		public void AppendRangeAsCopy(IEnumerable<rPoint> points)
		{
			foreach (rPoint point in points)
			{
				Add(point);
			}
		}

		public bool PointsInvalid { get; set; }

		public void PointsChanged()
		{
			base.OnCollectionChanged(new PointCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset) { PointPositionsMovedOnly = true });
		}
	}
}