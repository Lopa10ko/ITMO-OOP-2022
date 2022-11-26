using System.Collections.Generic;
using System.IO;
using Backups.RepositoryItems;
using Backups.Services;
using Backups.ZipArchivedItems;

namespace Backups.Visitors;

public interface IRepositoryVisitor
{
    void Visit(IRepositoryNode node);
    void Visit(IRepositoryLeaf leaf);
    IEnumerable<IZipArchivedItem> GetLastOnStack();
}