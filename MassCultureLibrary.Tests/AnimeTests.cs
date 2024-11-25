﻿using FluentAssertions;
using MassCultureLibrary.Animes;
using Moq;

namespace MassCultureLibrary.Tests
{
    public class AnimeTests
    {
        private IAnimeService _animeService;
        private readonly IAnimeRepository _animeRepository;
        private readonly Mock<IAnimeRepository> _mockAnimeRepository;
        private readonly Anime _anime;
        public AnimeTests()
        {
            _animeRepository = new JsonAnimeStorage();
            _mockAnimeRepository = new Mock<IAnimeRepository>();
            _animeService = new AnimeService(_animeRepository);
            _anime = new Anime {Id = Guid.NewGuid(), Title = "Наруто", Genre = "Экшен", Status = "Завершено" };
        }

        [Fact]
        public async Task AddAnime_ShouldAddAnimeCorrectly()
        {
            var anime = _anime;
            var result = await _animeService.AddAnimeAsync(anime);

            result.Should().NotBeNull();
            result.Title.Should().Be("Наруто");
        }

        [Fact]
        public async Task GetAnimeByTitle_ShouldReturnAnimeWithCorrectStatus()
        {
            var title = "Наруто";

            var anime = await _animeService.GetAnimeByTitleAsync(title);

            anime.Should().NotBeNull();
            anime.Title.Should().Be("Наруто");
        }

        [Fact]
        public async Task GetAnimeByGenre_ShouldReturnAnimeWithCorrectStatus()
        {
            var genre = "Экшен";

            var animes = await _animeService.GetAnimeByGenreAsync(genre);

            animes.Should().NotBeEmpty();
            animes.All(a => a.Genre == genre).Should().BeTrue();
        }

        [Fact]
        public async Task GetAnimeByStatus_ShouldReturnAnimeWithCorrectStatus()
        {
            var status = "Онгоинг";

            var animes = await _animeService.GetAnimeByStatusAsync(status);

            animes.Should().NotBeEmpty();
            animes.All(a => a.Status == status).Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAnime_ShouldUpdateAnimeStatus()
        {
            var animeId = _anime.Id;
            var updateInfo = new AnimeUpdateDto { Status = "Онгоинг" };

            var updatedAnime = await _animeService.UpdateAnimeAsync(animeId, updateInfo);

            updatedAnime.Should().NotBeNull();
            updatedAnime.Status.Should().Be("Онгоинг");
        }

        [Fact]
        public async Task DeleteAnimeById_ShouldRemoveAnime()
        {
            var animeId = _anime.Id;

            await _animeService.DeleteAnimeByIdAsync(animeId);

            var anime = await _animeService.GetAnimeByIdAsync(animeId);
            anime.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAnime_ShouldRemoveAnime()
        {
            Anime anime = new() { Id = Guid.NewGuid(), Title = "Наруто", Genre = "Экшен", Status = "Завершено" };

            await _animeService.DeleteAnimeAsync(anime);

            var deletedAnime = await _animeService.GetAnimeByIdAsync(anime.Id);
            deletedAnime.Should().BeNull();
        }
    }

}
