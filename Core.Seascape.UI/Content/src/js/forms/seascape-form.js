import Alert from "../controls/alert"

const SeascapeForm = {
    regex: {
        stylesheet: /[a-fA-F0-9]{8}-([a-fA-F0-9]{4}-){3}[a-fA-F0-9]{12}(\.min\.css)/i
    },
    elements: {
        hiddenFormGuid: 'SeascapeForm_AnimatedSeascapeImage_Guid'
    },
    attributes: {
        guid: 'data-animated-seascape-guid'
    },

    onBegin() {
        document.querySelectorAll('button').forEach(btn => {
            btn.disabled = true;
        });
    },

    onComplete() {
        document.querySelectorAll('button').forEach(btn => {
            btn.disabled = false;
        });
    },

    onSuccess() {
        Alert.hideAlert();

        this.updateStylesheet();

        if (!window.document.documentMode) {
            $.Slideshow.init();
        }
    },

    onFailed(msg) {
        Alert.showAlert(msg);

        if (!window.document.documentMode) {
            $.Slideshow.init();
        }
    },

    updateStylesheet() {
        this.removeStylesheet();

        var guid = this.getImageGuid();

        this.setFormGuid(guid);
        this.addStylesheet(guid);
    },

    removeStylesheet() {
        document.styleSheets.forEach(stylesheet => {
            if (this.regex.stylesheet.test(stylesheet.href)) {
                stylesheet.remove = true;
            }
        });
    },

    addStylesheet(guid) {
        var stylesheetPath = `/css/generated/${guid}.min.css`;
        var link = document.createElement("link");

        link.rel = "stylesheet";
        link.href = stylesheetPath;

        document.head.appendChild(link);
    },

    getImageGuid() {
        var element = document.querySelector(`[${this.attributes.guid}]`);

        return element.getAttribute(this.attributes.guid);
    },

    setFormGuid(guid) {
        var element = document.getElementById(this.elements.hiddenFormGuid);

        element.setAttribute("value", guid);
    }
};

export default SeascapeForm