import "core-js/stable";

class App {
    constructor() {
        this.modules = [];

        document.documentElement.className = 'js';

        this.modules.forEach(module => {
            module.init();
        });
    }
}

window.addEventListener('load', () => new App());