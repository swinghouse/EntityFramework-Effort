﻿#region License

// Copyright (c) 2011 Effort Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Configuration;
using System.Data;
using System.Threading;

namespace Effort.Provider
{
    public static class EffortProviderConfiguration
    {
        public static readonly string ProviderInvariantName = "EffortProvider";

        private static bool registered = false;
        private static object sync = new object();

        public static void RegisterProvider()
        {
            if (!registered)
            {
                lock (sync)
                {
                    if (!registered)
                    {
                        RegisterProvider("Effort Provider", ProviderInvariantName, typeof(EffortProviderFactory));

                        Thread.MemoryBarrier();
                        registered = true;
                    }
                }
            }
        }

        private static void RegisterProvider(string name, string invariantName, Type factoryType)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(invariantName))
            {
                throw new ArgumentNullException("invariantName");
            }

            if (factoryType == null)
            {
                throw new ArgumentNullException("factoryType");
            }

            var data = (DataSet)ConfigurationManager.GetSection("system.data");
            var providerFactories = data.Tables["DbProviderFactories"];
            providerFactories.Rows.Add(name, name, invariantName, factoryType.AssemblyQualifiedName);
        }
        
    }
}
