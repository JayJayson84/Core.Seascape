const path = require('path');

module.exports = {
    mode: 'none',
    name: 'bundle',
    entry: {
        app: './Content/src/js/index.js'
    },
    output: {
        path: path.resolve(__dirname, 'Content', 'dest', 'js'),
        filename: 'bundle.js',
        library: "$",
        libraryTarget: "var",
    },
    module: {
        rules: [{
            test: /\.js$/,
            exclude: /node_modules/,
            use: {
                loader: 'babel-loader',
                options: {
                    presets: [
                        ['env', {
                            "modules": false,
                            "plugins": [
                                "@babel/plugin-transform-arrow-functions"
                            ],
                            "targets": {
                                "browsers": ["last 2 versions", "ie >= 10"]
                            },
                            "useBuiltIns": "usage",
                            "corejs": 3,
                            "debug": true
                        }]
                    ]
                }
            }
        }]
    },
    target: ['web', 'es5']
}
