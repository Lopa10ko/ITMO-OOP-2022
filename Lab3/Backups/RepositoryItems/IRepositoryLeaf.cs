using System;
using System.IO;

namespace Backups.RepositoryItems;

public interface IRepositoryLeaf : IRepositoryItem
{
    string GetLeafId();
    string GetSourceId();
    Stream GetCurrentStream();
}