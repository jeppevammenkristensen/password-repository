define('environment',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        debug: true,
        testing: true,
        api: 'http://localhost:5000/api'
    };
});

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define('app',["require", "exports", "aurelia-fetch-client", "aurelia-framework", "./environment"], function (require, exports, aurelia_fetch_client_1, aurelia_framework_1, environment_1) {
    "use strict";
    var App = (function () {
        function App(client) {
            this.client = client;
            this.uploadedFiles = null;
            this.message = 'Hello World!';
            this.client.configure(function (config) {
                config.useStandardConfiguration();
            });
        }
        App.prototype.attached = function () {
        };
        App.prototype.upload = function () {
            var _this = this;
            var formData = new FormData();
            for (var i = 0; i < this.uploadedFiles.length; i++) {
                formData.append('uploadedFiles', this.uploadedFiles[0]);
            }
            return this.client.fetch(environment_1.default.api + "/passwords/upload", {
                method: 'post',
                body: formData
            }).then(function (x) { return x.json(); })
                .then(function (x) { return _this.uploadedId[0]; })
                .catch(function (rej) { return alert(rej); });
        };
        App.prototype.process = function () {
            return this.client.fetch(environment_1.default.api + "/password/process/" + this.uploadedId, {
                method: 'get',
            })
                .then(function (x) { return x.json(); })
                .then(function (x) { return alert(x); });
        };
        return App;
    }());
    App = __decorate([
        aurelia_framework_1.autoinject(),
        __metadata("design:paramtypes", [aurelia_fetch_client_1.HttpClient])
    ], App);
    exports.App = App;
});

define('main',["require", "exports", "./environment"], function (require, exports, environment_1) {
    "use strict";
    Promise.config({
        warnings: {
            wForgottenReturn: false
        }
    });
    function configure(aurelia) {
        aurelia.use
            .standardConfiguration()
            .feature('resources');
        if (environment_1.default.debug) {
            aurelia.use.developmentLogging();
        }
        if (environment_1.default.testing) {
            aurelia.use.plugin('aurelia-testing');
        }
        aurelia.start().then(function () { return aurelia.setRoot(); });
    }
    exports.configure = configure;
});

define('resources/index',["require", "exports"], function (require, exports) {
    "use strict";
    function configure(config) {
    }
    exports.configure = configure;
});

define('text!app.html', ['module'], function(module) { module.exports = "<template><h1>${message}</h1><form><input type=\"file\" name=\"\" files.bind=\"uploadedFiles\" multiple=\"multiple\"> <button type=\"\" click.delegate=\"upload()\" show.bind=\"uploadedFiles\">Upload</button></form></template>"; });
//# sourceMappingURL=app-bundle.js.map