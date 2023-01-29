
using MigrationTask.Models;
using MigrationTask.Models.Responses;
using MigrationTask.Services.RefreshTokenRepositories;
using MigrationTask.Services.TokenGenerators;

namespace MigrationTask.Services.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(AdminLogin admin)
        {
            string accessToken = _accessTokenGenerator.GenerateToken(admin);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDto = new RefreshToken()
            {
                Token = refreshToken,
                AdminId = admin.Id
            };

            await _refreshTokenRepository.Create(refreshTokenDto);

            return new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
