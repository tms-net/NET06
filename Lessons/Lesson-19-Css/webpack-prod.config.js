const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const {CleanWebpackPlugin} = require('clean-webpack-plugin');

class HtmlWebpackRemovePlugin {
  constructor(regEx) {
    this.regEx = regEx;
  }

  apply (compiler) {
    const regEx = this.regEx;
    compiler.hooks.compilation.tap('HtmlWebpackRemovePlugin', (compilation) => {
      HtmlWebpackPlugin.getHooks(compilation).beforeEmit.tapAsync(
        'HtmlWebpackRemovePlugin',
        (data, cb) => {
          // manipulate the content
          data.html = data.html.replace(regEx, '');
         
          // tell webpack to move on
          cb(null, data);
        }
      )
    })
  }
}

module.exports = {
  entry: "./src/main.js",
  mode: "production",
  module: {
    rules: [
      {
        test: /\.(js|jsx|mjs)$/,
        exclude: /(node_modules|bower_components)/,
        loader: "babel-loader",
        options: { presets: ["@babel/env"] }
      },
      {
        test: /\.css$/,        
        use: [MiniCssExtractPlugin.loader, "css-loader"]
      }
    ]
  },
  resolve: { extensions: ["*", ".js", ".jsx"] },
  output: {
    path: path.resolve(__dirname, "build/"),
    filename: "bundle.[contenthash].js"
  },
  plugins: [
    new CleanWebpackPlugin(),
    new HtmlWebpackPlugin({
      favicon: 'public/favicon.ico',
      title: "Learn Javascript",
      template: path.resolve(__dirname, "public/index.html")
    }),
    new HtmlWebpackRemovePlugin(/<\s*script[^>]*?src="\.\..*?\.js.*?>\s*<\s*\/\s*script>/),
    new HtmlWebpackRemovePlugin(/<link.*?href="\.\..*?\.css".*?\/>/),
    new MiniCssExtractPlugin({
        filename: "bundle.[contenthash].css"
    })
  ]
};
