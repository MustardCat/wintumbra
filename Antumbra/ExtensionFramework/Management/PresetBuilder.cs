﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antumbra.Glow.Settings;
using Antumbra.Glow.ExtensionFramework.Types;

namespace Antumbra.Glow.ExtensionFramework.Management
{
    public class PresetBuilder
    {
        private ExtensionLibrary lib;
        private Guid HSVDriver = Guid.Parse("8360550b-d599-4f0f-8806-bc323f9ce547");
        private Guid SinDriver = Guid.Parse("31cae25b-72c0-4ffc-860b-234fb931bc15");
        private Guid NeonDriver = Guid.Empty;//Guid.Parse("");//TODO
        private Guid ScreenDriverCoupler = Guid.Parse("70987576-1a00-4a34-b787-4c08516cd1b8");
        private Guid ScreenGrabber = Guid.Parse("15115e91-ed5c-49e6-b7a8-4ebbd4dabb2e");
        private Guid SmartProcessor = Guid.Parse("f4f3692d-a405-4dca-82fd-a9f3e9f93afe");
        private Guid FastProcessor = Guid.Parse("07eda8bc-28e6-4d57-a085-7f204785630f");
        private Guid DXGrabber = Guid.Parse("ae53796b-ac50-4cef-a335-2d75dea9f1ea");
        private Guid Saturator = Guid.Parse("2acba4a6-af21-47a9-9551-964a750fea06");
        private Guid Brightener = Guid.Parse("1a271e63-5f7e-43c0-bbb1-7d80d23d8db7");
        public PresetBuilder(ExtensionLibrary lib)
        {
            this.lib = lib;
        }

        public ActiveExtensions GetHSVFadePreset()
        {
            ActiveExtensions result = new ActiveExtensions();
            GlowExtension ext = this.lib.LookupExt(HSVDriver);
            if (ext != null)
                result.ActiveDriver = (GlowDriver)ext;
            return result;
        }

        public ActiveExtensions GetSinFadePreset()
        {
            ActiveExtensions result = new ActiveExtensions();
            GlowExtension ext = this.lib.LookupExt(SinDriver);
            if (ext != null)
                result.ActiveDriver = (GlowDriver)ext;
            return result;
        }

        public ActiveExtensions GetNeonFadePreset()
        {
            ActiveExtensions result = new ActiveExtensions();
            GlowExtension ext = this.lib.LookupExt(NeonDriver);
            if (ext != null)
                result.ActiveDriver = (GlowDriver)ext;
            return result;
        }

        public ActiveExtensions GetMirrorPreset()
        {
            ActiveExtensions result = new ActiveExtensions();
            GlowExtension ext = this.lib.LookupExt(ScreenDriverCoupler);
            if (ext != null)
                result.ActiveDriver = (GlowDriver)ext;
            ext = this.lib.LookupExt(ScreenGrabber);
            if (ext != null)
                result.ActiveGrabber = (GlowScreenGrabber)ext;
            ext = this.lib.LookupExt(SmartProcessor);
            if (ext != null)
                result.ActiveProcessor = (GlowScreenProcessor)ext;
            return result;
        }

        public ActiveExtensions GetAugmentMirrorPreset()
        {
            ActiveExtensions result = new ActiveExtensions();
            GlowExtension ext = this.lib.LookupExt(ScreenDriverCoupler);
            if (ext != null)
                result.ActiveDriver = (GlowDriver)ext;
            ext = this.lib.LookupExt(ScreenGrabber);
            if (ext != null)
                result.ActiveGrabber = (GlowScreenGrabber)ext;
            ext = this.lib.LookupExt(SmartProcessor);
            if (ext != null)
                result.ActiveProcessor = (GlowScreenProcessor)ext;
            return result;
        }

        public ActiveExtensions GetGameMirrorPreset()
        {
            ActiveExtensions result = new ActiveExtensions();
            GlowExtension ext = this.lib.LookupExt(ScreenDriverCoupler);
            if (ext != null)
                result.ActiveDriver = (GlowDriver)ext;
            ext = this.lib.LookupExt(FastProcessor);
            if (ext != null)
                result.ActiveProcessor = (GlowScreenProcessor)ext;
            ext = this.lib.LookupExt(DXGrabber);
            if (ext != null)
                result.ActiveGrabber = (GlowScreenGrabber)ext;
            return result;
        }
    }
}