﻿namespace MangaReader.Model
{
    public class Manga
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int AvailableChapters { get; set; }

        public string Miniature
        {
            get { return $"{Config.Server.url}/mangas/{Id}/miniatures"; }
        }
    }
}
