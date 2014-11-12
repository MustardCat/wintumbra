﻿using System.Drawing;

namespace Antumbra.Glow
{
  // Cyotek Color Picker controls library
  // Copyright © 2013-2014 Cyotek.
  // http://cyotek.com/blog/tag/colorpicker

  // Licensed under the MIT License. See colorpicker-license.txt for the full text.

  // If you use this code in your applications, donations or attribution are welcome

  public class ColorHitTestInfo
  {
    #region Public Properties

    public Color Color { get; set; }

    public int Index { get; set; }

    public ColorSource Source { get; set; }

    #endregion
  }
}