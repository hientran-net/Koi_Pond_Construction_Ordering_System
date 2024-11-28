import gulp from 'gulp';
import sass from 'gulp-sass';
import postcss from 'gulp-postcss';
import autoprefixer from 'autoprefixer';
import browserSync from 'browser-sync';
import concat from 'gulp-concat';
import uglify from 'gulp-uglify';
import webpack from 'webpack-stream';
import named from 'vinyl-named';
import del from 'del';

//Directories Constant
const dirs = {
    app: 'app/',
    dist: 'dist/',
    assets: 'assets/'
};

//Paths Constant
const paths = {
    app: {
        assets: dirs.app + dirs.assets,
        scss: dirs.app + dirs.assets + 'scss/',
        css: dirs.app + dirs.assets + 'css/',
        es6: dirs.app + dirs.assets + 'es6/',
        es6Pages: dirs.app + dirs.assets + 'es6/pages/',
        js: dirs.app + dirs.assets + 'js/',
        jsPages: dirs.app + dirs.assets + 'js/pages',
        vendors: dirs.app + dirs.assets + 'vendors/',
        fonts: dirs.app + dirs.assets + 'fonts/',
        images: dirs.app + dirs.assets + 'images/'
    },
    dist: {
        assets: dirs.dist + dirs.assets,
        css: dirs.dist + dirs.assets + 'css/',
        js: dirs.dist + dirs.assets + 'js/',
        jsPages: dirs.dist + dirs.assets + 'js/pages',
        fonts: dirs.dist + dirs.assets + 'fonts/',
        images: dirs.dist + dirs.assets + 'images/'
    }
}


//Core Vendors Files
const vendorsFiles = {
    js: [
        paths.app.vendors + 'jquery/dist/jquery.js',
        paths.app.vendors + 'popper.js/dist/umd/popper.min.js',
        paths.app.vendors + 'bootstrap/dist/js/bootstrap.js',
        paths.app.vendors + 'perfect-scrollbar/js/perfect-scrollbar.jquery.js',

        // Dependencies for Demo
        paths.app.vendors + 'prism/prism.js'
    ]
}


// Compile SCSS to CSS 
gulp.task('scss-dev', () => {
    return gulp.src(paths.app.scss + 'app.scss')
    .pipe(sass.sync({outputStyle: 'compressed'}).on('error', sass.logError))
    .pipe(postcss([autoprefixer()]))
    .pipe(concat('app.min.css'))
    .pipe(gulp.dest(paths.app.css));
});


// Watch SCSS Changes
gulp.task('watch-scss', () => {
    return gulp.watch(paths.app.scss + '**/*.scss', gulp.series('scss-dev'));
});

// Create Local Server
gulp.task('local-server', () => {
    browserSync.init({
        server: {
            baseDir: dirs.app
        },
        injectChanges: true,
        notify: false
    });

    gulp.watch([
        paths.app.scss + '**/*.scss',
        paths.app.es6 + '**/*.js',
        dirs.app + '**/*.html',
    ]).on('change', () => {
        browserSync.reload();
        console.log('Press Ctrl + C to terminate the server')
    });
});

//Compile core vendors JS
gulp.task('compile-vendors-js', () => {
    return gulp.src(vendorsFiles.js)
        .pipe(concat('vendors' + '.min.js'))
        .pipe(gulp.dest(paths.app.js));
});

// Convert App.js ES6 to ES5 
gulp.task('appES6toES5', () => {
    return gulp.src(paths.app.es6 + 'app.js')
    .pipe(webpack(
        {
            mode: 'development',
            watch: true,
            output: {
                filename: 'app.min.js'
            } 
        }
    ))
    .pipe(gulp.dest(paths.app.js));
});

// Convert Pages ES6 to ES5
gulp.task('pagesES6toES5', () => {
    return gulp.src(paths.app.es6Pages + '**/*.js')
    .pipe(named())
    .pipe(webpack(
        {
            mode: 'development',
            watch: true
        }
    ))
    .pipe(gulp.dest(paths.app.jsPages));
});

//Clean Build Directory
gulp.task('build-clean', () => {
    return del(dirs.dist);
});

//Copy Files to dist folder
gulp.task('build-copy', () => {
    return gulp.src(
        [
            paths.app.css + '**/*.css',
            paths.app.js + '*.js',
            paths.app.jsPages + '**/*.js',
            paths.app.vendors + '**/*.*',
            paths.app.vendors + '**/*.*',
            paths.app.fonts + '**/*.*',
            paths.app.images + '**/*.*',
            dirs.app + '**/*.html'
        ], {
            base: dirs.app
        }
    ).pipe(gulp.dest(dirs.dist));
});

//Uglify JS
gulp.task('uglify-js', () => {
    return gulp.src(
        [
            paths.dist.js + '*.js',
            paths.dist.jsPages + '**/*.js',
        ]
    )
    .pipe(uglify())
    .pipe(gulp.dest(paths.dist.js))
}); 
    
// Watch ES6 Changes
gulp.task('watch-js', () => {
    return gulp.watch(paths.app.es6 + '**/*.js', gulp.series('appES6toES5','pagesES6toES5'));
});

// Gather JS Related Tasks 
gulp.task('js', gulp.parallel('compile-vendors-js', 'appES6toES5', 'pagesES6toES5'))

// Gather all Serve, Compile & Watch in one task
gulp.task('serve', gulp.parallel('local-server', 'scss-dev','watch-scss', 'js', 'watch-js'));

//Gather all Build task
gulp.task('build', gulp.series('build-clean', 'build-copy'));