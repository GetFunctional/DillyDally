using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GF.DillyDally.Mvvmc.Contracts;

namespace GF.DillyDally.Mvvmc
{
    public class PagedContentViewModel : ViewModelBase
    {
        private readonly ObservableCollection<IDisplayPage> _pages;

        public PagedContentViewModel()
        {
            this._pages = new ObservableCollection<IDisplayPage>();
        }

        public IReadOnlyList<IDisplayPage> Pages
        {
            get
            {
                return this._pages;
            }
        }

        public void AddPage(IDisplayPage page)
        {
            this._pages.Add(page);
        }

        public T GetPage<T>() where T : IDisplayPage
        {
            return this.Pages.OfType<T>().Single();
        }

        public IEnumerable<T> GetPages<T>() where T : IDisplayPage
        {
            return this.Pages.OfType<T>();
        }
    }
}