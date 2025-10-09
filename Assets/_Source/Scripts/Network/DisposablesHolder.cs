using System;
using System.Collections.Generic;

namespace SportPresentation.App
{
    public class DisposablesHolder : IDisposablesHolder
    {
        private HashSet<IDisposable> _disposables = new HashSet<IDisposable>();

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void DisposeAll()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}