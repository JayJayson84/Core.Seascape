import { getRandomInt, getRandomColour } from '../functions/random';

const RandomiseButton = {
    elements: {
        target: document.querySelectorAll('[data-btn-randomise]')
    },
    attributes: {
        random: 'data-btn-randomise'
    },

    init() {
        if (!this.elements.target.length) return;
        this.render();
    },

    render() {
        this.elements.target.forEach(e => {
            e.addEventListener('click',
                () => {
                    const parentId = e.getAttribute(RandomiseButton.attributes.random);
                    RandomiseButton.onClick(parentId);
                }
            )
        });
    },

    onClick(parentId) {
        const parent = document.getElementById(parentId);
        const controls = parent.querySelectorAll('input');

        controls.forEach(control => {
            const type = control.getAttribute('type');

            switch (type) {
                case "number": {
                    RandomiseButton.setRandomNo(control);
                    break;
                }
                case "color": {
                    RandomiseButton.setRandomColour(control);
                    break;
                }
                default: {
                    break;
                }
            }
        });
    },

    setRandomNo(control) {
        const min = control.getAttribute('min');
        const max = control.getAttribute('max');

        control.value = getRandomInt(min, max);
    },

    setRandomColour(control) {
        control.value = getRandomColour();
    }
};

export default RandomiseButton