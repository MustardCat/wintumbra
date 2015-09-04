﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antumbra.Glow.Observer.ScreenInfo {
    public interface AntumbraBitmapSource {
        void AttachObserver(AntumbraScreenInfoObserver observer);
    }
}