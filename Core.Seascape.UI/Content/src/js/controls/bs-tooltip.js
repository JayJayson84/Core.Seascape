import Tooltip from '../lib/bootstrap.native/components/tooltip-native.esm.js'

const BSTooltip = {
    elements: {
        target: document.querySelectorAll('[data-toggle="tooltip"]')
    },

    init() {
        if (!this.elements.target.length) return;
        if (window.document.documentMode) return;
        this.render();
    },

    render() {
        this.elements.target.forEach(e => {
            new Tooltip(e, {
                animation: 'slideNfade',
                delay: 150,
                offset: 0,
                template: '<div class="tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
            })
        });
    }
};

export default BSTooltip