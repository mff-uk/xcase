using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using NUml.Uml2;
using XCase.View.Geometries;
using XCase.View.Interfaces;
using System.Windows;
using System;
using Element = XCase.Model.Element;

namespace XCase.View.Controls
{
	/// <summary>
	/// Possible <see cref="XCaseCanvas.State">states</see> of <see cref="XCaseCanvas"/>
	/// </summary>
	public enum ECanvasState
	{
		/// <summary>
		/// <see cref="XCaseCanvas.NormalState">Normal state</see>
		/// </summary>
		Normal,
		/// <summary>
		/// <see cref="XCaseCanvas.DraggingConnectionState">Dragging connection state</see>
		/// </summary>
		DraggingConnection,
		/// <summary>
		/// <see cref="XCaseCanvas.DraggingElementState">Dragging element state</see>
		/// </summary>
		DraggingElement
	}

	public partial class XCaseCanvas
	{
		/// <summary>
		/// State of <see cref="XCaseCanvas"/>, see inheriting classes for description of 
		/// different <see cref="XCaseCanvas"/> behavior.
		/// </summary>
		/// <seealso cref="NormalState"/>
		/// <seealso cref="DraggingElementState"/>
		/// <seealso cref="DraggingConnectionState"/>
		public abstract class XCaseCanvasState
		{
			/// <summary>
			/// Reference to <see cref="XCaseCanvas"/> of this state
			/// </summary>
			public XCaseCanvas Canvas { get; private set; }

			/// <summary>
			/// Create new instance of XCaseCanvasState
			/// </summary>
			/// <param name="canvas"><see cref="XCaseCanvas"/></param>
			protected XCaseCanvasState(XCaseCanvas canvas)
			{
				Canvas = canvas;
			}

			/// <summary>
			/// Method is called when the state is activeted on the canvas
			/// </summary>
			public virtual void StateActivated() { }

			/// <summary>
			/// Method is called when the state of the canvas changes from this state 
			/// to another state
			/// </summary>
			public virtual void StateLeft() { }

			/// <summary>
			/// Type of this state
			/// </summary>
			public abstract ECanvasState Type { get; }

			/// <summary>
			/// Reaction to the <see cref="UIElement.MouseDown"/> event of <see cref="Canvas"/>
			/// </summary>
			/// <param name="e">event arguments</param>
			public virtual void OnMouseDown(MouseButtonEventArgs e) { }

			/// <summary>
			/// Reaction to the <see cref="UIElement.MouseMove"/> event of <see cref="Canvas"/>
			/// </summary>
			/// <param name="e">event arguments</param>
			public virtual void OnMouseMove(MouseEventArgs e) { }

			/// <summary>
			/// Operation performed when <see cref="ISelectable">selectable item's</see>
			/// <see cref="UIElement.PreviewMouseDownEvent"/> occurs.
			/// </summary>
			/// <param name="item">clicked item</param>
			/// <param name="e">event arguments</param>
			public virtual void SelectableItemPreviewMouseDown(ISelectable item, MouseButtonEventArgs e) { }

			/// <summary>
			/// Reaction to the <see cref="UIElement.MouseUp"/> event of <see cref="Canvas"/>
			/// </summary>
			/// <param name="e">event arguments</param>
			public virtual void OnMouseUp(MouseButtonEventArgs e) { }
		}

		/// <summary>
		/// Normal state of the canvas, allows dragging elements on the cnavas
		/// and selecting elements via mouse.
		/// </summary>
		public class NormalState : XCaseCanvasState
		{
			/// <summary>
			/// Creates new instance of <see cref="NormalState"/>
			/// </summary>
			/// <param name="canvas">canvas</param>
			public NormalState(XCaseCanvas canvas)
				: base(canvas)
			{
			}

			/// <summary>
			/// Returns <see cref="ECanvasState.Normal"/>
			/// </summary>
			public override ECanvasState Type
			{
				get
				{
					return ECanvasState.Normal;
				}
			}

