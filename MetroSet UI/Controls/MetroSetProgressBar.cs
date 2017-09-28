﻿using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSetProgressBar), "Bitmaps.Progress.bmp")] 
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSetProgressBar : Label, iControl
    {
        #region Interfaces

        /// <summary>
        /// Gets or sets the style associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get
            {
                return StyleManager?.Style ?? style;
            }
            set
            {
                style = value;
                switch (value)
                {
                    case Style.Light:
                        ApplyTheme();
                        break;

                    case Style.Dark:
                        ApplyTheme(Style.Dark);
                        break;

                    case Style.Custom:
                        ApplyTheme(Style.Custom);
                        break;

                    default:
                        ApplyTheme();
                        break;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the Style Manager associated with the control.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
        public StyleManager StyleManager
        {
            get { return _StyleManager; }
            set { _StyleManager = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the The Author name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        /// <summary>
        /// Gets or sets the The Theme name associated with the theme.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        #endregion Interfaces

        #region Global Vars

        private static ProgressBarProperties prop;
        private Methods mth;
        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style style;
        private StyleManager _StyleManager;
        private int _Value;
        private int CurrentValue;

        #endregion Internal Vars

        #region Constructors

        public MetroSetProgressBar()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            UpdateStyles();
            prop = new ProgressBarProperties();
            mth = new Methods();
            utl = new Utilites();
            style = Style.Light;
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        internal void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    prop.Enabled = Enabled;
                    prop.ProgressColor = Color.FromArgb(65, 177, 225);
                    prop.BorderColor = Color.FromArgb(238, 238, 238);
                    prop.BackColor = Color.FromArgb(238, 238, 238);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroLite";
                    SetProperties();
                    break;

                case Style.Dark:
                    prop.Enabled = Enabled;
                    prop.ProgressColor = Color.FromArgb(65, 177, 225);
                    prop.BackColor = Color.FromArgb(38, 38, 38);
                    prop.BorderColor = Color.FromArgb(38, 38, 38);
                    ThemeAuthor = "Narwin";
                    ThemeName = "MetroDark";
                    SetProperties();
                    break;

                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.LabelDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Enabled":
                                    prop.Enabled = Convert.ToBoolean(varkey.Value);
                                    break;

                                case "ProgressColor":
                                    prop.ProgressColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    prop.BorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "BackColor":
                                    prop.BackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBackColor":
                                    prop.DisabledBackColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledBorderColor":
                                    prop.DisabledBorderColor = utl.HexColor((string)varkey.Value);
                                    break;

                                case "DisabledProgressColor":
                                    prop.DisabledProgressColor = utl.HexColor((string)varkey.Value);
                                    break;

                                default:
                                    return;
                            }
                        }
                    SetProperties();
                    break;
            }
        }

        public void SetProperties()
        {
            try
            {
                Enabled = prop.Enabled;
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (SolidBrush BG = new SolidBrush(prop.BackColor))
            {
                using (Pen P = new Pen(prop.BorderColor))
                {
                    using (SolidBrush PS = new SolidBrush(prop.ProgressColor))
                    {
                        G.FillRectangle(BG, Rect);
                        if (CurrentValue != 0)
                        {
                            G.FillRectangle(PS, new Rectangle(0, 0, CurrentValue - 1, Height - 1));
                        }
                        G.DrawRectangle(P, Rect);
                    }
                }
            }
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the current position of the progressbar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the current position of the progressbar.")]
        public int Value
        {
            get
            {
                if (_Value < 0)
                {
                    return 0;
                }
                else
                {
                    return _Value;
                }
            }
            set
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }
                _Value = value;
                RenewCurrentValue();
                ValueChanged?.Invoke(this);
                Invalidate();
            }
        }
        
        /// <summary>
        /// Gets or sets the maximum value of the progressbar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the maximum value of the progressbar.")]
        public int Maximum { get; set; } = 100;
        
        /// <summary>
        /// Gets or sets the minimum value of the progressbar.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the minimum value of the progressbar.")]
        public int Minimum { get; set; } = 0;

        [Browsable(false)]
        public new Color BackColor
        {
            get { return Color.Transparent; }
        }

        #endregion

        #region Events

        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler(object sender);
        public void RenewCurrentValue()
        {
            CurrentValue = (int)Math.Round((double)(Value - Minimum) / (double)(Maximum - Minimum) * (double)(Width - 1));
        }

        #endregion

    }
}