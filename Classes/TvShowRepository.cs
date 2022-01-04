using System;
using System.Collections.Generic;
using TvShows.Interfaces;

namespace TvShows
{
    public class TvShowRepository : IRepository<TvShow>
    {

        private List<TvShow> listTvShow = new List<TvShow>();

        public void Delete(int id)
        {
            listTvShow[id].Delete();
        }

        public void Insert(TvShow obj)
        {
            listTvShow.Add(obj);
        }

        public List<TvShow> List()
        {
            return listTvShow;
        }

        public int NextId()
        {
            return listTvShow.Count;
        }

        public TvShow ReturnById(int id)
        {
            return listTvShow[id];
        }

        public void Update(int id, TvShow obj)
        {
            listTvShow[id] = obj;
        }
    }
}