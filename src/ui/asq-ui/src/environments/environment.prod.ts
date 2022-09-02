export const environment = {
  production: true,
  baseUrl: 'http://asq.properties:4000/',
  staticAssetBaseUrl: 'http://asq.properties:2000/',
  domain: 'http://asq.properties/',
  cache: {
    expiration: {
      meetingSummaries: {
        min: 2
      },
      blogSummaries: {
        min: 2
      },
      videoSummaries: {
        min: 2
      },
      hostSummaries: {
        min: 2
      }
    }
  }
};
