#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Linq;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
namespace System.Web
{
    public partial class StaticSiteMapProviderEx
    {
        private List<PartialProvider> _partialProviders; // = new List<PartialProvider>();
        private ReaderWriterLockSlim _partialProviderRwLock = new ReaderWriterLockSlim();

        private class PartialProvider : IValue<string[]>
        {
            public string[] Value { get; set; }
            public SiteMapProvider SiteMapProvider { get; set; }
            public SiteMapNodeEx NodeEx { get; set; }
        }

        protected bool HasPartialProviders
        {
            get { return (_partialProviders != null); }
        }

        private SiteMapNode FindSiteMapNodeFromPartialProvider(string[] segments)
        {
            _partialProviderRwLock.EnterReadLock();
            if (_partialProviders != null)
            {
                PartialProvider tightestValue;
                bool hasTightestValue = _partialProviders.TryGetTightestMatch(segments, out tightestValue);
                if (hasTightestValue)
                {
                    _partialProviderRwLock.ExitReadLock();
                    return DelegateFindSiteMapNodeToChildProvider(tightestValue, segments, tightestValue.Value);
                }
            }
            _partialProviderRwLock.ExitReadLock();
            return null;
        }

        private SiteMapNode CheckExNodeFromScanAsPartialProvider(SiteMapNodeEx nodeEx, string[] segments)
        {
            var partialProviderExtent = nodeEx.Get<SiteMapNodePartialProviderExtent>();
            if (partialProviderExtent == null)
                return null;
            var newProvider = GetProviderFromName(partialProviderExtent.ProviderId);
            string queryPart;
            string[] tightestSegments = GetUrlSegments(nodeEx.Url, out queryPart);
            var partialProvider = new PartialProvider { Value = tightestSegments, SiteMapProvider = newProvider, NodeEx = nodeEx };
            {
                _partialProviderRwLock.EnterWriteLock();
                if (_partialProviders == null)
                    _partialProviders = new List<PartialProvider>();
                _partialProviders.Add(partialProvider);
                _partialProviderRwLock.ExitWriteLock();
            }
            return DelegateFindSiteMapNodeToChildProvider(partialProvider, segments, tightestSegments);
        }

        private static SiteMapNode DelegateFindSiteMapNodeToChildProvider(PartialProvider provider, string[] segments, string[] tightestSegments)
        {
            var siteMapProvider = provider.SiteMapProvider;
            if (siteMapProvider == null)
                return provider.NodeEx;
            var tightestSegmentsLength = tightestSegments.Length;
            string newUrl = "/" + string.Join("/", segments, tightestSegmentsLength, segments.Length - tightestSegmentsLength);
            return siteMapProvider.FindSiteMapNode(newUrl);
        }
    }
}