			private Point? dragStartPoint;


			/// <summary>
			/// Reaction to the <see cref="UIElement.MouseDown"/> event of <see cref="XCaseCanvasState.Canvas"/>.
			/// OnMouseDown creates <see cref="RubberbandAdorner"/> - a selecting rectangle, which can be 
			/// resized via mouse drag and selects touched elements when mouse button is realeased
			/// </summary>
			/// <param name="e">event arguments</param>
			public override void OnMouseDown(MouseButtonEventArgs e)
			{
				base.OnMouseDown(e);

				if (e.Source == Canvas && e.ChangedButton == MouseButton.Left)
				{
					this.dragStartPoint = e.GetPosition(Canvas);
					// empty selection
					Canvas.SelectedItems.SetSelection();
					e.Handled = true;
				}
				else
				{
					this.dragStartPoint = null;
				}
			}

			/// <summary>
			/// Resizes the <see cref="RubberbandAdorner"/> created in <see cref="OnMouseDown"/>.
			/// </summary>
			/// <param name="e">event arguments</param>
			public override void OnMouseMove(MouseEventArgs e)
			{
				base.OnMouseMove(e);

				if (e.LeftButton != MouseButtonState.Pressed)
					this.dragStartPoint = null;

				if (this.dragStartPoint.HasValue)
				{
					AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(Canvas);
					if (adornerLayer != null)
					{
						RubberbandAdorner adorner = new RubberbandAdorner(Canvas, dragStartPoint);
						adornerLayer.Add(adorner);
					}
				}
			}

			/// <summary>
			/// Selects or deselects the <paramref name="item"/> (depends on whether the item was already 
			/// selected and whether Shift or Control keys are pressed).
			/// </summary>
			/// <param name="item">clicked item</param>
			/// <param name="e">event arguments</param>
			public override void SelectableItemPreviewMouseDown(ISelectable item, MouseButtonEventArgs e)
			{
				base.SelectableItemPreviewMouseDown(item, e);

				//Selection handling
				if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
				{
					if (item.IsSelected)
					{
						item = SelectedItemsCollection.GetOwner(item);
						item.IsSelected = false;
						Canvas.SelectedItems.Remove(item);

					}
					else
					{
						item.IsSelected = true;
						Canvas.SelectedItems.Add(item);
					}
				}
				else if (!item.IsSelected)
				{
					Canvas.SelectedItems.SetSelection(item);
					//foreach (ISelectable i in Canvas.SelectedItems)
					//    i.IsSelected = false;

					//Canvas.SelectedItems.Clear();
					//item.IsSelected = true;
					//Canvas.SelectedItems.Add(item);
				}
			}
		}

		/// <summary>
		/// In this state a connection of a certain <see cref="EDraggedConnectionType">type</see>
		/// can be dragged between <see cref="IConnectable">connectable</see> items.
		/// </summary>
		public class DraggingConnectionState : XCaseCanvasState
		{
			/// <summary>
			/// Type of the dragged connection
			/// </summary>
			public enum EDraggedConnectionType
			{
				Association,
				NavigableAssociation,
				Generalization,
				Composition,
				Aggregation
			}

			/// <summary>
			/// Creates new instance of <see cref="DraggingConnectionState"/>
			/// </summary>
			/// <param name="canvas">canvas</param>
			public DraggingConnectionState(XCaseCanvas canvas)
				: base(canvas)
			{
			}

			/// <summary>
			/// Returns <see cref="ECanvasState.DraggingConnection"/>
			/// </summary>
			public override ECanvasState Type
			{
				get
				{
					return ECanvasState.DraggingConnection;
				}
			}

			/// <summary>
			/// Flag whether an association is currenctly dragged
			/// </summary>
			private bool InConnectionDrag { get; set; }

			/// <summary>
			/// Currently dragged connection
			/// </summary>
			private XCaseJunction draggedConnection;

			/// <summary>
			/// JunctionPoint used as an end of <see cref="draggedConnection"/>
			/// </summary>
			private JunctionPoint draggedPoint;

