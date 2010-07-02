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
using System.Patterns.Schema;
namespace System.Patterns.Versioning
{
	/// <summary>
	/// VersionTypeUriPart
	/// </summary>
	public class VersionTypeUriPart : UriPartBase
	{
		private const string UriId_DraftId = "@Draft";
		private const int UriId_DraftIdLength = 6;
		private const string UriId_ArchiveId = "@Archive";
		private const int UriId_ArchiveIdLength = 8;

		public static string CreateDraftKeyValue(string id)
		{
			if (string.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");
			return id + "=" + UriId_DraftId;
		}
		public static string CreateDraftKeyValue(string id, string target)
		{
			if (string.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");
			if (string.IsNullOrEmpty(target))
				throw new ArgumentNullException("target");
			return id + "=" + UriId_DraftId + "-" + target;
		}

		public override string CreateUriPart(ref UriPart part, string newValue)
		{
			string value = (newValue ?? part.Value);
			return (string.IsNullOrEmpty(value) ? string.Empty : value + "/");
		}

		public override void ParseUriPart(ref UriPart part, IUriPartScanner scanner)
		{
			if (scanner == null)
				throw new ArgumentNullException("scanner");
			var manager = VersionManagerBase.Get();
			if (manager == null)
				return;
			string normalizedPath = scanner.NormalizedPath;
			int normalizedPathIndex;
			if ((normalizedPath.Length > 1) && ((normalizedPathIndex = normalizedPath.IndexOf("/", 1)) > -1))
			{
				string id = normalizedPath.Substring(1, normalizedPathIndex - 1);
				// draft
				if (id.StartsWith(UriId_DraftId, StringComparison.OrdinalIgnoreCase))
				{
					EnsureAccess(manager, VersionType.Draft);
					if (id.Length > UriId_DraftIdLength)
					{
						int draftIdTargetIndex = UriId_DraftIdLength + 1;
						if ((id[UriId_DraftIdLength] == '-') && (id.Length > draftIdTargetIndex))
							manager.VersionTarget = id.Substring(draftIdTargetIndex);
					}
					part.Value = part.RequestValue = id;
					scanner.IncreasePath(normalizedPathIndex);
					manager.VersionType = VersionType.Draft;
					return;
				}
				// archive
				else if (id.StartsWith(UriId_ArchiveId, StringComparison.OrdinalIgnoreCase))
				{
					EnsureAccess(manager, VersionType.Archive);
					if (id.Length > UriId_ArchiveIdLength)
					{
						int archiveIdTargetIndex = UriId_ArchiveIdLength + 1;
						if ((id[UriId_ArchiveIdLength] == '-') && (id.Length > archiveIdTargetIndex))
							manager.VersionTarget = id.Substring(archiveIdTargetIndex);
					}
					part.Value = part.RequestValue = id;
					scanner.IncreasePath(normalizedPathIndex);
					manager.VersionType = VersionType.Archive;
					return;
				}
			}
			manager.VersionType = VersionType.Publish;
		}

		private void EnsureAccess(VersionManagerBase manager, VersionType versionType)
		{
			//if (!manager.HasAccess(versionType))
			//    throw new System.Web.HttpException(401, "Unauthorized");
		}
	}
}