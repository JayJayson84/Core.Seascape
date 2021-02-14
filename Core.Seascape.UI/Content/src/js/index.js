import "core-js/stable";
import BSTooltip from "./controls/bs-tooltip"
import RandomiseButton from "./controls/randomise-button"
import Slideshow from "./controls/slideshow"
import SeascapeForm from "./forms/seascape-form"

class App {
    constructor() {
        this.modules = [
            BSTooltip,
            RandomiseButton,
            Slideshow
        ];

        document.documentElement.className = 'js';

        this.modules.forEach(module => {
            module.init();
        });
    }
}

window.addEventListener('load', () => new App());

export { Slideshow, SeascapeForm }