using System;

namespace SportPresentation.App
{
    public interface IDisposablesHolder
    {
        void Add(IDisposable disposable);
    }
}