			/// <summary>
			/// Item under mouse cursor
			/// </summary>
			private IConnectable ItemDraggedOver;

			/// <summary>
			/// Reference to an object that should handle the results when connection is dragged
			/// </summary>
			public IDraggedConnectionProcessor DraggedConnectionProcessor { get; set; }

			/// <summary>
			/// If the mouse cursor is over a <see cref="IConnectable">connectable item</see> a connection is 
			/// drawng between the item and the item where drag started. Otherwise a connection is drawn 
			/// between the item where drag started and current mouse position on the canvas
			/// </summary>
			/// <param name="e"></param>
			public override void OnMouseMove(MouseEventArgs e)
			{
				base.OnMouseMove(e);

				//if (InConnectionDrag)
				{
					Point p = e.GetPosition(Canvas);
					if (InConnectionDrag)
						draggedPoint.SetPreferedPosition(p.X - draggedPoint.Width / 2, p.Y - draggedPoint.Height / 2);

					foreach (KeyValuePair<Rect, IConnectable> pair in selectableItemsBounds)
					{
						if (pair.Key.Contains(p))
						{
							if (InConnectionDrag)
								ConnectableItemMouseEnter(pair.Value);
							else
								Mouse.SetCursor(Cursors.Hand);
							return;
						}
					}
					// not over any element
					if (InConnectionDrag && ItemDraggedOver != null)
					{
						if (InConnectionDrag)
							ConnectableItemMouseLeave(ItemDraggedOver);
						else
							Mouse.SetCursor(Cursors.Arrow);
					}
				}
			}

			/// <summary>
			/// If mouse is over a connectable item, <see cref="DraggedConnectionProcessor"/>'s 
			/// <see cref="IDraggedConnectionProcessor.DragConnectionCompleted"/> handler is called 
			/// to finilize the operation. Otherwise nothing happens.
			/// </summary>
			/// <param name="e"></param>
			public override void OnMouseUp(MouseButtonEventArgs e)
			{
				if (e.ChangedButton == MouseButton.Right)
				{
					Canvas.State = ECanvasState.Normal;
					e.Handled = true;
					return;
				}
				base.OnMouseUp(e);
                
				Canvas.ReleaseMouseCapture();
				InConnectionDrag = false;
				Mouse.SetCursor(Cursors.Arrow);

				if (draggedConnection != null)
				{
					if (draggedConnection.SourceElement != null)
						((IConnectable)draggedConnection.SourceElement).UnHighlight();
					if (draggedConnection.TargetElement != null)
						((IConnectable)draggedConnection.TargetElement).UnHighlight();

					draggedConnection.DeleteFromCanvas();

					if (DraggedConnectionProcessor != null)
					{
						Element sourceElement = null;
						Element targetElement = null;

						if (draggedConnection.SourceElement is XCaseViewBase)
						{
							sourceElement = ((XCaseViewBase)draggedConnection.SourceElement).ModelElement;
						}
						if (draggedConnection.TargetElement is XCaseViewBase)
						{
							targetElement = ((XCaseViewBase)draggedConnection.TargetElement).ModelElement;
						}
						if (draggedConnection.SourceElement != null && ItemDraggedOver == draggedConnection.SourceElement)
						{
							targetElement = sourceElement;
						}
						if (draggedConnection.SourceElement is PIM_Class && draggedConnection.TargetElement is AssociationDiamond)
						{
							targetElement = ((PIM_Class)draggedConnection.SourceElement).ModelElement;
							sourceElement = ((AssociationDiamond) draggedConnection.TargetElement).Association.Association;
						}
						if (draggedConnection.SourceElement is AssociationDiamond && draggedConnection.TargetElement is PIM_Class)
						{
							targetElement = ((PIM_Class)draggedConnection.TargetElement).ModelElement;
							sourceElement = ((AssociationDiamond)draggedConnection.SourceElement).Association.Association;
						}
						DraggedConnectionProcessor.DragConnectionCompleted(sourceElement, targetElement);
						ItemDraggedOver = (IConnectable)draggedConnection.TargetElement; 
					}
				}

				draggedConnection = null;
				Canvas.Children.Remove(draggedPoint);
				draggedPoint = null;
				
			}

