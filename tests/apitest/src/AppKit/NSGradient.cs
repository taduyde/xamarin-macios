using System;
using NUnit.Framework;

#if !XAMCORE_2_0
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using nfloat = System.Single;
#else
using AppKit;
using ObjCRuntime;
#endif

namespace Xamarin.Mac.Tests  
{
	[TestFixture]
	public class NSGradientTests
	{
		[Test]
		public void NSGradientConstructorTests ()
		{
			NSColorSpace colorSpace = NSColorSpace.GenericRGBColorSpace;
			NSGradient g = new NSGradient (new[] { NSColor.Black, NSColor.White, NSColor.Black }, new[] { 0f, .5f, 1.0f }, colorSpace);
			Assert.IsNotNull (g);
			Assert.AreEqual (colorSpace, g.ColorSpace);
			Assert.AreEqual (3, g.ColorStopsCount);

			// Since we are asking for colors on a gradient, there will be some color blending, even with just black and white.
			const float closeEnough = .05f;
			NSColor black = NSColor.Black.UsingColorSpace (NSColorSpace.CalibratedRGB);
			NSColor white = NSColor.White.UsingColorSpace (NSColorSpace.CalibratedRGB);

			NSColor color;
			nfloat location;
			
			g.GetColor (out color, out location, 0);
			color = color.UsingColorSpace (NSColorSpace.CalibratedRGB);
			Assert.IsTrue (black.RedComponent - color.RedComponent < closeEnough);
			Assert.IsTrue (black.BlueComponent - color.BlueComponent < closeEnough);
			Assert.IsTrue (black.GreenComponent - color.GreenComponent < closeEnough);
			Assert.AreEqual (0.0f, (float)location);

			g.GetColor (out color, out location, 1);
			color = color.UsingColorSpace (NSColorSpace.CalibratedRGB);
			Assert.IsTrue (white.RedComponent - color.RedComponent < closeEnough);
			Assert.IsTrue (white.BlueComponent - color.BlueComponent < closeEnough);
			Assert.IsTrue (white.GreenComponent - color.GreenComponent < closeEnough);
			Assert.AreEqual (0.5f, (float)location);

			g.GetColor (out color, out location, 2);
			color = color.UsingColorSpace (NSColorSpace.CalibratedRGB);
			Assert.IsTrue (black.RedComponent - color.RedComponent < closeEnough);
			Assert.IsTrue (black.BlueComponent - color.BlueComponent < closeEnough);
			Assert.IsTrue (black.GreenComponent - color.GreenComponent < closeEnough);
			Assert.AreEqual (1.0f, (float)location);
		}
	}
}
