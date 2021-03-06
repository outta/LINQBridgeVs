﻿#region License
// Copyright (c) 2013 Giovanni Campo
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using LINQBridgeVs.VisualStudio.Properties;

namespace LINQBridgeVs.VisualStudio
{
    internal struct Settings
    {
        public List<string> InstallationPaths;

        public string AssemblyLocation;

        public string MsBuildVersion;

    }

    public static class VisualStudioOptions
    {
        private static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string Vs2010Path1 = MyDocuments + Resources.VS2010Path1;
        private static readonly string Vs2010Path2 = MyDocuments + Resources.VS2010Path2;
        private static readonly string Vs2012Path1 = MyDocuments + Resources.VS2012Path1;
        private static readonly string Vs2013Path1 = MyDocuments + Resources.VS2013Path1;


        private static readonly Dictionary<string, Settings> VisualStudioPaths;

        static VisualStudioOptions()
        {
            VisualStudioPaths = new Dictionary<string, Settings>
            {
                {
                    "10.0", new Settings
                    {
                        InstallationPaths =
                            new List<string> {Vs2010Path1, Vs2010Path2},

                        AssemblyLocation = DynamicVisualizer.V10.Settings.AssemblyLocation,
                        MsBuildVersion = "v4.0"
                    }
                },
                {
                    "11.0", new Settings
                    {
                        InstallationPaths =
                            new List<string> {Vs2012Path1},
                        AssemblyLocation = DynamicVisualizer.V11.Settings.AssemblyLocation,
                        MsBuildVersion = "v4.0"

                    }
                },
                {
                    "12.0", new Settings
                    {
                        InstallationPaths =
                            new List<string> {Vs2013Path1},
                        AssemblyLocation = DynamicVisualizer.V12.Settings.AssemblyLocation,
                        MsBuildVersion = "v12.0"
                    }
                }
            };
        }

        private static void CheckVersion(string vsInputVersion)
        {
            if (vsInputVersion == null) throw new ArgumentNullException("vsInputVersion");

            if (!VisualStudioPaths.ContainsKey(vsInputVersion))
                throw new ArgumentException("This Version of visual studio is not yet supported.", "vsInputVersion");
        }

        public static string GetVisualizerAssemblyLocation(string visualStudioVersion)
        {
            CheckVersion(visualStudioVersion);

            return VisualStudioPaths[visualStudioVersion].AssemblyLocation;
        }

        public static IEnumerable<string> GetInstallationPath(string visualStudioVersion)
        {
            CheckVersion(visualStudioVersion);

            return VisualStudioPaths[visualStudioVersion].InstallationPaths;
        }
    }
}
