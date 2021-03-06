﻿/// <binding BeforeBuild='copy_from_npm_modules' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');

gulp.task('copy_from_npm_modules', function () {
    gulp.src('./node_modules/knockout/build/output/knockout-latest.js').pipe(gulp.dest('./wwwroot/js/lib'));
    gulp.src('./node_modules/superagent/superagent.js').pipe(gulp.dest('./wwwroot/js/lib'));
});