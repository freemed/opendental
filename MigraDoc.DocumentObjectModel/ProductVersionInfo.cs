#region MigraDoc - Creating Documents on the Fly
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

namespace MigraDoc
{
  /// <summary>
  /// Version info base for all MigraDoc related assemblies.
  /// </summary>
  public sealed class ProductVersionInfo
  {
    ProductVersionInfo() { }

    public const string Title = "empira MigraDoc";
    public const string Description = "Creating Documents on the Fly";
    public const string Creator = Title + " " + VersionMajor + "." + VersionMinor + "." + VersionBuild + " (" + Url + ")";
    public const string Version = VersionMajor + "." + VersionMinor + "." + VersionBuild + "." + VersionPatch;
    public const string Url = "www.migradoc.com";
    public const string Configuration = "";
    public const string Company = "empira Software GmbH, Cologne (Germany)";
    public const string Product = "empira MigraDoc®";
    public const string Copyright = "Copyright © 2001-2007 empira Software GmbH.";
    public const string Trademark = "empira MigraDoc®";
    public const string Culture = "";

    // Build = days since 2001-07-04  -  change values ONLY here
    public const string VersionMajor = "1";
    public const string VersionMinor = "2";
    public const string VersionBuild = "2175";
    public const string VersionPatch = "0";

#if DEBUG
    public static int BuildNumber = (System.DateTime.Now - new System.DateTime(2001, 7, 4)).Days;
#endif
  }
}
