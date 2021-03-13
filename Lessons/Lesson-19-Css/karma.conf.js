module.exports = function(config) {
    config.set({
      frameworks: ['jasmine', 'webpack'],
      plugins: [
        'karma-webpack',
        'karma-jasmine',
        'karma-chrome-launcher',
        'karma-verbose-reporter'
      ],
      browsers : ['Chrome'],
      files: [
        { pattern: 'spec/**/*.[sS]pec.?(m)js', watched: false }
      ],

      preprocessors: {
        // add webpack as preprocessor
        'spec/**/*.[sS]pec.?(m)js': [ 'webpack' ]
      },

      webpack: {
        module: {
            rules: [
              {
                test: /\.(js|jsx|mjs)$/,
                exclude: /(node_modules|bower_components)/,
                loader: "babel-loader",
                options: { presets: ["@babel/env"] }
              }
            ]
          }
      }
    })
  }