			private IDictionary<Rect, IConnectable> selectableItemsBounds = new Dictionary<Rect, IConnectable>();

			/// <summary>
			/// Dragging beggins, the <paramref name="item"/> will be the source of the connection.
			/// </summary>
			/// <param name="item">item clicked</param>
			/// <param name="e">event arguments</param>
			public override void SelectableItemPreviewMouseDown(ISelectable item, MouseButtonEventArgs e)
			{
				if (e.ChangedButton == MouseButton.Right)
				{
					Canvas.State = ECanvasState.Normal;
					e.Handled = true;
					return;
				}
				base.SelectableItemPreviewMouseDown(item, e);
                if (item is IConnectable && e.ChangedButton == MouseButton.Left)
                {
                    Canvas.CaptureMouse();
				    
					foreach (ISelectable i in Canvas.SelectedItems)
						i.IsSelected = false;

					Canvas.SelectedItems.Clear();

					draggedPoint = new JunctionPoint(Canvas) { Width = 0, Height = 0 };
					Canvas.Children.Add(draggedPoint);
					draggedPoint.SetPreferedPosition(e.GetPosition(Canvas));
					SetZIndex(draggedPoint, -5);
					draggedPoint.Visibility = Visibility.Visible;
					//throw new NotImplementedException("Method or operation is not implemented.");
					draggedConnection = new XCaseJunction(Canvas);
					draggedPoint.Junction = draggedConnection;
					(item as IConnectable).Highlight();
					InConnectionDrag = true;
					draggedConnection.DragConnection(item as IConnectable, draggedPoint, Canvas);
					Canvas.Children.Add(draggedConnection);
					draggedConnection.EndCapStyle = JunctionGeometryHelper.GetCap(DraggedConnectionType);
					e.Handled = true;
					draggedConnection.InvalidateGeometry();
                	ConnectableItemMouseEnter((IConnectable)item);
				}
				
			}

			/// <summary>
			/// Returns <see cref="AggregationKind"/> for <see cref="EDraggedConnectionType"/>.
			/// </summary>
			/// <param name="connectionType">type of the connection</param>
			/// <returns>proper <see cref="AggregationKind"/></returns>
			public static AggregationKind GetAggregationType(EDraggedConnectionType connectionType)
			{
				switch (connectionType)
				{
					case EDraggedConnectionType.Association:
						return AggregationKind.none;
					case EDraggedConnectionType.Composition:
						return AggregationKind.composite;
					case EDraggedConnectionType.Aggregation:
						return AggregationKind.shared;
					case EDraggedConnectionType.NavigableAssociation:
						return AggregationKind.none;
					default:
						throw new InvalidOperationException("Cannot return AggregationType for EDraggedConnectionType.Generalization. ");
				}
			}

			private EDraggedConnectionType draggedConnectionType;

			/// <summary>
			/// Type of the dragged connection
			/// </summary>
			public EDraggedConnectionType DraggedConnectionType
			{
				get
				{
					return draggedConnectionType;
				}
				set
				{
					draggedConnectionType = value;
					StateActivated();
				}
			}

			/// <summary>
			/// Untoggles all buttons in <see cref="ToggleButtonsGroup"/>
			/// </summary>
			public override void StateLeft()
			{
				base.StateLeft();
				foreach (IDraggedConnectionProcessor processor in ToggleButtonsGroup)
				{
					processor.StateLeft();
				}
				Mouse.SetCursor(Cursors.Arrow);
			}

