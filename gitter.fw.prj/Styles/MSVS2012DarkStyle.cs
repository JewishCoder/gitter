﻿namespace gitter.Framework
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using gitter.Framework.Controls;

	sealed class MSVS2012DarkStyle : IGitterStyle
	{
		private readonly IGitterStyleColors _colors;
		private readonly IItemBackgroundStyles _itemBackgroundStyles;
		private ToolStripRenderer _toolStriprenderer;

		private sealed class BackgroundWithBorder : IBackgroundStyle
		{
			#region Data

			private readonly Color _backgroundColor;
			private readonly Color _borderColor;

			#endregion

			#region .ctor

			public BackgroundWithBorder(Color backgroundColor, Color borderColor)
			{
				_backgroundColor = backgroundColor;
				_borderColor = borderColor;
			}

			#endregion

			#region Methods

			public void Draw(Graphics graphics, Rectangle rect)
			{
				using(var brush = new SolidBrush(_backgroundColor))
				{
					graphics.FillRectangle(brush, rect);
				}
				using(var pen = new Pen(_borderColor))
				{
					rect.Width -= 1;
					rect.Height -= 1;
					graphics.DrawRectangle(pen, rect);
				}
			}

			#endregion
		}

		private sealed class SolidBackground : IBackgroundStyle
		{
			#region Data

			private readonly Color _backgroundColor;

			#endregion

			#region .ctor

			public SolidBackground(Color backgroundColor)
			{
				_backgroundColor = backgroundColor;
			}

			#endregion

			#region Methods

			public void Draw(Graphics graphics, Rectangle rect)
			{
				using(var brush = new SolidBrush(_backgroundColor))
				{
					graphics.FillRectangle(brush, rect);
				}
			}

			#endregion
		}

		private sealed class MSVS2012DarkItemBackgroundStyles : IItemBackgroundStyles
		{
			#region Data

			private readonly IBackgroundStyle _focused;
			private readonly IBackgroundStyle _selectedFocused;
			private readonly IBackgroundStyle _selected;
			private readonly IBackgroundStyle _selectedNoFocus;
			private readonly IBackgroundStyle _hovered;
			private readonly IBackgroundStyle _hoveredFocused;

			#endregion

			#region .ctor

			public MSVS2012DarkItemBackgroundStyles()
			{
				_focused			= new BackgroundWithBorder(MSVS2012DarkColors.WINDOW, MSVS2012DarkColors.HIGHLIGHT);
				_selectedFocused	= new SolidBackground(MSVS2012DarkColors.HIGHLIGHT);
				_selected			= _selectedFocused;
				_selectedNoFocus	= new SolidBackground(MSVS2012DarkColors.HIDDEN_HIGHLIGHT);
				_hovered			= new SolidBackground(MSVS2012DarkColors.HOT_TRACK);
				_hoveredFocused		= new BackgroundWithBorder(MSVS2012DarkColors.HOT_TRACK, MSVS2012DarkColors.HIGHLIGHT);
			}

			#endregion

			#region Properties

			public IBackgroundStyle Focused
			{
				get { return _focused; }
			}

			public IBackgroundStyle SelectedFocused
			{
				get { return _selectedFocused; }
			}

			public IBackgroundStyle Selected
			{
				get { return _selected; }
			}

			public IBackgroundStyle SelectedNoFocus
			{
				get { return _selectedNoFocus; }
			}

			public IBackgroundStyle Hovered
			{
				get { return _hovered; }
			}

			public IBackgroundStyle HoveredFocused
			{
				get { return _hoveredFocused; }
			}

			#endregion
		}

		public MSVS2012DarkStyle()
		{
			_colors = new MSVS2012DarkColors();
			_itemBackgroundStyles = new MSVS2012DarkItemBackgroundStyles();
		}

		public string Name
		{
			get { return "MSVS2012DarkStyle"; }
		}

		public string DisplayName
		{
			get { return "Microsoft Visual Studio 2012 Dark"; }
		}

		public GitterStyleType Type
		{
			get { return GitterStyleType.DarkBackground; }
		}

		public IGitterStyleColors Colors
		{
			get { return _colors; }
		}

		public IItemBackgroundStyles ItemBackgroundStyles
		{
			get { return _itemBackgroundStyles; }
		}

		public IScrollBarWidget CreateScrollBar(Orientation orientation)
		{
			return new CustomScrollBarAdapter(orientation, CustomScrollBarRenderer.MSVS2012Dark);
		}

		public ICheckBoxWidget CreateCheckBox()
		{
			return new CustomCheckBoxAdapter(CustomCheckBoxRenderer.MSVS2012Dark);
		}

		public CustomListBoxRenderer ListBoxRenderer
		{
			get { return CustomListBoxManager.MSVS2012DarkRenderer; }
		}

		public ProcessOverlayRenderer OverlayRenderer
		{
			get { return ProcessOverlayRenderer.MSVS2012Dark; }
		}

		public ToolStripRenderer ToolStripRenderer
		{
			get
			{
				if(_toolStriprenderer == null)
				{
					_toolStriprenderer = new MSVS2012StyleToolStripRenderer(MSVS2012StyleToolStripRenderer.DarkColors);
				}
				return _toolStriprenderer;
			}
		}

		public ViewRenderer ViewRenderer
		{
			get { return ViewManager.MSVS2012DarkStyleRenderer; }
		}
	}
}
