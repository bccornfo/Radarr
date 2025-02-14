using NLog;
using NzbDrone.Common.Http;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.Parser;

namespace NzbDrone.Core.ImportLists.Rss
{
    public abstract class RssImportBase<TSettings> : HttpImportListBase<TSettings>
        where TSettings : RssImportBaseSettings, new()
    {
        public override bool Enabled => true;
        public override bool EnableAuto => false;

        public RssImportBase(IHttpClient httpClient,
            IImportListStatusService importListStatusService,
            IConfigService configService,
            IParsingService parsingService,
            Logger logger)
            : base(httpClient, importListStatusService, configService, parsingService, logger)
        {
        }

        public override ImportListFetchResult Fetch()
        {
            var generator = GetRequestGenerator();

            return FetchMovies(generator.GetMovies());
        }

        public override IParseImportListResponse GetParser()
        {
            return new RssImportBaseParser(_logger);
        }

        public override IImportListRequestGenerator GetRequestGenerator()
        {
            return new RssImportRequestGenerator
            {
                Settings = Settings
            };
        }
    }
}
