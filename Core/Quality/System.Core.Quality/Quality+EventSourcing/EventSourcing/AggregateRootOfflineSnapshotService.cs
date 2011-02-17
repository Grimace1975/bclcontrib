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
using System.Collections.Generic;
namespace System.Quality.EventSourcing
{
    /// <summary>
    /// IAggregateRootOfflineSnapshotService
    /// </summary>
    public interface IAggregateRootOfflineSnapshotService
    {
        void MakeSnapshots(IEnumerable<Type> aggregateTypes);
    }

    /// <summary>
    /// AggregateRootOfflineSnapshotService
    /// </summary>
    public class AggregateRootOfflineSnapshotService : IAggregateRootOfflineSnapshotService
    {
        private readonly IAggregateRootRepository _repository;
        private readonly IOfflineSnaphotQuery _snaphotQuery;

        public AggregateRootOfflineSnapshotService(IAggregateRootRepository repository, IOfflineSnaphotQuery snaphotQuery)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (snaphotQuery == null)
                throw new ArgumentNullException("snaphotQuery");
            _repository = repository;
            _snaphotQuery = snaphotQuery;
        }

        public void MakeSnapshots(IEnumerable<Type> aggregateTypes)
        {
            foreach (var item in _snaphotQuery.GetAggregatesToSnapshot(aggregateTypes))
            {
                var aggregate = _repository.GetById(item.Item1, item.AggregateId, AggregateRootQueryOptions.UseNullAggregates);
                if (aggregate != null)
                    _repository.MakeSnapshot(aggregate);
            }
        }
    }
}