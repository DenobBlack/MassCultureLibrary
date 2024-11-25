﻿namespace MassCultureLibrary.Animes
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAnimeAsync();
        Task<Anime> AddAnimeAsync(Anime anime);
        Task DeleteAnimeAsync(Guid animeId);
        Task<Anime?> GetAnimeByIdAsync(Guid animeId);
        Task<IEnumerable<Anime>> GetAnimeByStatusAsync(string status);
        Task<Anime> UpdateAnimeAsync(Guid animeId, AnimeUpdateDto updateInfo);
        Task<string> GetAnimeTitleById(Guid animeId);
        Task<IEnumerable<Anime>> GetAnimeByGenreAsync(string genre);
        Task<Guid> GetAnimeIdbyNameAsync(string name);
    }
}