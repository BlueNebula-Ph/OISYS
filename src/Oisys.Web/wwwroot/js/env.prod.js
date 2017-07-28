(function (window) {
    window._environment = window.__env || {};

    // API url
    window._environment.baseUrl = 'http://localhost';

    // Whether or not to enable debug mode
    // Setting this to false will disable console output
    window._environment.enableDebug = false;
}(this));