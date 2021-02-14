/// <binding ProjectOpened='default' />
'use strict';

var gulp = require('gulp'),
    sass = require('gulp-dart-sass'),
    concat = require('gulp-concat'),
    cssmin = require('gulp-cssmin'),
    run = require('gulp-run'),
    terser = require('gulp-terser'),
    merge = require('merge-stream'),
    del = require('del'),
    bundleconfig = require('./bundleconfig.json');

const js_rules = {
    paths: 'Content/src/js/**/*.js'
}

const sass_rules = {
    paths: 'Content/src/scss/**/*.scss',
    source: 'Content/src/scss/main.scss',
    as: 'site.css',
    dest: 'Content/dest/css'
};

const regex = {
    any: /\.(css|js)$/,
    css: /\.css$/,
    js: /^((?!(bundle)).)*\.js$/,
    bundle: /bundle.*\.js$$/
};

gulp.task('build:bundle', function () {
    return run('npm run build-bundle').exec();
});

gulp.task('build', gulp.series(['build:bundle']));

gulp.task("build:sass", function () {
    return gulp.src(sass_rules.source)
        .pipe(sass())
        .pipe(concat(sass_rules.as))
        .pipe(gulp.dest(sass_rules.dest));
});

gulp.task('min:bundle', async function () {
    merge(getBundles(regex.bundle).map(bundle => {
        return gulp.src(bundle.inputFiles, { base: '.' })
            .pipe(concat(bundle.outputFileName))
            .pipe(terser())
            .pipe(gulp.dest('.'));
    }))
});

gulp.task('min:css', async function () {
    merge(getBundles(regex.css).map(bundle => {
        return gulp.src(bundle.inputFiles, { base: '.' })
            .pipe(concat(bundle.outputFileName))
            .pipe(cssmin())
            .pipe(gulp.dest('.'));
    }))
});

gulp.task('min', gulp.series(['min:bundle', 'build:sass', 'min:css']));

gulp.task('clean', () => {
    return del(getIOBundles());
});

gulp.task('clean:css', () => {
    return del(getIOBundles(regex.css));
});

gulp.task('clean:bundle', () => {
    return del(getIOBundles(regex.bundle));
});

gulp.task('watch', () => {
    gulp.watch(js_rules.paths, gulp.series([
        "clean:bundle",
        "build:bundle",
        "min:bundle"]));

    gulp.watch(sass_rules.paths, gulp.series([
        "clean:css",
        "build:sass",
        "min:css"]));
});

const getBundles = (regexPattern) => {
    return bundleconfig.filter(bundle => {
        return regexPattern.test(bundle.outputFileName);
    });
};

const getIOBundles = (regexPattern = regex.any) => {
    return bundleconfig
        .map(bundle => bundle.inputFiles)
        .reduce((prev, next) => {
            return prev.concat(next);
        })
        .concat(bundleconfig.map(bundle => bundle.outputFileName))
        .filter(e => {
            return regexPattern.test(e);
        });;
};

gulp.task('default', gulp.series("clean", "build", "min", "watch"));
