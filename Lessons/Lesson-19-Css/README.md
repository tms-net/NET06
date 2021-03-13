# Getting Started with Frontend Development

This project is a sample of single page application frontend development setup. It uses [WebPack 5](https://webpack.js.org/) to process all resources (.js, .css, .html) that are required to run the application.
See [https://webpack.js.org/configuration/](https://webpack.js.org/configuration/) to learn configuration options.

At the beginning run
```
> npm install
```

## Available Scripts

In the project directory, you can run:

### `npm start`

To run the applicaiton WebPack [DevServer](https://webpack.js.org/configuration/dev-server/) web server is used.

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

Development configuration for WebPack is defined in [webpack-dev.config.js](./webpack-dev.config.js) configuration file.

Scripts will reload if you make edits.\
You will also see any lint errors in the console.\
You do not need to restart when you make edits.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles components in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
All resources are automatically included in result .html file, development references removed.\
Your app is ready to be deployed!

Production configuration for WebPack is defined in [webpack-prod.config.js](./webpack-dev.config.js) configuration file.

### `npm test`

Launches the test runner in the single run mode (you can manually change to interactive watch by setting `--autoWatch=true --singleRun=false` in corresponding `package.config` property).\

The tests are run by [Karma Project](https://karma-runner.github.io/latest/index.html) with settings specified in [karma.conf.js](./karma.conf.js) configuration file.

For browser launch only tests with **.spec** postfix are included (you can use DOM manipulation there).

### `npm run unit-test`

Launches a regular Node.js [Jasmine](https://jasmine.github.io/setup/nodejs.html) test runner with verbose reporter.\
Tests that are run by this runner are considered as regular unit tests, supposing to test pure functions in an ideal case.

For Node.js launch only tests with **.test** postfix are included (you can use DOM manipulation there).

## Learn more

### Testing
Learn how to write tests suites, create mocks and use assertions [here](https://jasmine.github.io/tutorials/your_first_suite.html).

Learn how to find and check DOM elements for test purposes [here](https://testing-library.com/docs/dom-testing-library/api).

### Running
To run a production build you can use a custom [server.js](./server.js) file that is run from `/build` folder.

More general solution is to use [serve](https://github.com/vercel/serve) package
```
> npm install -g serve
> serve build
```

Open http://localhost:5000 to view it in the browser.