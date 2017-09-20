﻿using System.Drawing;

namespace MetroSet_UI.Property
{
    internal class ButtonProperties
    {
        /// <summary>
        /// Gets or sets the button background color in normal mouse sate.
        /// </summary>
        public Color NormalColor { get; set; }

        /// <summary>
        /// Gets or sets the button border color in normal mouse sate.
        /// </summary>
        public Color NormalBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the button Text color in normal mouse sate.
        /// </summary>
        public Color NormalTextColor { get; set; }

        /// <summary>
        /// Gets or sets the button background color in hover mouse sate.
        /// </summary>
        public Color HoverColor { get; set; }

        /// <summary>
        /// Gets or sets the button border color in hover mouse sate.
        /// </summary>
        public Color HoverBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the button Text color in hover mouse sate.
        /// </summary>
        public Color HoverTextColor { get; set; }

        /// <summary>
        /// Gets or sets the button background color in pushed mouse sate.
        /// </summary>
        public Color PressColor { get; set; }

        /// <summary>
        /// Gets or sets the button border color in pushed mouse sate.
        /// </summary>
        public Color PressBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the button Text color in pushed mouse sate.
        /// </summary>
        public Color PressTextColor { get; set; }

        /// <summary>
        /// Gets or sets whether the control enabled.
        /// </summary>
        public bool Enabled { get; set; }

        ///// <summary>
        ///// Gets or sets the font used by the control.
        ///// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Gets or sets the font size used by the control.
        /// </summary>
        public float FontSize { get; set; }

    }
}