			/// <summary>
			/// Changes cursor to Hand
			/// </summary>
			public override void StateActivated()
			{
				base.StateActivated();
				IEnumerable<IConnectable> selectableItems = Canvas.Children.OfType<IConnectable>();
				selectableItemsBounds.Clear();
				foreach (IConnectable connectable in selectableItems)
				{
					selectableItemsBounds[connectable.GetBounds()] = connectable;
				}
				//Canvas.Cursor = Cursors.Hand;
				//Mouse.SetCursor(Cursors.Hand)
				if (ToggleButtonsGroup != null)
				{
					foreach (IDraggedConnectionProcessor processor in ToggleButtonsGroup)
					{
						processor.StateActivated(DraggedConnectionType);
					}
				}
			}
		

			/// <summary>
			/// Buttons assigned to this propperty are untoggled when the DraggingConnectionState state
			/// is left.
			/// </summary>
			public IEnumerable<IDraggedConnectionProcessor> ToggleButtonsGroup { get; set; }

			/// <summary>
			/// End of the connection is set to <paramref name="connectable"/>
			/// </summary>
			/// <param name="connectable">connectable item</param>
			public void ConnectableItemMouseEnter(IConnectable connectable)
			{
				Mouse.SetCursor(Cursors.Hand);
				if (ItemDraggedOver != connectable && InConnectionDrag && draggedConnection != null)
				{
					ItemDraggedOver = connectable;
					draggedConnection.ReconnectTargetElement(connectable);
				}
				else
				{
					if (ItemDraggedOver != connectable)
					{
					}
				}
			}

			/// <summary>
			/// End of the connnection is released from <paramref name="connectable"/> and set to
			/// the current mouse position over the canvas. 
			/// </summary>
			/// <param name="connectable"></param>
			public void ConnectableItemMouseLeave(IConnectable connectable)
			{
				Mouse.SetCursor(Cursors.Arrow);
				ItemDraggedOver = null;

				if (InConnectionDrag && draggedPoint != null /*&& connectable != draggedConnection.SourceElement*/)
				{
					draggedConnection.ReconnectTargetElement(draggedPoint);
				}
			}
		}

		/// <summary>
		/// In this state an element is being dragged over the canvas. 
		/// It is set when a new element is being added into the diagram. 
		/// <see cref="DragData"/> contain parameters of the process and callback handlers
		/// that are callled when the operation is finished.
		/// </summary>
		public class DraggingElementState : XCaseCanvasState
		{
			/// <summary>
			/// Creates new isntance of <see cref="DraggingElementState"/>
			/// </summary>
			/// <param name="canvas">canvas</param>
			public DraggingElementState(XCaseCanvas canvas)
				: base(canvas)
			{
			}

			/// <summary>
			/// Returns <see cref="ECanvasState.DraggingElement"/>
			/// </summary>
			public override ECanvasState Type
			{
				get
				{
					return ECanvasState.DraggingElement;
				}
			}

			/// <summary>
			/// Data for the currently dragged item
			/// </summary>
			public DragButtonData DragData { get; set; }

			/// <summary>
			/// Dragged element is moved over the canvas
			/// </summary>
			/// <param name="e">event arguments</param>
			public override void OnMouseMove(MouseEventArgs e)
			{
				base.OnMouseMove(e);

				if (DragData.DraggedObject.Visibility != Visibility.Visible)
				{
					//Canvas.Children.Add(DragData.DraggedObject);
					DragData.DraggedObject.BringIntoView();
					DragData.DraggedObject.Visibility = Visibility.Visible;
				}

				SetLeft(DragData.DraggedObject, e.GetPosition(Canvas).X);
				SetTop(DragData.DraggedObject, e.GetPosition(Canvas).Y);
			}

			/// <summary>
			/// Process is finished, <see cref="DragButtonData.OnDragCompleted"/> handler is called 
			/// on <see cref="DragData"/>
			/// </summary>
			/// <param name="e">event arguments</param>
			public override void OnMouseUp(MouseButtonEventArgs e)
			{
				base.OnMouseUp(e);

				DragData.OnDragCompleted(e);
				Canvas.ReleaseMouseCapture();
			}
		}
	}
}