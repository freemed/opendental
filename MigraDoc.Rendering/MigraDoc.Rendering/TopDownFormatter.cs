#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   Klaus Potzesny (mailto:Klaus.Potzesny@pdfsharp.com)
//
// Copyright (c) 2001-2007 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Drawing;

namespace MigraDoc.Rendering
{
  /// <summary>
  /// Formats a series of document elements from top to bottom.
  /// </summary>
  internal class TopDownFormatter
  {
    internal TopDownFormatter(IAreaProvider areaProvider, DocumentRenderer documentRenderer, DocumentElements elements)
    {
      this.documentRenderer = documentRenderer;
      this.areaProvider = areaProvider;
      this.elements = elements;
    }
    IAreaProvider areaProvider;

    private DocumentElements elements;

    /// <summary>
    /// Formats the elements on the areas provided by the area provider.
    /// </summary>
    /// <param name="gfx">The graphics object to render on.</param>
    /// <returns></returns>
    public void FormatOnAreas(XGraphics gfx, bool topLevel)
    {
      this.gfx = gfx;
      XUnit prevBottomMargin = 0;
      XUnit yPos = prevBottomMargin;
      RenderInfo prevRenderInfo = null;
      FormatInfo prevFormatInfo = null;
      ArrayList renderInfos = new ArrayList();
      bool ready = this.elements.Count == 0;
      bool isFirstOnPage = true;
      Area area = this.areaProvider.GetNextArea();
      XUnit maxHeight = area.Height;
      if (ready)
      {
        this.areaProvider.StoreRenderInfos(renderInfos);
        return;
      }
      int idx = 0;
      while (!ready && area != null)
      {
        DocumentObject docObj = this.elements[idx];
        Renderer renderer = Renderer.Create(gfx, this.documentRenderer, docObj, this.areaProvider.AreaFieldInfos);
        renderer.MaxElementHeight = maxHeight;

        if (topLevel && this.documentRenderer.HasPrepareDocumentProgress)
        {
          this.documentRenderer.OnPrepareDocumentProgress(this.documentRenderer.ProgressCompleted + idx + 1,
            this.documentRenderer.ProgressMaximum);
        }

        // "Slightly hacked" for legends: they are rendered as part of the chart.
        // So they are skipped here.
        if (renderer == null)
        {
          ready = idx == this.elements.Count - 1;
          if (ready)
            this.areaProvider.StoreRenderInfos(renderInfos);
          ++idx;
          continue;
        }
        ///////////////////////////////////////////
        if (prevFormatInfo == null)
        {
          LayoutInfo initialLayoutInfo = renderer.InitialLayoutInfo;
          XUnit distance = prevBottomMargin;
          if (initialLayoutInfo.VerticalReference == VerticalReference.PreviousElement)
            distance = Math.Max(initialLayoutInfo.MarginTop, distance);
          area = area.Lower(distance);
        }
        renderer.Format(area, prevFormatInfo);
        this.areaProvider.PositionHorizontally(renderer.RenderInfo.LayoutInfo);
        bool pagebreakBefore = this.areaProvider.IsAreaBreakBefore(renderer.RenderInfo.LayoutInfo) && !isFirstOnPage;
        pagebreakBefore = pagebreakBefore || !isFirstOnPage && IsForcedAreaBreak(idx, renderer, area);

        if (!pagebreakBefore && renderer.RenderInfo.FormatInfo.IsEnding)
        {
          if (PreviousRendererNeedsRemoveEnding(prevRenderInfo, renderer.RenderInfo, area))
          {
            prevRenderInfo.RemoveEnding();
            renderer = Renderer.Create(gfx, this.documentRenderer, docObj, this.areaProvider.AreaFieldInfos);
            renderer.MaxElementHeight = maxHeight;
            renderer.Format(area, prevRenderInfo.FormatInfo);
            //KlPo Ich weiss nicht wo er's speichert, aber hier darf er's anscheinend nicht.
            //KlPo: Removed

            //if (renderer.RenderInfo.FormatInfo.IsEnding)
            //{
            //    renderInfos.Add(renderer.RenderInfo);
            //    ++idx;
            //}
          }
          else if (NeedsEndingOnNextArea(idx, renderer, area))
          {
            renderer.RenderInfo.RemoveEnding();
            prevRenderInfo = FinishPage(renderer.RenderInfo, pagebreakBefore, ref renderInfos);
            if (prevRenderInfo != null)
              prevFormatInfo = prevRenderInfo.FormatInfo;
            else
            {
              prevFormatInfo = null;
              isFirstOnPage = true;
            }
            prevBottomMargin = 0;
            area = this.areaProvider.GetNextArea();
            maxHeight = area.Height;
          }
          else
          {
            renderInfos.Add(renderer.RenderInfo);
            isFirstOnPage = false;
            areaProvider.PositionVertically(renderer.RenderInfo.LayoutInfo);
            if (renderer.RenderInfo.LayoutInfo.VerticalReference == VerticalReference.PreviousElement)
            {
              prevBottomMargin = renderer.RenderInfo.LayoutInfo.MarginBottom;
              if (renderer.RenderInfo.LayoutInfo.Floating != Floating.None)
                area = area.Lower(renderer.RenderInfo.LayoutInfo.ContentArea.Height);
            }
            else
              prevBottomMargin = 0;

            prevFormatInfo = null;
            prevRenderInfo = null;

            ++idx;
          }
        }
        else
        {
          if (renderer.RenderInfo.FormatInfo.IsEmpty && isFirstOnPage)
          {
            area = area.Unite(new Rectangle(area.X, area.Y, area.Width, double.MaxValue));
            renderer = Renderer.Create(gfx, this.documentRenderer, docObj, this.areaProvider.AreaFieldInfos);
            renderer.MaxElementHeight = maxHeight;
            renderer.Format(area, prevFormatInfo);
            prevFormatInfo = null;
            ready = idx == this.elements.Count - 1; //!!!newTHHO 19.01.2007

            ++idx;
          }
          prevRenderInfo = FinishPage(renderer.RenderInfo, pagebreakBefore, ref renderInfos);
          if (prevRenderInfo != null)
            prevFormatInfo = prevRenderInfo.FormatInfo;
          else
          {
            prevFormatInfo = null;
          }
          isFirstOnPage = true;
          prevBottomMargin = 0;
#if false
          area = this.areaProvider.GetNextArea();
#else
          if (!ready)  //!!!newTHHO 19.01.2007: korrekt? oder GetNextArea immer ausführen???
          {
            area = this.areaProvider.GetNextArea();
            maxHeight = area.Height;
          }
#endif
        }
        if (idx == this.elements.Count)
        {
          this.areaProvider.StoreRenderInfos(renderInfos);
          ready = true;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentRenderInfo"></param>
    /// <param name="pageBreakBefore"></param>
    /// <param name="savedRenderInfo"></param>
    /// <param name="savedFormatInfo"></param>
    /// <returns>The RenderInfo to set as previous RenderInfo.</returns>
    RenderInfo FinishPage(RenderInfo lastRenderInfo, bool pagebreakBefore, ref ArrayList renderInfos)
    {
      RenderInfo prevRenderInfo;
      if (lastRenderInfo.FormatInfo.IsEmpty || pagebreakBefore)
      {
        prevRenderInfo = null;
      }
      else
      {
        prevRenderInfo = lastRenderInfo;
        renderInfos.Add(lastRenderInfo);
        if (lastRenderInfo.FormatInfo.IsEnding)
          prevRenderInfo = null;
      }
      this.areaProvider.StoreRenderInfos(renderInfos);
      renderInfos = new ArrayList();
      return prevRenderInfo;
    }

    /// <summary>
    /// Indicates that a break between areas has to be performed before the element with the given idx.
    /// </summary>
    /// <param name="idx">Index of the document element.</param>
    /// <param name="renderer">A formatted renderer for the document element.</param>
    /// <returns></returns>
    bool IsForcedAreaBreak(int idx, Renderer renderer, Area remainingArea)
    {
      //REM: Add more code here for keepTogether etc.
      FormatInfo formatInfo = renderer.RenderInfo.FormatInfo;
      LayoutInfo layoutInfo = renderer.RenderInfo.LayoutInfo;

      if (formatInfo.IsStarting && !formatInfo.StartingIsComplete)
        return true;

      if (layoutInfo.KeepTogether && !formatInfo.IsComplete)
        return true;

      if (layoutInfo.KeepTogether && layoutInfo.KeepWithNext)
      {
        Area area = remainingArea.Lower(layoutInfo.ContentArea.Height);
        return NextElementsDontFit(idx, area, layoutInfo.MarginBottom);
      }
      return false;
    }

    /// <summary>
    /// Indicates that the Ending of the element with the given idx has to be removed.
    /// </summary>
    /// <param name="idx">Index of the document element.</param>
    /// <param name="renderer">A formatted renderer for the document element.</param>
    /// <returns></returns>
    bool PreviousRendererNeedsRemoveEnding(RenderInfo prevRenderInfo, RenderInfo succedingRenderInfo, Area remainingArea)
    {
      if (prevRenderInfo == null)
        return false;
      LayoutInfo layoutInfo = succedingRenderInfo.LayoutInfo;
      FormatInfo formatInfo = succedingRenderInfo.FormatInfo;
      LayoutInfo prevLayoutInfo = prevRenderInfo.LayoutInfo;
      if (formatInfo.IsEnding && !formatInfo.EndingIsComplete)
      {
        Area area = this.areaProvider.ProbeNextArea();
        if (area.Height > prevLayoutInfo.TrailingHeight + layoutInfo.TrailingHeight + Renderer.Tolerance)
          return true;
      }

      return false;
    }

    bool NextElementsDontFit(int idx, Area remainingArea, XUnit previousMarginBottom)
    {
      XUnit elementDistance = previousMarginBottom;
      Area area = remainingArea;
      for (int index = idx + 1; index < this.elements.Count; ++index)
      {
        DocumentObject obj = this.elements[index];
        Renderer currRenderer = Renderer.Create(this.gfx, this.documentRenderer, obj, this.areaProvider.AreaFieldInfos);
        elementDistance = Math.Max(elementDistance, currRenderer.InitialLayoutInfo.MarginTop);
        area = area.Lower(elementDistance);

        if (area.Height <= 0)
          return true;

        currRenderer.Format(area, null);
        FormatInfo currFormatInfo = currRenderer.RenderInfo.FormatInfo;
        LayoutInfo currLayoutInfo = currRenderer.RenderInfo.LayoutInfo;

        if (!(currLayoutInfo.VerticalReference == VerticalReference.PreviousElement))
          return false;

        if (!currFormatInfo.StartingIsComplete)
          return true;

        if (currLayoutInfo.KeepTogether && !currFormatInfo.IsComplete)
          return true;

        if (!(currLayoutInfo.KeepTogether && currLayoutInfo.KeepWithNext))
          return false;

        area = area.Lower(currLayoutInfo.ContentArea.Height);
        if (area.Height <= 0)
          return true;

        elementDistance = currLayoutInfo.MarginBottom;
      }
      return false;
    }

    bool NeedsEndingOnNextArea(int idx, Renderer renderer, Area remainingArea)
    {
      LayoutInfo layoutInfo = renderer.RenderInfo.LayoutInfo;
      FormatInfo formatInfo = renderer.RenderInfo.FormatInfo;

      if (!formatInfo.EndingIsComplete)
        return false;

      if (layoutInfo.KeepWithNext)
      {
        remainingArea = remainingArea.Lower(layoutInfo.ContentArea.Height);
        return NextElementsDontFit(idx, remainingArea, layoutInfo.MarginBottom);
      }

      return false;
    }

    DocumentRenderer documentRenderer;
    XGraphics gfx;
  }
}
